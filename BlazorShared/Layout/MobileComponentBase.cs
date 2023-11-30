using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Layout
{
    public class MobileComponentBase : CultureComponentBase
    {
        [CascadingParameter(Name = "IsMobile")]
        public bool IsMobile { get; set; }
    }
}
