using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models
{
    public class LoginUser
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 验证码ID
        /// </summary>
        public long CodeID { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
