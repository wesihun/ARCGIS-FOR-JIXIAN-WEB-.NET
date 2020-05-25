using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.Entities
{
    /// <summary>
    /// 用户表 
    /// </summary>
    [SugarTable("tb_user")]
    public class UserEntity
    {
        /// <summary>
        /// 申请编号 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public int userid { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public int departmentid { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public int postid { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string username { get; set; }       
        /// <summary>
        ///  
        /// </summary>
        public string password { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string realname { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string telephone { get; set; }        
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime createtime { get; set; }
        /// <summary>
        /// 状态   1为已审核通过 
        /// </summary>
        public int state { get; set; }
    }
}
