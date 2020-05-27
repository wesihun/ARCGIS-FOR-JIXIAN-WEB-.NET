using System;
using System.Collections.Generic;
using System.Text;

namespace Universal.Models
{
    public class TreeModel
    {
        public int menueid { get; set; }
        public string menuename { get; set; }
        public int parentmenueid { get; set; }

        public IList<TreeModel> subMenue = new List<TreeModel>();
    }
}
