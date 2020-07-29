using Arcgis.Entities;
using Arcgis.IService;
using DataNs.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Universal.Models;
using Utilities;

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
        public List<ResourceEntity> GetPageListByCondition(string name, int typeid,int pageIndex, int pageSize, ref int totalCount)
        {
            var DataResult = new List<ResourceEntity>();
            using (var db = _dbContext.GetIntance())
            {
                List<int> typeids = new List<int>();
                var list = db.Queryable<ResourceTypeEntity>().Where(it => it.parentid == typeid).ToList();
                if (list.Count() != 0)
                {
                    typeids = list.Select(it => it.resourcetypeid).ToList();
                }
                else
                {
                    typeids.Add(typeid);
                }
                DataResult = db.Queryable<ResourceEntity, ResourceTypeEntity>((de1, de2) => new object[] {
                    JoinType.Left,de1.resourcetypeid == de2.resourcetypeid
                    })
                    //树节点获取下面的所有数据
                    .WhereIF(typeid != 1 && typeid !=0, (de1, de2) => typeids.Contains(de1.resourcetypeid))
                    .WhereIF(!string.IsNullOrEmpty(name), (de1, de2) => de1.resourcename.Contains(name))
                    .Select((de1, de2) => new ResourceEntity
                    {
                       resourceid = de1.resourceid,
                       resourcetypename = de2.resourcetype,
                       resourcename = de1.resourcename,
                       resourcedir = de1.resourcedir,
                       resourcetypeid = de1.resourcetypeid,
                       sender = de1.sender,
                       createtime = de1.createtime,
                       url = de1.resourcedir,
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
        public List<TreeModel> GetResourceTypeList()
        {
            var DataResult = new List<TreeModel>();
            using (var db = _dbContext.GetIntance())
            {
                var data = db.Queryable<ResourceTypeEntity>().ToList();
                DataResult = Tree.CreateTree(
                    data.Where(it => it.parentid == 0).Select(x => new TreeModel { menueid = x.resourcetypeid, menuename = x.resourcetype, parentmenueid = x.parentid }).ToList(),
                    data.Where(it => it.parentid != 0).Select(x => new TreeModel { menueid = x.resourcetypeid, menuename = x.resourcetype, parentmenueid = x.parentid }).ToList()
                    );               
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
                try
                {
                    db.Ado.BeginTran();
                    db.Insertable(entity).IgnoreColumns(it => it.applyid).ExecuteCommand();
                    LogEntity logEntity = new LogEntity()
                    {
                        userid = entity.userid,
                        createtime = DateTime.Now,
                        logtitle = "新增",
                        logcontent = "提交下载申请"
                    };
                    db.Insertable(logEntity).ExecuteCommand();
                    db.Ado.CommitTran();
                }
                catch (Exception)
                {
                    db.Ado.RollbackTran();
                    return false;
                }
                return true;
            }
        }
        #endregion
    }
}
