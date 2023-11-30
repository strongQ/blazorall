using System;
using System.Collections.Generic;
using System.Text;

namespace Client.API.Models
{
    public class ActionResultVm<T>
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> Content { get; set; }

        /// <summary>
        /// 总数据量
        /// </summary>
        public int TotalElements { get; set; }
    }
}
