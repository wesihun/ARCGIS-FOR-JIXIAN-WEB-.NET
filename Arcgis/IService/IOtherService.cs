using Arcgis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcgis.IService
{
    public interface IOtherService
    {
        #region 获取数据
        /// <summary>
        /// 获取轮播表
        /// </summary>
        /// <returns></returns>
        List<BannerEntity> GetBannerList();
        /// <summary>
        /// 获取头条公告表
        /// </summary>
        /// <returns></returns>
        List<NoticeEntity> GetNoticeList(int istitle);
        #endregion
    }
}
