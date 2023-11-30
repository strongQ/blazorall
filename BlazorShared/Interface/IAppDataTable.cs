using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Interface
{
    public interface IAppDataTable
    {
        public Task QueryClickAsync();
    }
}
