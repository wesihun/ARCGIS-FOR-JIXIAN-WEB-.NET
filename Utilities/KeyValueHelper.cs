using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// 键值对解析Helper，修改matchKey作为键值之间的符号，matchValue为键值对之间的符号
    /// </summary>
    public static class KeyValueHelper
    {
        static Dictionary<object, object> conents = new Dictionary<object, object>();
        public static string matchKey { get; set; }
        public static string matchValue { get; set; }

        public static string data = "AREA: 面积,NAME: 名称,ZDMJ: 占地面积,CZCLX: 城镇村等用地类型,CZCDM: 城镇村代码,CZCMC: 城镇村名称,CZCMJ: 城镇村面积,  TBYBH: 图斑预编号,  KCXS: 扣除地类系数,KCMJ: 扣除地类面积,TBDLMJ: 图斑地类面积,GDPDJB: 耕地坡度级别,XZDWKD: 线状地物宽度,TBXHDM: 图斑细化代码,TBXHMC: 图斑细化名称,ZZSXDM: 种植属性代码,ZZSXMC: 种植属性名称,GDDB: 耕地等别,FRDBS: 飞入地标识,CZCSXM: 城镇村属性码,SJNF: 数据年份,  KFYQMC: 开发园区名称,KFYQLX: 开发园区类型,KFYQXZ: 开发园区性质,KFYQTZ: 开发园区特征,KFYQMJ: 开发园区面积,GLTBBSM: 关联图斑标识码,PZWJMC: 批准文件名称,PZWH: 批准文号, PZMJ: 批准面积,YTFL: 用途分类,JTXMYT: 具体项目用途,PZRQ: 批准日期,  XJXZQHDM: 县级行政区划代码,LXDM: 类型代码,SLDM: 数量代码,MC: 名称,RKSL: 人口数量,STGNYBHMB: 生态系统服务功能与保护目标,DLWZ: 地理位置,QYMJ: 区域面积,STXTYZBLX: 生态系统与植被类型,RWHDLX: 主要人为活动类型,STHJWT: 生态环境问题,GKCS: 管控措施,SLSJ: 设立时间,  TTQMJ: 推土区面积,   JXLX: 界线类型,JXXZ: 界线性质,JXSM: 界线说明,XZQDM : 行政区代码,XZQMC  : 行政区名称,DCMJ : 调查面积,JSMJ : 计算面积,MSSM : 描述说明,HDMC : 海岛名称,JBNTTBBH: 基本农田图斑编号,TBBH: 图斑编号,DLBM: 地类编码,DLMC: 地类名称,QSXZ: 权属性质,QSDWDM: 权属单位代码,QSDWMC: 权属单位名称,ZLDWDM: 坐落单位代码,ZLDWMC: 坐落单位名称,GDLX: 耕地类型,KCLX: 扣除类型,KCDLBM: 扣除地类编码,TKXS: 扣除地类系数,XZDWMJ: 线状地物面积,LXDWMJ: 零星地物面积,TKMJ: 扣除地类面积,TBMJ: 图斑面积,JBNTMJ: 基本农田面积,JBNTLX: 基本农田类型,ZLDJDM: 质量等级代码,PDJB: 坡度级别,DLBZ: 地类备注,BSM: 标识码,YSDM: 要素代码,BHQMC: 保护区名称,BHQDLWZ: 保护区地理位置,BHQJB: 保护区级别,PZJG: 批准机关,PZSJ: 批准时间,BHQMJ: 保护区面积,BZ: 备注";

        /// <summary>
        /// 解析输入Bytes中的键值对
        /// </summary>
        /// <param name="data">输入字节数组</param>
        /// <returns>解析后的键值对字典</returns>
        public static Dictionary<object, object> GetConentByBytes(byte[] data)
        {
            conents.Clear();
            conents = GetConentByString(Encoding.Default.GetString(data));
            return conents;
        }

        /// <summary>
        /// 解析输入字符串中的键值对
        /// </summary>
        /// <param name="data">输入字符串</param>
        /// <returns>解析后的键值对字典</returns>
        public static Dictionary<object, object> GetConentByString(string data)
        {
            conents.Clear();

            //Predicate<string> matchEqual = delegate(string value)
            //{
            //    return value == "=" ? true : false;
            //};

            //Predicate<string> matchComma = delegate(string value)
            //{
            //    return value == "," ? true : false;
            //};

            if (data.Substring(data.Length - 1) != matchValue)
            {
                data = data + matchValue;
            }

            try
            {
                int pos = 0;
                int startIndex = 0;
                while (true)
                {
                    //Get Key
                    pos = data.IndexOf(matchKey, startIndex);
                    string key = data.Substring(startIndex, pos - startIndex);
                    startIndex = pos + 1;
                    //Get Value
                    pos = data.IndexOf(matchValue, startIndex);
                    string value = data.Substring(startIndex, pos - startIndex);
                    startIndex = pos + 1;
                    conents.Add(key, value);

                    if (startIndex >= data.Length)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Info: " + ex.ToString());
            }

            return conents;
        }
    }
}
