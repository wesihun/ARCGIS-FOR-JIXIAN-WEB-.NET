using Arcgis.Entities;
using Arcgis.IService;
using DataNs.SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool PostLog(int userid, string content)
        {
            using (var db = _dbContext.GetIntance())
            {
                LogEntity logEntity = new LogEntity()
                {
                    userid = userid,
                    createtime = DateTime.Now,
                    logtitle = "浏览记录",
                    logcontent = content
                };
                return db.Insertable(logEntity).ExecuteCommand()>0?true:false;
            }
        }
        public List<LogEntity> GetLog(int userid, int pageIndex, int pageSize, ref int totalCount)
        {
            var DataResult = new List<LogEntity>();
            using (var db = _dbContext.GetIntance())
            {
                DataResult = db.Queryable<LogEntity>()
                    .Where(it => it.logtitle == "浏览记录" && it.userid == userid && it.createtime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                    .ToPageList(pageIndex, pageSize, ref totalCount);

            }
            return DataResult;
        }
        public int GetLoginCount(int userid)
        {
            int Count = 0;
            using (var db = _dbContext.GetIntance())
            {
                Count = db.Queryable<LogEntity>()
                    .Where(it => it.logtitle == "浏览记录" && it.logcontent == "登入系统" && it.userid == userid && it.createtime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                    .ToList().Count();

            }
            return Count;
        }
    }
}
