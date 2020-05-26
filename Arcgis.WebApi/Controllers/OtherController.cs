using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arcgis.IService;
using Microsoft.AspNetCore.Mvc;
using Universal.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Arcgis.WebApi.Controllers
{
    [Route("arcgis/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class OtherController : Controller
    {
        private readonly IOtherService _otherService;
        public OtherController(IOtherService otherService)
        {
            _otherService = otherService;
        }
        /// <summary>
        /// 获取轮播列表
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        public IActionResult GetBannerList()
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _otherService.GetBannerList();
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "成功";
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
        /// 获取公告列表
        /// </summary>
        /// <remarks>
        /// 说明:
        /// istitle:1 头条公告  0不是
        /// </remarks>
        /// <returns></returns> 
        [HttpGet]
        public IActionResult GetNoticeList(int istitle)
        {
            var resultCountModel = new RespResultCountViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _otherService.GetNoticeList(istitle);
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "成功";
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
    }
}
