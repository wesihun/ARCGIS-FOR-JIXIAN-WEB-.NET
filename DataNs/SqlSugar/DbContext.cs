﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using SqlSugar;
using System.Diagnostics;

namespace DataNs.SqlSugar
{
    public class DbContext: IDbContext
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string DefaultDbConnectionString { get; set; }
        public static string DefaultDbConnectionStringForYb { get; set; }
        public static DbType _dbType { get; set; }
        public static string _dataBaseType { get; set; }
        //文件上传路径
        public static string UploadPath { get; set; }

        /// <summary>
        /// 获得SqlSugarClient(使用该方法, 默认请手动释放资源, 如using(var db = SugarBase.GetIntance()){你的代码}, 如果把isAutoCloseConnection参数设置为true, 则无需手动释放, 会每次操作数据库释放一次, 可能会影响性能, 请自行判断使用)
        /// </summary>
        /// <param name="commandTimeOut">等待超时时间, 默认为30秒 (单位: 秒)</param>
        /// <param name="dbType">数据库类型, 默认为SQL Server</param>
        /// <param name="isAutoCloseConnection">是否自动关闭数据库连接, 默认不是, 如果设置为true, 则会在每次操作完数据库后, 即时关闭, 如果一个方法里面多次操作了数据库, 建议保持为false, 否则可能会引发性能问题</param>
        /// <returns></returns>
        public SqlSugarClient GetIntance(int commandTimeOut = 60000, bool isAutoCloseConnection = false)
        {
            return InitDB(commandTimeOut, isAutoCloseConnection, DefaultDbConnectionString);
        }
        public SqlSugarClient GetIntanceForYb(int commandTimeOut = 60000, bool isAutoCloseConnection = false)
        {
            return InitDB(commandTimeOut, isAutoCloseConnection, DefaultDbConnectionStringForYb);
        }
        /// <summary>
        /// 初始化ORM连接对象
        /// </summary>
        /// <param name="commandTimeOut">等待超时时间, 默认为30秒 (单位: 秒)</param>
        /// <param name="dbType">数据库类型, 默认为SQL Server</param>
        /// <param name="isAutoCloseConnection">是否自动关闭数据库连接, 默认不是, 如果设置为true, 则会在每次操作完数据库后, 即时关闭, 如果一个方法里面多次操作了数据库, 建议保持为false, 否则可能会引发性能问题</param>
        /// <param name="dbConnectionString">数据库连接串</param>
        private SqlSugarClient InitDB(int commandTimeOut = 60000, bool isAutoCloseConnection = false, string dbConnectionString = "")
        {
            switch (_dataBaseType)
            {
                case "SqlServer":
                    _dbType = DbType.SqlServer;
                    break;
                case "MySql":
                    _dbType = DbType.MySql;
                    break;
                default:
                    _dbType = DbType.Oracle;
                    break;
            }
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = dbConnectionString,
                DbType = _dbType,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = isAutoCloseConnection

            });
            db.Ado.CommandTimeOut = commandTimeOut;
#if DEBUG
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Debug.WriteLine("=======================================");
                Debug.WriteLine(sql);
                Debug.WriteLine("=======================================");
            };
#endif
            return db;
        }
    }
}
