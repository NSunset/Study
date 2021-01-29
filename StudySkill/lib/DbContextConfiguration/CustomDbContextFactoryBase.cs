using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.DbContextConfiguration
{
    public class CustomDbContextFactoryBase<TDbContext> : ICustomDbContextFactory where TDbContext : DbContext
    {
        protected readonly ConcurrentDictionary<string, DbContext> _dbContexts = new ConcurrentDictionary<string, DbContext>();

        protected readonly IDbConnection _dbConnection;
        protected readonly IDbContextOptions _dbContextOptions;
        protected readonly IServiceProvider _serviceProvider;
        protected static int _iSeed = 0;
        protected static bool _isSet = true;
        protected static readonly object _ObjectisSet_Lock = new object();

        public CustomDbContextFactoryBase(IDbConnection dbConnection, IDbContextOptions dbContextOptions, IServiceProvider serviceProvider)
        {
            _dbConnection = dbConnection;
            _dbContextOptions = dbContextOptions;
            _serviceProvider = serviceProvider;
            if (_isSet)
            {
                lock (_ObjectisSet_Lock)
                {
                    if (_isSet)
                    {
                        _iSeed = _dbConnection.ReadConnectionStr.Length; //应该保证  只有在CustomConnectionFactory 第一次初始化的时候，对其赋值；
                        _isSet = false;
                    }
                }
            }
        }

        public virtual DbContext GetDbContext(WriteAndRead writeAndRead)
        {
            var conStr = GetConnectionStr(writeAndRead);
            if (_dbContexts.TryGetValue(conStr, out DbContext dbContext))
            {
                return dbContext;
            }
            else
            {
                DbContextOptions dbContextOptions = _dbContextOptions.Configure<TDbContext>(_dbConnection.DbContextOptionsBuilder, conStr);
                var constructorMethod = typeof(TDbContext).GetConstructors()
                        .Where(c => c.IsPublic && !c.IsAbstract && !c.IsStatic)
                        .OrderByDescending(c => c.GetParameters().Length)
                        .FirstOrDefault();
                if (constructorMethod == null)
                {
                    throw new Exception("无法获取有效的上下文构造器");
                }
                var paramTypes = constructorMethod.GetParameters();
                var argumentExpressions = new object[paramTypes.Length];
                for (int i = 0; i < paramTypes.Length; i++)
                {
                    var pType = paramTypes[i].ParameterType;
                    if (pType == typeof(DbContextOptions) || (pType.IsGenericType && pType.GetGenericTypeDefinition() == typeof(DbContextOptions<>)))
                    {
                        argumentExpressions[i] = dbContextOptions;
                    }
                    else
                    {
                        var service = _serviceProvider.GetService(pType);
                        if (service == null)
                            throw new Exception($"{nameof(TDbContext)}构造函数参数{pType.Name}不在容器中");
                        argumentExpressions[i] = service;
                    }
                }
                var obj = constructorMethod.Invoke(argumentExpressions) as TDbContext;
                _dbContexts.TryAdd(conStr, obj);
                return obj;
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="writeAndRead"></param>
        /// <returns></returns>
        protected virtual string GetConnectionStr(WriteAndRead writeAndRead)
        {
            string connectionStr = null;
            switch (writeAndRead)
            {
                case WriteAndRead.Write:
                    connectionStr = _dbConnection.WriteConnectionStr; //增删改
                    break;
                case WriteAndRead.Read:
                    connectionStr = QuyerStrategy();
                    break;
                default:
                    break;
            }
            return connectionStr;
        }

        /// <summary>
        /// 选择策略
        /// </summary>
        /// <returns></returns>
        protected virtual string QuyerStrategy()
        {
            switch (_dbConnection.Strategy)
            {
                case Strategy.Polling:
                    return Polling();
                case Strategy.Random:
                    return Random();
                default:
                    throw new Exception("分库查询策略不存在。。。");
            }
        }

        /// <summary>
        /// 随机策略
        /// </summary>
        /// <returns></returns>
        protected virtual string Random()
        {
            int count = _dbConnection.ReadConnectionStr.Length;
            int index = new Random().Next(0, count);
            return _dbConnection.ReadConnectionStr[index];
        }

        /// <summary>
        /// 轮询
        /// </summary>
        /// <returns></returns>
        protected virtual string Polling()
        {
            return this._dbConnection.ReadConnectionStr[_iSeed++ % this._dbConnection.ReadConnectionStr.Length];//轮询;   
        }

        public virtual void Dispose()
        {
            if (_dbContexts != null && _dbContexts.Count > 0)
            {
                foreach (var dbContext in _dbContexts.Values)
                    dbContext.Dispose();
                _dbContexts.Clear();
            }
        }
    }
}
