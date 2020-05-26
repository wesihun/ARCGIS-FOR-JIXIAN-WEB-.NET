using Arcgis.Entities;
using Arcgis.IService;
using DataNs.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Service
{
    public class OtherService: IOtherService
    {
        private bool result = false;
        private readonly IDbContext _dbContext;
        public OtherService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BannerEntity> GetBannerList()
        {
            var DataResult = new List<BannerEntity>();
            using (var db = _dbContext.GetIntance())
            {
                DataResult = db.Queryable<BannerEntity>().Where(it => it.state == 1)
                    .OrderBy(it => it.createtime,SqlSugar.OrderByType.Desc).ToList();
            }
            return DataResult;
        }
        public List<NoticeEntity> GetNoticeList(int istitle)
        {
            var DataResult = new List<NoticeEntity>();
            using (var db = _dbContext.GetIntance())
            {
                DataResult = db.Queryable<NoticeEntity>().Where(it => it.istitle == istitle)
                    .OrderBy(it => it.createtime, SqlSugar.OrderByType.Desc).ToList();
            }
            return DataResult;
        }
    }
}
