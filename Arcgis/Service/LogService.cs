using Arcgis.Entities;
using Arcgis.IService;
using DataNs.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Service
{
    public class LogService : IlogService
    {
        private bool result = false;
        private readonly IDbContext _dbContext;
        public LogService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Create(LogEntity entity)
        {
            using (var db = _dbContext.GetIntance())
            {
                var count = db.Insertable(entity).ExecuteCommand();
                result = count > 0 ? true : false;
            }
            return result;
        }
    }
}
