﻿using System;
using System.Linq;
using System.Linq.Expressions;
using ShardingCore.Exceptions;
using ShardingCore.Extensions;
using ShardingCore.Sharding.Abstractions;

namespace ShardingCore.Sharding.MergeEngines.Abstractions.InMemoryMerge.AbstractGenericMergeEngines
{
    /*
    * @Author: xjm
    * @Description:
    * @Date: 2021/8/19 8:29:05
    * @Ver: 1.0
    * @Email: 326308290@qq.com
    */
    public abstract class AbstractGenericMethodCallSelectorInMemoryAsyncMergeEngine<TEntity,TSelect>:AbstractGenericMethodCallInMemoryAsyncMergeEngine<TEntity>
    {
        public AbstractGenericMethodCallSelectorInMemoryAsyncMergeEngine(MethodCallExpression methodCallExpression, IShardingDbContext shardingDbContext) : base(methodCallExpression, shardingDbContext)
        {
        }

        public override IQueryable DoCombineQueryable<TResult>(IQueryable<TEntity> queryable)
        {
            var secondExpression = GetSecondExpression();
            if (secondExpression != null)
            {
                if (secondExpression is UnaryExpression unaryExpression && unaryExpression.Operand is LambdaExpression lambdaExpression && lambdaExpression is Expression<Func<TEntity, TSelect>> selector)
                {
                    return queryable.Select(selector);
                }

                throw new ShardingCoreException($"expression is not selector:{secondExpression.ShardingPrint()}");   
            }
            return queryable;
        }

        protected override IQueryable<TEntity> CombineQueryable(IQueryable<TEntity> queryable, Expression secondExpression)
        {
            return queryable;
        }

    }
}