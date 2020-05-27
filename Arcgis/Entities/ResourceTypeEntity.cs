using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 资源类型表
    /// </summary>
    [SugarTable("tb_resourcetype")]
    public class ResourceTypeEntity
    {
        /// <summary>
        /// 资源类型编号 
        /// </summary>
        public int resourcetypeid { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int parentid { get; set; }
        /// <summary>
        /// 资源类型名 
        /// </summary>
        public string resourcetype { get; set; }       
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime createtime { get; set; }
        /// <summary>
        /// 排序 
        /// </summary>
        public string SortCode { get; set; }
    }
}
