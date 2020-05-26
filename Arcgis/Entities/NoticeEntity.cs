using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 公告表
    /// </summary>
    [SugarTable("tb_notice")]
    public class NoticeEntity
    {
        /// <summary>
        /// 编号 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public int noticeid { get; set; }
        /// <summary>
        /// 内容 
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 普通图片
        /// </summary>
        public string image { get; set; }
        /// <summary>
        /// 标题图片
        /// </summary>
        public string titleimage { get; set; }
        /// <summary>
        /// 是否头条公告  1是  0否
        /// </summary>
        public int istitle { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public string createtime { get; set; }
    }
}
