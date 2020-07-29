using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 行政区划表
    /// </summary>
    [SugarTable("tb_addressinfo")]
    public class AddressInfoEntity
    {
        /// <summary>
        ///  
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public int id { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string TreeCode { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string Remark { get; set; }
    }
}
