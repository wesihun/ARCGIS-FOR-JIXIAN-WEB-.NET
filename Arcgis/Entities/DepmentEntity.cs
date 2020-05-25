using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 部门表
    /// </summary>
    [SugarTable("tb_department")]
    public class DepmentEntity
    {
        /// <summary>
        /// 部门编号 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public int departmentid { get; set; }
        /// <summary>
        /// 部门名称 
        /// </summary>
        public string departmentname { get; set; }
        /// <summary>
        /// 父类Id 
        /// </summary>
        public string parentid { get; set; }
        ///  创建时间
        /// </summary>
        public DateTime createtime { get; set; }
    }
}
