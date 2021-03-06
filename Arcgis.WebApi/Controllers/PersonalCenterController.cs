﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Arcgis.Entities;
using Arcgis.IService;
using DataNs.SqlSugar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Universal.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Arcgis.WebApi.Controllers
{
    [Route("arcgis/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class PersonalCenterController : Controller
    {
        private readonly IPersonalCenterService _personalcenterService;
        public PersonalCenterController(IPersonalCenterService personalcenterService)
        {
            _personalcenterService = personalcenterService;
        }
        /// <summary>
        /// 获取审核管理员个人中心审核管理列表
        /// </summary>
        /// <remarks>
        /// 说明:
        /// states:0审核中  1已通过   -1退回  传空为所有
        /// </remarks>
        /// <param name="states">状态</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页行数</param>
        /// <returns></returns>  
        [HttpGet]
        public IActionResult GetManageList(string states, int page, int limit)
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _personalcenterService.GetManageList(states, page, limit, ref totalcount);
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "查询成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.count = totalcount;
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "没有检索到数据";
                }
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }
        /// <summary>
        /// 获取个人资料管理列表
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="userid">userid</param>
        /// <returns></returns>  
        [HttpGet]
        public IActionResult GetPerInfo(int userid)
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _personalcenterService.GetPerInfo(userid);
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "查询成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.count = totalcount;
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "没有检索到数据";
                }
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }
        /// <summary>
        /// 提交个人信息
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model">model</param>
        /// <returns></returns>  
        [HttpPost]
        public IActionResult PostPerInfo(UserEntity model)
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                if (model.userid == 0)
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "缺少主键";
                    return Ok(resultCountModel);
                }
                var dataResult = _personalcenterService.PostPerInfo(model);
                if (dataResult)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "修改成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.count = totalcount;
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "没有检索到数据";
                }
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }
        /// <summary>
        /// 获取乡镇下拉
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>  
        [HttpGet]
        public IActionResult GetAreaInfo1()
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _personalcenterService.GetAreaInfo1();
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "查询成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.count = totalcount;
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "没有检索到数据";
                }
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }
        /// <summary>
        /// 获取村级下拉
        /// </summary>
        /// <remarks>
        /// id为下拉获取的id
        /// </remarks>
        /// <returns></returns>  
        [HttpGet]
        public IActionResult GetAreaInfo2(int id)
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _personalcenterService.GetAreaInfo2(id);
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "查询成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.count = totalcount;
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "没有检索到数据";
                }
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }

        /// <summary>
        /// 获取岗位下拉
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>  
        [HttpGet]
        public IActionResult GetPostList()
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _personalcenterService.GetPostList();
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "查询成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.count = totalcount;
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "没有检索到数据";
                }
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }
        /// <summary>
        /// 获取部门下拉
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>  
        [HttpGet]
        public IActionResult GetDepList()
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _personalcenterService.GetDepList();
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "查询成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.count = totalcount;
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "没有检索到数据";
                }
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }
        /// <summary>
        /// 个人中心获取下载管理列表
        /// </summary>
        /// <remarks>
        /// 说明:
        /// states:1已通过  2已下载  -1退回
        /// </remarks>
        /// <param name="userid">用户id</param>
        /// <param name="states">状态</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页行数</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPersonList(int userid,int states, int page, int limit)
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                if (userid == 0)
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "存在必填项!";
                    return Ok(resultCountModel);
                }
                var dataResult = _personalcenterService.GetPersonList(userid,states, page, limit, ref totalcount);
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "查询成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.count = totalcount;
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "没有检索到数据";
                }
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }
        /// <summary>
        /// 获取消息通知
        /// </summary>
        /// <remarks>
        /// 说明:
        /// 消息通知包括 已通过  已退回   暂定只显示条数
        /// </remarks>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNoticeCount(int userid)
        {
            var resultCountModel = new RespResultCountViewModel();
            try
            {
                if (userid == 0)
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "存在必填项!";
                    return Ok(resultCountModel);
                }
                var dataResult = _personalcenterService.GetNoticeCount(userid);
                resultCountModel.code = 0;
                resultCountModel.count = dataResult;
                resultCountModel.msg = "成功";
                return Ok(resultCountModel);
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }
        /// <summary>
        /// 审核管理员审核提交
        /// </summary>
        /// <remarks>
        /// 说明:
        /// states:1已通过 -1退回
        /// </remarks>
        /// <param name="applyid">申请id主键</param>
        /// <param name="reson">说明</param>
        /// <param name="states">状态</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Examine(int applyid,string reson,int states)
        {
            RespResultViewModel resultModel = new RespResultCountViewModel();
            try
            {
                #region 验证
                if (applyid == 0)
                {
                    resultModel.code = -1;
                    resultModel.msg = "请传入主键!";
                    return Ok(resultModel);
                }
                if (states == 0 || string.IsNullOrEmpty(reson))
                {
                    resultModel.code = -1;
                    resultModel.msg = "存在必填项!";
                    return Ok(resultModel);
                }
                #endregion

                bool result = _personalcenterService.Examine(applyid,reson,states);
                if (result)
                {
                    resultModel.code = 0;
                    resultModel.msg = "成功";
                }
                else
                {
                    resultModel.code = -1;
                    resultModel.msg = "失败";
                }
                return Ok(resultModel);
            }
            catch (Exception ex)
            {
                resultModel.code = -1;
                resultModel.msg = "操作失败" + ex.ToString();
                return Ok(resultModel);
            }
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <remarks>
        /// 说明:
        /// 用户下载时调用
        /// </remarks>
        /// <param name="applyid">申请主键</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Download(int applyid)
        {
            var resultCountModel = new RespResultCountViewModel();
            try
            {
                if (applyid == 0)
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "存在必填项!";
                    return Ok(resultCountModel);
                }
                string resultPath = DbContext.UploadPath + "/" + _personalcenterService.Download(applyid,"1");              
                if (!string.IsNullOrEmpty(resultPath))
                {
                    //var filepath = @"F:\resource\images\1.jpg";
                    var provider = new FileExtensionContentTypeProvider();
                    FileInfo fileInfo = new FileInfo(resultPath);
                    new FileExtensionContentTypeProvider().Mappings.TryGetValue(fileInfo.Extension, out var contenttype);
                    return File(System.IO.File.ReadAllBytes(resultPath), contenttype ?? "application/octet-stream", _personalcenterService.Download(applyid, "2")+ fileInfo.Extension);
                }
                else
                {
                    resultCountModel.code = -1;
                    resultCountModel.msg = "失败";
                    return Ok(resultCountModel);
                }
            }
            catch (Exception ex)
            {
                resultCountModel.code = -1;
                resultCountModel.msg = "操作失败！原因：" + ex.Message;
                return Ok(resultCountModel);
            }
        }        
    }
}
