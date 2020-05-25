using Arcgis.Entities;
using Arcgis.IService;
using DataNs.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Service
{
    public class SpecialInvestigationService : ISpecialInvestigationService
    {
        private bool result = false;
        private readonly IDbContext _dbContext;
        public SpecialInvestigationService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ResourceEntity> GetPageListByCondition(int typeid,int pageIndex, int pageSize, ref int totalCount)
        {
            var DataResult = new List<ResourceEntity>();
            using (var db = _dbContext.GetIntance())
            {
                DataResult = db.Queryable<ResourceEntity, ResourceTypeEntity>((de1, de2) => new object[] {
                    JoinType.Left,de1.resourcetypeid == de2.resourcetypeid
                    }).WhereIF(typeid != 0, (de1, de2) => de1.resourcetypeid == typeid)
                    .Select((de1, de2) => new ResourceEntity
                    {
                       resourceid = de1.resourceid,
                       resourcetypename = de2.resourcetype,
                       resourcename = de1.resourcename,
                       resourcedir = de1.resourcedir,
                       resourcetypeid = de1.resourcetypeid,
                       sender = de1.sender,
                       createtime = de1.createtime,
                       url = de1.url,
                       filesize = de1.filesize,
                       encryption = de1.encryption
                    }).ToPageList(pageIndex, pageSize, ref totalCount);
            }
            return DataResult;
        }
        public List<DepmentEntity> GetDepList()
        {
            var DataResult = new List<DepmentEntity>();
            using (var db = _dbContext.GetIntance())
            {
                DataResult = db.Queryable<DepmentEntity>().ToList();
            }
            return DataResult;
        }
        public List<ResourceTypeEntity> GetResourceTypeList(int type)
        {
            var DataResult = new List<ResourceTypeEntity>();
            using (var db = _dbContext.GetIntance())
            {
                DataResult = db.Queryable<ResourceTypeEntity>().Where(it => it.type == type).OrderBy(it => it.SortCode).ToList();
            }
            return DataResult;
        }
        #region 提交数据
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool InsertApply(ApplyEntity entity)
        {
            using (var db = _dbContext.GetIntance())
            {
                var count = db.Insertable(entity).ExecuteCommand();
                result = count > 0 ? true : false;
            }
            return result;
        }
        #endregion
    }
}
