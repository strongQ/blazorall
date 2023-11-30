using System;
using System.Collections.Generic;
using System.Text;

namespace Client.API.Models
{
    public class TokenInput
    {
        public TokenInput(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// 过期token
        /// </summary>
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

}
