using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 资源
    /// </summary>
    [SugarTable("tb_resource")]
    public class ResourceEntity
    {
        /// <summary>
        /// 资源编号 
        /// </summary>
        public int resourceid { get; set; }
        /// <summary>
        /// 资源类型编号 
        /// </summary>
        public int resourcetypeid { get; set; }
        /// <summary>
        /// 资源位置 
        /// </summary>
        public string resourcedir { get; set; }
        /// <summary>
        /// 资源名称 
        /// </summary>
        public string resourcename { get; set; }
        /// <summary>
        /// 发布机构 
        /// </summary>
        public string sender { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime createtime { get; set; }
        /// <summary>
        ///  文件大小
        /// </summary>
        public float filesize { get; set; }
        /// <summary>
        /// url 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 1.加密  0.未加密
        /// </summary>
        public int encryption { get; set; }
        /// <summary>
        /// 资源类型名 
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string resourcetypename { get; set; }
    }
}
