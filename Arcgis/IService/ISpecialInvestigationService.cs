using Arcgis.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Universal.Models;

namespace Arcgis.IService
{
    public interface ISpecialInvestigationService
    {
        #region 获取数据
        /// <summary>
        /// 资源列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="totalCount">返回数目</param>
        /// <returns></returns>
        List<ResourceEntity> GetPageListByCondition(string name, int typeid,int pageIndex, int pageSize, ref int totalCount);
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        List<DepmentEntity> GetDepList();
        /// <summary>
        /// 获取资源类型列表
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetResourceTypeList();
        #endregion

        #region 提交数据
        /// <summary>
        /// 新增申请
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool InsertApply(ApplyEntity entity);
        #endregion
    }
}
