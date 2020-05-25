using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 日志表
    /// </summary>
    [SugarTable("tb_log")]
    public class LogEntity
    {
        /// <summary>
        /// 日志id 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public int logid { get; set; }
        /// <summary>
        /// 用户id 
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 日志标题 
        /// </summary>
        public string logtitle { get; set; }
        /// <summary>
        /// 日志内容 
        /// </summary>
        public string logcontent { get; set; }
        ///  创建时间
        /// </summary>
        public DateTime createtime { get; set; }
    }
}
