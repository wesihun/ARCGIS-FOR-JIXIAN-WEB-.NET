using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 申请表
    /// </summary>
    [SugarTable("tb_apply")]
    public class ApplyEntity
    {
        /// <summary>
        /// 申请编号 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public int applyid { get; set; }
        /// <summary>
        /// 资源编号 
        /// </summary>
        public int resourceid { get; set; }
        /// <summary>
        /// 用户编号 
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 用户名 
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 部门id 
        /// </summary>
        public int depid { get; set; }
        /// <summary>
        /// 部门名
        /// </summary>
        public string depname { get; set; }
        /// <summary>
        /// 申请用途 
        /// </summary>
        public string applyreason { get; set; }
        /// <summary>
        /// 申请状态  0 审核中   1已通过   2 已下载   -1 退回
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 审核原因 
        /// </summary>
        public string reson { get; set; }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime createtime { get; set; }
    }
}
