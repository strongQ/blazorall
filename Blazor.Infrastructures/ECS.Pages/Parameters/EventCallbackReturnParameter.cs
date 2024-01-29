using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Pages.Parameters
{
    public sealed class EventCallbackReturnParameter<TInput, TOutput>
    {
        public TInput? Value { get; set; }
        public TOutput? OutputValue { get; set; }

        public EventCallbackReturnParameter()
        {
        }

        public EventCallbackReturnParameter(TOutput outputValue)
        {
            OutputValue = outputValue;
        }

        public EventCallbackReturnParameter(TInput value, TOutput outputValue)
        {
            Value = value;
            OutputValue = outputValue;
        }
    }
}
