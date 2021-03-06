﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApiClient.Contexts;

namespace WebApiClient.Attributes
{
    /// <summary>
    /// 表示使用缓存的特性
    /// 缓存功能依赖于HttpApiConfig.ResponseCacheProvider
    /// </summary>
    [DebuggerDisplay("Expiration = {Expiration}")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class ApiActionCacheAttribute : Attribute, IApiActionCachePolicyAttribute
    {
        /// <summary>
        /// 获取缓存的时间戳
        /// </summary>
        public TimeSpan Expiration { get; }

        /// <summary>
        /// 使用缓存的特性
        /// </summary>
        /// <param name="expiration">缓存毫秒数</param>
        public ApiActionCacheAttribute(double expiration)
        {
            this.Expiration = TimeSpan.FromMilliseconds(expiration);
        }


        /// <summary>
        /// 返回读取缓存的策略
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public virtual CachePolicy GetReadPolicy(ApiActionContext context)
        {
            return CachePolicy.Include;
        }

        /// <summary>
        /// 返回写入缓存的策略
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public virtual CachePolicy GetWritePolicy(ApiActionContext context)
        {
            return CachePolicy.Include;
        }

        /// <summary>
        /// 返回缓存的键
        /// 该键用于读取或写入缓存到缓存提供者
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public abstract Task<string> GetCacheKeyAsync(ApiActionContext context);
    }
}
