using Arcgis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.IService
{
    public interface IlogService
    {
        #region 提交数据
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool Create(LogEntity entity);
        #endregion
    }
}
