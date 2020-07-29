using System;
using System.Collections.Generic;
using System.Text;

namespace Universal.Models
{
    public class RespResultViewModel
    {
        public int code { get; set; }

        public string msg { get; set; }

        public object data { get; set; }

    }

    public class RespResultCountViewModel : RespResultViewModel
    {
        public int count { get; set; }
        //public object complaindata { get; set; }
        //public string statusName { get; set; }

    }
    public class RespViewModel : RespResultViewModel
    {
        public int count { get; set; }
        public int loginCount { get; set; }

    }

}
