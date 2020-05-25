﻿using Arcgis.Entities;
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
                       name = de2.username,
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
                var model = new ApplyEntity()
                {
                    reson = reson,
                    state = states
                };
                var count = db.Updateable(model)
                .UpdateColumns(it => new { it.reson, it.state })
                .Where(it => it.applyid == applyid)
                .ExecuteCommand();
                result = count > 0 ? true : false;
            }
            return result;
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
