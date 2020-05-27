using System;
using System.Collections.Generic;
using System.Linq;
using Universal.Models;

namespace Utilities
{
    public class Tree
    {
        #region tree使用

        public static List<TreeModel> CreateTree(List<TreeModel> parentTreeObject, List<TreeModel> childrensTreeObject)
        {
            List<TreeModel> nodes = parentTreeObject;
            foreach (TreeModel item in nodes)
            {
                item.subMenue = GetChildrens(item, childrensTreeObject);
            }
            return nodes;
        }
        //递归获取子节点
        public static List<TreeModel> GetChildrens(TreeModel node, List<TreeModel> childrensTreeObject)
        {
            List<TreeModel> childrens = childrensTreeObject.Where(it => node.menueid == it.parentmenueid).Select(x => new TreeModel { menueid = x.menueid, menuename = x.menuename, parentmenueid = x.parentmenueid }).ToList();
            foreach (TreeModel item in childrens)
            {
                item.subMenue = GetChildrens(item, childrensTreeObject);
            }
            return childrens;
        }
        #endregion
    }
}
