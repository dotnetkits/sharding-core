﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShardingCore.Core.EntityMetadatas;
using ShardingCore.Core.VirtualDatabase.VirtualDataSources;
using ShardingCore.Core.VirtualRoutes.DataSourceRoutes.RouteRuleEngine;
using ShardingCore.Core.VirtualRoutes.TableRoutes.RouteTails.Abstractions;
using ShardingCore.Core.VirtualRoutes.TableRoutes.RoutingRuleEngine;
using ShardingCore.Extensions;
using ShardingCore.Sharding.Abstractions;
using ShardingCore.Sharding.ParallelTables;
using ShardingCore.Sharding.ShardingExecutors.Abstractions;
using ShardingCore.Sharding.ShardingExecutors.QueryableCombines;

namespace ShardingCore.Sharding.ShardingExecutors
{
    public class MergeQueryCompilerContext : IMergeQueryCompilerContext
    {

        private readonly IParallelTableManager _parallelTableManager;
        private readonly IQueryCompilerContext _queryCompilerContext;
        private readonly QueryCombineResult _queryCombineResult;
        private readonly Type _queryEntityType;
        private readonly DataSourceRouteResult _dataSourceRouteResult;
        private readonly bool _isEnumerableQuery;
        private readonly IEnumerable<TableRouteResult> _tableRouteResults;

        /// <summary>
        /// 本次查询跨库
        /// </summary>
        public readonly bool _isCrossDataSource;

        /// <summary>
        /// 本次查询跨表
        /// </summary>
        public readonly bool _isCrossTable;


        private QueryCompilerExecutor _queryCompilerExecutor;
        private bool? hasQueryCompilerExecutor;
        private MergeQueryCompilerContext(IQueryCompilerContext queryCompilerContext, QueryCombineResult queryCombineResult,Type queryEntityType, DataSourceRouteResult dataSourceRouteResult, IEnumerable<TableRouteResult> tableRouteResults,bool isEnumerableQuery)
        {
            _queryCompilerContext = queryCompilerContext;
            _queryCombineResult = queryCombineResult;
            _queryEntityType = queryEntityType;
            _parallelTableManager = (IParallelTableManager)ShardingContainer.GetService(typeof(IParallelTableManager<>).GetGenericType0(queryCompilerContext.GetShardingDbContextType()));
            _dataSourceRouteResult = dataSourceRouteResult;
            _isEnumerableQuery = isEnumerableQuery;
            _tableRouteResults = GetTableRouteResults(tableRouteResults);
            _isCrossDataSource = dataSourceRouteResult.IntersectDataSources.Count > 1;
            _isCrossTable = _tableRouteResults.Count() > 1;
        }

        private IEnumerable<TableRouteResult> GetTableRouteResults(IEnumerable<TableRouteResult> tableRouteResults)
        {
            if (_queryCompilerContext.GetQueryEntities().Count > 1)
            {
                var entityMetadataManager = _queryCompilerContext.GetEntityMetadataManager();
                var queryShardingTables = _queryCompilerContext.GetQueryEntities().Where(o => entityMetadataManager.IsShardingTable(o)).ToArray();
                if (queryShardingTables.Length > 1 && _parallelTableManager.IsParallelTableQuery(queryShardingTables))
                {
                    return tableRouteResults.Where(o => o.ReplaceTables.Select(p => p.Tail).ToHashSet().Count == 1);
                }
            }
            return tableRouteResults;
        }

        public static MergeQueryCompilerContext Create(IQueryCompilerContext queryCompilerContext, QueryCombineResult queryCombineResult, Type queryEntityType, DataSourceRouteResult dataSourceRouteResult,IEnumerable<TableRouteResult> tableRouteResults,bool isEnumerableQuery)
        {
            return new MergeQueryCompilerContext(queryCompilerContext, queryCombineResult, queryEntityType,dataSourceRouteResult, tableRouteResults, isEnumerableQuery);
        }
        public ISet<Type> GetQueryEntities()
        {
            return _queryCompilerContext.GetQueryEntities();
        }

        public IShardingDbContext GetShardingDbContext()
        {
            return _queryCompilerContext.GetShardingDbContext();
        }

        public Expression GetQueryExpression()
        {
            return _queryCompilerContext.GetQueryExpression();
        }

        public IEntityMetadataManager GetEntityMetadataManager()
        {
            return _queryCompilerContext.GetEntityMetadataManager();
        }

        public Type GetShardingDbContextType()
        {
            return _queryCompilerContext.GetShardingDbContextType();
        }

        public QueryCompilerExecutor GetQueryCompilerExecutor()
        {
            if (!hasQueryCompilerExecutor.HasValue)
            {
                if (!IsMergeQuery())
                {
                    var routeTailFactory = ShardingContainer.GetService<IRouteTailFactory>();
                    var dbContext = GetShardingDbContext().GetDbContext(_dataSourceRouteResult.IntersectDataSources.First(), false, routeTailFactory.Create(_tableRouteResults.First()));
                    _queryCompilerExecutor = new QueryCompilerExecutor(dbContext, GetQueryExpression());
                }
            }

            return _queryCompilerExecutor;
        }


        public QueryCombineResult GetQueryCombineResult()
        {
            return _queryCombineResult;
        }

        public Type GetQueryEntityType()
        {
            return _queryEntityType;
        }

        public IEnumerable<TableRouteResult> GetTableRouteResults()
        {
            return _tableRouteResults;
        }

        public DataSourceRouteResult GetDataSourceRouteResult()
        {
            return _dataSourceRouteResult;
        }

        public bool IsMergeQuery()
        {
            return _isCrossDataSource || _isCrossTable;
        }

        public bool IsCrossTable()
        {
            return _isCrossTable;
        }

        public bool IsCrossDataSource()
        {
            return _isCrossDataSource;
        }

        public bool IsEnumerableQuery()
        {
            return _isEnumerableQuery;
        }
    }
}