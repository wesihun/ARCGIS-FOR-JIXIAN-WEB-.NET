using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Arcgis.Entities;
using Arcgis.Entities.Dto;
using Arcgis.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RestSharp;
using Universal.Models;
using Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Arcgis.WebApi.Controllers
{
    [Route("arcgis/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class OtherController : Controller
    {
        private readonly IOtherService _otherService;

        public IConfiguration Configuration { get; }
        public OtherController(IOtherService otherService, IConfiguration configuration)
        {
            _otherService = otherService;
            Configuration = configuration;
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
                //var dataResult = _otherService.GetBannerList().Select(
                //                        it => new BannerEntityDto
                //                        {
                //                            bannerdir = it.bannerdir,
                //                            remake = it.remake
                //                        }                        
                //                    ).ToList();
                //var dataResult = _otherService.GetBannerList()
                //                .Select(x => ItemToDTO(x))
                //                .ToList();
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
                    return NotFound();
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
        //private static BannerEntityDto ItemToDTO(BannerEntity it) =>
        //   new BannerEntityDto
        //   {
        //       bannerdir = it.bannerdir,
        //       remake = it.remake
        //   };
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
        /// <summary>
        /// 浏览记录调用接口
        /// </summary>
        /// <remarks>
        /// 说明:
        /// content  内容自定义   登录时传入content:登入系统  （退出系统）
        /// </remarks>
        /// <returns></returns> 
        [HttpPost]
        public IActionResult PostLog(int userid,string content)
        {
            var resultCountModel = new RespResultCountViewModel();
            try
            {
                var dataResult = _otherService.PostLog(userid,content);
                if (dataResult)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "成功";
                    resultCountModel.data = dataResult;
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
        /// 浏览记录列表显示
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns></returns> 
        [HttpGet]
        public IActionResult GetLog(int userid,int page, int limit)
        {
            var resultCountModel = new RespViewModel();
            int totalcount = 0;
            try
            {
                var dataResult = _otherService.GetLog(userid, page, limit, ref totalcount);
                var loginCount = _otherService.GetLoginCount(userid);
                if (dataResult != null)
                {
                    resultCountModel.code = 0;
                    resultCountModel.msg = "成功";
                    resultCountModel.data = dataResult;
                    resultCountModel.loginCount = loginCount;
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
        [HttpPost]
        public IActionResult Upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string text = string.Empty;
            var fileFolder = @"F:\resource";

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                    //               "111";
                    text = formFile.FileName.Split('.')[1];
                    var filePath = Path.Combine(fileFolder, formFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                        stream.Flush();
                    }
                }
            }
            return Ok(new {count = files.Count,size = size,text = text });
        }

        [HttpGet]
        public IActionResult Export1(string jsontree, string exclename)
        {
            try
            {
                if (string.IsNullOrEmpty(exclename))
                {
                    return Ok("exclename不能为空!");
                }
                var tempName = "temp";
                var rootPath = @"F://Temp";
                //var rootPath = @"http://192.168.0.105:8080//app";
                if (System.IO.Directory.Exists(rootPath) == false)
                    System.IO.Directory.CreateDirectory(rootPath);
                var newFile = rootPath + "//" + tempName + ".xls";
                if (System.IO.File.Exists(newFile))
                {
                    System.IO.File.Delete(newFile);
                }
                var client = new RestClient(Configuration["ConnectionStrings:JavaServerPath"].ToString());
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("getAnalysisData", Method.POST);

                request.AddParameter("Content-Type", "application/json;charset=utf-8");
                //  string jsonstr = "{\"id\":5,\"parentid\":4,\"menuename\":\"拆除未尽区\",\"type\":\"polygon\",\"createtime\":\"2020-06-17 00:00:00\",\"subAnalysisMenue\":[],\"tablename\":\"ccwjq\"}";
                //  string jsonstr = "{\"id\":2,\"parentid\":1,\"menuename\":\"权属分析\",\"type\":\"polygon\",\"createtime\":\"2020-06-17 00:00:00\",\"subAnalysisMenue\":[],\"tablename\":\"dltb\"}";
                byte[] AsciiByte = Encoding.ASCII.GetBytes(jsontree);
                byte[] Utif8Byte = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, AsciiByte);
                string Text = System.Text.Encoding.UTF8.GetString(Utif8Byte);
                request.AddParameter("jsonTree", Text); // adds to POST or URL querystring based on Method
                                                            //   request.AddParameter("jsonTree", jsonstr);
                                            // execute the request
                IRestResponse response = client.Execute(request);
                JObject jobj = (JObject)JsonConvert.DeserializeObject(response.Content);
                //JsonStr为Json字符串
                JArray array = JsonConvert.DeserializeObject(jobj["result"].ToString()) as JArray;//反序列化为数组
                if (array.Count > 0)
                {
                    using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                    {

                        IWorkbook workbook = new XSSFWorkbook();


                        var sheet = workbook.CreateSheet("Data");

                        var header = sheet.CreateRow(0);


                        JObject objColumns = array[0] as JObject;
                        int headerCount = 0;
                        //构造表头
                        foreach (JToken jkon in objColumns.AsJEnumerable<JToken>())
                        {

                            string name = ((JProperty)(jkon)).Name.ToUpper();
                            if (name == "AREA" || name == "OBJECTID" || name == "SHAPE")
                            {
                                continue;
                            }
                            KeyValueHelper.matchKey = ":";
                            KeyValueHelper.matchValue = ",";
                            Dictionary<object, object> conents = KeyValueHelper.GetConentByString(KeyValueHelper.data);
                            if (conents.ContainsKey(name))
                            {
                                name = conents[name.ToUpper()].ToString();
                            }
                            header.CreateCell(headerCount).SetCellValue(name);
                            headerCount++;
                        }
                        //向表中添加数据
                        for (int i = 0; i < array.Count; i++)
                        {
                            var datarow = sheet.CreateRow(i + 1);
                            JObject obj = array[i] as JObject;
                            int cellCount = 0;
                            foreach (JToken jkon in obj.AsJEnumerable<JToken>())
                            {
                                string Name = ((JProperty)(jkon)).Name.ToString().ToUpper();
                                if (Name == "AREA" || Name == "OBJECTID" || Name == "SHAPE")
                                {
                                    continue;
                                }
                                string value = ((JProperty)(jkon)).Value.ToString();
                                datarow.CreateCell(cellCount).SetCellValue(value);
                                cellCount++;
                            }
                        }
                        workbook.Write(fs);
                    }
                }
                else
                {
                    return BadRequest();
                }
                var memory = new MemoryStream();
                using (var stream = new FileStream(newFile, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
                if (System.IO.File.Exists(newFile))
                {
                    System.IO.File.Delete(newFile);
                }
                if (System.IO.Directory.Exists(rootPath) == true)
                    System.IO.Directory.Delete(rootPath);
                return File(memory, "application/vnd.ms-excel", exclename + ".xlsx");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message.ToString());
            }
        }

        [HttpGet]
        public IActionResult Export(string jsontree, string exclename)
        {
            try
            {
                if (string.IsNullOrEmpty(exclename))
                {
                    return Ok("exclename不能为空!");
                }
                var tempName = "temp";
                var rootPath = @"F://Temp";
                //var rootPath = @"http://192.168.0.105:8080//app";
                if (System.IO.Directory.Exists(rootPath) == false)
                    System.IO.Directory.CreateDirectory(rootPath);
                var newFile = rootPath + "//" + tempName + ".xls";
                if (System.IO.File.Exists(newFile))
                {
                    System.IO.File.Delete(newFile);
                }
                var client = new RestClient(Configuration["ConnectionStrings:JavaServerPath"].ToString());
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("getAnalysisData", Method.POST);

                request.AddParameter("Content-Type", "multipart/form-data; boundary=<calculated when request is sent>");
                //string jsonstr = "{\"id\":5,\"parentid\":4,\"menuename\":\"拆除未尽区\",\"type\":\"polygon\",\"createtime\":\"2020-06-17 00:00:00\",\"subAnalysisMenue\":[],\"tablename\":\"ccwjq\"}";
                //  string jsonstr = "{\"id\":2,\"parentid\":1,\"menuename\":\"权属分析\",\"type\":\"polygon\",\"createtime\":\"2020-06-17 00:00:00\",\"subAnalysisMenue\":[],\"tablename\":\"dltb\"}";
                request.AddParameter("jsonTree", jsontree); // adds to POST or URL querystring based on Method
                                                            //   request.AddParameter("jsonTree", jsonstr);
                                                            // execute the request
                IRestResponse response = client.Execute(request);
                JObject jobj = (JObject)JsonConvert.DeserializeObject(response.Content);
                //JsonStr为Json字符串
                JArray array = JsonConvert.DeserializeObject(jobj["result"].ToString()) as JArray;//反序列化为数组
                if (array.Count > 0)
                {
                    using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                    {

                        IWorkbook workbook = new XSSFWorkbook();


                        var sheet = workbook.CreateSheet("Data");

                        var header = sheet.CreateRow(0);


                        JObject objColumns = array[0] as JObject;
                        int headerCount = 0;
                        //构造表头
                        foreach (JToken jkon in objColumns.AsJEnumerable<JToken>())
                        {

                            string name = ((JProperty)(jkon)).Name.ToUpper();
                            if (name == "AREA" || name == "OBJECTID" || name == "SHAPE")
                            {
                                continue;
                            }
                            KeyValueHelper.matchKey = ":";
                            KeyValueHelper.matchValue = ",";
                            Dictionary<object, object> conents = KeyValueHelper.GetConentByString(KeyValueHelper.data);
                            if (conents.ContainsKey(name))
                            {
                                name = conents[name.ToUpper()].ToString();
                            }
                            header.CreateCell(headerCount).SetCellValue(name);
                            headerCount++;
                        }
                        //向表中添加数据
                        for (int i = 0; i < array.Count; i++)
                        {
                            var datarow = sheet.CreateRow(i + 1);
                            JObject obj = array[i] as JObject;
                            int cellCount = 0;
                            foreach (JToken jkon in obj.AsJEnumerable<JToken>())
                            {
                                string Name = ((JProperty)(jkon)).Name.ToString().ToUpper();
                                if (Name == "AREA" || Name == "OBJECTID" || Name == "SHAPE")
                                {
                                    continue;
                                }
                                string value = ((JProperty)(jkon)).Value.ToString();
                                datarow.CreateCell(cellCount).SetCellValue(value);
                                cellCount++;
                            }
                        }
                        workbook.Write(fs);
                    }
                }
                else
                {
                    return BadRequest();
                }
                var memory = new MemoryStream();
                using (var stream = new FileStream(newFile, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
                if (System.IO.File.Exists(newFile))
                {
                    System.IO.File.Delete(newFile);
                }
                if (System.IO.Directory.Exists(rootPath) == true)
                    System.IO.Directory.Delete(rootPath);
                return File(memory, "application/vnd.ms-excel", exclename + "11.xlsx");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message.ToString());
            }
        }


    }
}
