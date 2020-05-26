using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 轮播图表
    /// </summary>
    [SugarTable("tb_apply")]
    public class BannerEntity
    {
        /// <summary>
        /// 编号 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public int bannerid { get; set; }
        /// <summary>
        /// 存储路径 
        /// </summary>
        public string bannerdir { get; set; }
        /// <summary>
        /// 启用状态   1启用    0禁用 
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public string createtime { get; set; }
    }
}
