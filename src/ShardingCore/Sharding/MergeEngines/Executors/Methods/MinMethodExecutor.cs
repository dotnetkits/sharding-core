﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShardingCore.Exceptions;
using ShardingCore.Extensions;
using ShardingCore.Extensions.InternalExtensions;
using ShardingCore.Sharding.Abstractions.ParallelExecutors;
using ShardingCore.Sharding.MergeEngines.Executors.Methods.Abstractions;
using ShardingCore.Sharding.MergeEngines.ParallelControls.CircuitBreakers;

namespace ShardingCore.Sharding.MergeEngines.Executors.Methods
{
    /// <summary>
    /// 
    /// </summary>
    /// Author: xjm
    /// Created: 2022/5/7 11:13:57
    /// Email: 326308290@qq.com
    internal class MinMethodExecutor<TEntity> : AbstractMethodExecutor<TEntity>
    {
        public MinMethodExecutor(StreamMergeContext streamMergeContext) : base(streamMergeContext)
        {
        }

        public override ICircuitBreaker CreateCircuitBreaker()
        {
            return new AnyElementCircuitBreaker(GetStreamMergeContext());
        }

        protected override Task<TEntity> EFCoreQueryAsync(IQueryable queryable, CancellationToken cancellationToken = new CancellationToken())
        {
            var resultType = typeof(TEntity);
            if (!resultType.IsNullableType())
            {
                if (typeof(decimal) == resultType)
                {
                    return queryable.As<IQueryable<decimal>>().Select(o => (decimal?)o).MinAsync(cancellationToken).As<Task<TEntity>>();
                }
                if (typeof(float) == resultType)
                {
                    return queryable.As<IQueryable<float>>().Select(o => (float?)o).MinAsync(cancellationToken).As<Task<TEntity>>();
                }
                if (typeof(int) == resultType)
                {
                    return queryable.As<IQueryable<int>>().Select(o => (int?)o).MinAsync(cancellationToken).As<Task<TEntity>>();
                }
                if (typeof(long) == resultType)
                {
                    return queryable.As<IQueryable<long>>().Select(o => (long?)o).MinAsync(cancellationToken).As<Task<TEntity>>();
                }
                if (typeof(double) == resultType)
                {
                    return queryable.As<IQueryable<double>>().Select(o => (double?)o).MinAsync(cancellationToken).As<Task<TEntity>>();
                }

                throw new ShardingCoreException($"cant calc max value, type:[{resultType}]");
            }
            else
            {
                return queryable.As<IQueryable<TEntity>>().MinAsync(cancellationToken);
            }
        }
    }
}