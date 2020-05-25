using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataNs.SqlSugar
{
    public interface IDbContext
    {
        SqlSugarClient GetIntance(int commandTimeOut = 6000, bool isAutoCloseConnection = false);
        SqlSugarClient GetIntanceForYb(int commandTimeOut = 6000, bool isAutoCloseConnection = false);
    }
}
