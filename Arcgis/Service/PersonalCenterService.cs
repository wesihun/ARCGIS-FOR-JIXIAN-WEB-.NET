using Arcgis.Entities;
using Arcgis.Entities.Dto;
using Arcgis.IService;
using DataNs.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcgis.Service
{
    public class PersonalCenterService : IPersonalCenterService
    {
        private bool result = false;
        private readonly IDbContext _dbContext;
        public PersonalCenterService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ManagePersonalCenter> GetManageList(int states, int pageIndex, int pageSize, ref int totalCount)
        {
            var DataResult = new List<ManagePersonalCenter>();
            using (var db = _dbContext.GetIntance())
            {
                DataResult = db.Queryable<ApplyEntity, UserEntity,PostEntity>((de1, de2,de3) => new object[] {
                    JoinType.Left,de1.userid == de2.userid,
                    JoinType.Left,de2.postid == de3.postid
                    }).Where((de1, de2, de3) => de2.state == 1)
                    .WhereIF(states != 0 && states == 1,(de1, de2, de3) => de1.state == 1 || de1.state == 2)
                    .WhereIF(states != 0 && states != 1, (de1, de2, de3) => de1.state == states)
                    .Select((de1, de2, de3) => new ManagePersonalCenter
                    {
                       applyid = de1.applyid,
                       name = de2.realname,
                       depname = de1.depname,
                       postname = de3.postname,
                       phone = de2.telephone,
                       applytime = de1.createtime,
                       states = de1.state,
                       applyreason = de1.applyreason
                    }).ToPageList(pageIndex, pageSize, ref totalCount);
            }
            return DataResult;
        }
        public List<PersonalCenter> GetPersonList(int userid, int states, int pageIndex, int pageSize, ref int totalCount)
        {
            var DataResult = new List<PersonalCenter>();
            using (var db = _dbContext.GetIntance())
            {
                DataResult = db.Queryable<ApplyEntity, ResourceEntity>((de1, de2) => new object[] {
                    JoinType.Left,de1.resourceid == de2.resourceid
                    }).Where((de1, de2) => de1.userid == userid)
                    .WhereIF(states != 0 && states == 1, (de1, de2) => de1.state == 1 || de1.state == 2)
                    .WhereIF(states != 0 && states != 1, (de1, de2) => de1.state == states)
                     .Select((de1, de2) => new PersonalCenter
                     {
                         applytime = de1.createtime,
                         reson = de1.reson,
                         resourcename = de2.resourcename
                     }).ToPageList(pageIndex, pageSize, ref totalCount);
            }
            return DataResult;
        }

        public bool Examine(int applyid, string reson, int states)
        {
            using (var db = _dbContext.GetIntance())
            {
                try
                {
                    db.Ado.BeginTran();
                    var model = new ApplyEntity()
                    {
                        reson = reson,
                        state = states
                    };
                    db.Updateable(model)
                    .UpdateColumns(it => new { it.reson, it.state })
                    .Where(it => it.applyid == applyid)
                    .ExecuteCommand();
                    int userid = db.Queryable<ApplyEntity>().Where(it => it.applyid == applyid).First().userid;
                    LogEntity logEntity = new LogEntity()
                    {
                        userid = userid,
                        createtime = DateTime.Now,
                        logtitle = "审核"
                    };
                    switch (states)
                    {
                        case 1:
                            logEntity.logcontent = "通过";
                            break;
                        case -1:
                            logEntity.logcontent = "退回";
                            break;
                    }
                    db.Insertable(logEntity).ExecuteCommand();
                    db.Ado.CommitTran();
                }
                catch (Exception ex)
                {
                    string error = ex.Message.ToString();
                    db.Ado.RollbackTran();
                    return false;
                }
                return true;
            }
        }
        public bool Download(int applyid)
        {
            using (var db = _dbContext.GetIntance())
            {
                try
                {
                    db.Ado.BeginTran();
                    var model = new ApplyEntity()
                    {
                        state = 2
                    };
                    db.Updateable(model)
                    .UpdateColumns(it => new { it.state })
                    .Where(it => it.applyid == applyid)
                    .ExecuteCommand();
                    var userid = db.Queryable<ApplyEntity>().Where(it => it.applyid == applyid).First().userid;
                    LogEntity logEntity = new LogEntity()
                    {
                        userid = userid,
                        createtime = DateTime.Now,
                        logtitle = "下载",
                        logcontent = "已下载"
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
        /// <summary>
        /// states:1 为取消审核传入   2 为删除传入
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public bool Operation(int applyid, int states)
        {
            using (var db = _dbContext.GetIntance())
            {
                try
                {
                    db.Ado.BeginTran();
                    var userid = db.Queryable<ApplyEntity>().Where(it => it.applyid == applyid).First().userid;
                    if (states == 1)
                    {

                        var model = new ApplyEntity()
                        {
                            state = 0
                        };
                        db.Updateable(model)
                           .UpdateColumns(it => new { it.state })
                           .Where(it => it.applyid == applyid)
                           .ExecuteCommand();
                        LogEntity logEntity = new LogEntity()
                        {
                            userid = userid,
                            createtime = DateTime.Now,
                            logtitle = "审核管理员操作",
                            logcontent = "取消审核 该applyid为" + applyid
                        };
                        db.Insertable(logEntity).ExecuteCommand();
                    }
                    else
                    {
                        db.Deleteable<ApplyEntity>().Where(it => it.applyid == applyid).ExecuteCommand();
                        LogEntity logEntity = new LogEntity()
                        {
                            userid = userid,
                            createtime = DateTime.Now,
                            logtitle = "审核管理员操作",
                            logcontent = "删除 该applyid为" + applyid
                        };
                        db.Insertable(logEntity).ExecuteCommand();
                    }
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
        /// <summary>
        /// states:1 为取消审核传入   2 为删除传入
        /// </summary>
        /// <param name="applyids"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public bool OperationBatch(List<int> applyids, int states)
        {
            using (var db = _dbContext.GetIntance())
            {
                try
                {
                    db.Ado.BeginTran();
                    foreach (var id in applyids)
                    {
                        var userid = db.Queryable<ApplyEntity>().Where(it => it.applyid == id).First().userid;
                        if (states == 1)
                        {
                            var model = new ApplyEntity()
                            {
                                state = 0
                            };
                            db.Updateable(model)
                               .UpdateColumns(it => new { it.state })
                               .Where(it => it.applyid == id)
                               .ExecuteCommand();
                            LogEntity logEntity = new LogEntity()
                            {
                                userid = userid,
                                createtime = DateTime.Now,
                                logtitle = "审核管理员操作",
                                logcontent = "取消审核 该applyid为" + id
                            };
                            db.Insertable(logEntity).ExecuteCommand();
                        }
                        else
                        {
                            db.Deleteable<ApplyEntity>().Where(it => it.applyid == id).ExecuteCommand();
                            LogEntity logEntity = new LogEntity()
                            {
                                userid = userid,
                                createtime = DateTime.Now,
                                logtitle = "审核管理员操作",
                                logcontent = "删除 该applyid为" + id
                            };
                            db.Insertable(logEntity).ExecuteCommand();
                        }
                    }
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

        public int GetNoticeCount(int userid)
        {
            int count = 0;
            using (var db = _dbContext.GetIntance())
            {
                count = db.Queryable<ApplyEntity>()
                    .Where(it => it.userid == userid)
                    .Where(it => it.state == 1 || it.state == 2)
                    .ToList().Count();
            }
            return count;
        }
    }
}
