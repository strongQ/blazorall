using System;
using System.Collections.Generic;
using System.Text;

namespace Client.API.Models
{
    public class KeyWordQuery
    {
        public string KeyWords { get; set; }

        public List<DateTime> CreateTime { get; set; }
    }
}
