using Arcgis.Entities;
using Arcgis.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Universal.Models;

namespace Arcgis.IService
{
    public interface IPersonalCenterService
    {
        #region 获取数据
        /// <summary>
        /// 管理员个人中心列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="totalCount">返回数目</param>
        /// <returns></returns>
        List<ManagePersonalCenter> GetManageList(string states,int pageIndex, int pageSize, ref int totalCount);
        /// <summary>
        /// 获取个人资料管理列表
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="userid">userid</param>
        /// <returns></returns>  
        UserEntity GetPerInfo(int userid);

        bool PostPerInfo(UserEntity entity);

        List<AddressInfoEntity> GetAreaInfo1();

        List<AddressInfoEntity> GetAreaInfo2(int userid);

        List<PostEntity> GetPostList();

        List<TreeModel> GetDepList();
        /// <summary>
        /// 正常个人中心列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="totalCount">返回数目</param>
        /// <returns></returns>
        List<PersonalCenter> GetPersonList(int userid,int states, int pageIndex, int pageSize, ref int totalCount);
        /// <summary>
        /// 获取消息通知条数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        int GetNoticeCount(int userid);
        #endregion

        #region 提交数据
        /// <summary>
        /// 审核管理员审核
        /// </summary>
        /// <param name="reson"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        bool Examine(int applyid, string reson,int states);
        string Download(int applyid,string type);
        bool Operation(int applyid,int states);
        bool OperationBatch(List<int> applyids, int states);
        #endregion
    }
}
