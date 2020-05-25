using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arcgis.Entities;
using Arcgis.IService;
using Microsoft.AspNetCore.Mvc;
using Universal.Models;

namespace Arcgis.WebApi.Controllers
{
    [Route("arcgis/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class SpecialInvestigationController : ControllerBase
    {
        private readonly ISpecialInvestigationService _specailService;
        public SpecialInvestigationController(ISpecialInvestigationService specailService)
        {
            _specailService = specailService;
        }
        #region 获取数据  
        /// <summary>
        /// 获取专项调查列表并分页
        /// </summary>
        /// <remarks>
        /// 说明:
        /// typeid:点击左侧专项类别获取的资源类型编号
        /// </remarks>
        /// <param name="typeid">资源类型编号</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页行数</param>
        /// <returns></returns>  
        [HttpGet]
        public IActionResult GetPageListByCondition(int typeid,int page, int limit)
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _specailService.GetPageListByCondition(typeid,page, limit, ref totalcount);
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
        /// 获取部门列表
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        public IActionResult GetDepList()
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _specailService.GetDepList(); 
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
        /// 获取专项调查左侧类型列表
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        public IActionResult GetResourceTypeList()
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _specailService.GetResourceTypeList(1);
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
        #endregion

        #region 提交数据

        /// <summary>
        /// 专项调查提交申请操作
        /// </summary>
        /// <remarks>
        /// 说明:
        /// model:参数传json字符串   key值为resourceid userid  username depid depname  applyreason（申请用途）
        /// </remarks>
        /// <returns></returns> 
        [HttpPost]
        public ActionResult CreateApply(ApplyEntity model)
        {
            RespResultViewModel resultModel = new RespResultCountViewModel();
            try
            {
                #region 验证
                if (model.userid == 0 || string.IsNullOrEmpty(model.username) || model.depid  ==0)
                {
                    resultModel.code = -1;
                    resultModel.msg = "存在必填项!";
                    return Ok(resultModel);
                }
                #endregion
                model.createtime = DateTime.Now;
                model.state = 0;
                model.type = 1;
                bool result = _specailService.InsertApply(model);
                if (result)
                {
                    resultModel.code = 0;
                    resultModel.msg = "新增成功";
                }
                else
                {
                    resultModel.code = -1;
                    resultModel.msg = "新增失败";
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
        #endregion
    }
}
