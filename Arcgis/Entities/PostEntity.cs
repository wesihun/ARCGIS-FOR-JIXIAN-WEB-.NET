using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 岗位表 
    /// </summary>
    [SugarTable("tb_post")]
    public class PostEntity
    {
        /// <summary>
        /// 岗位Id 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public int postid { get; set; }
        /// <summary>
        /// 岗位名 
        /// </summary>
        public string postname { get; set; }
        /// <summary>
        /// 岗位描述 
        /// </summary>
        public string postdetail { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime createtime { get; set; }
    }
}
