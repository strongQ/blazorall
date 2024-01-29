using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Pages.Parameters
{
    public static class EventCallbackExtension
    {
        public static async Task<TOutput?> InvokeReturnAsync<TInput, TOutput>(this EventCallback<EventCallbackReturnParameter<TInput, TOutput?>> callback, TInput input, TOutput? output = default)
        {
            var callbackReturn = new EventCallbackReturnParameter<TInput, TOutput?>()
            {
                Value = input,
                OutputValue = output
            };

            await callback.InvokeAsync(callbackReturn);

            return callbackReturn.OutputValue;
        }
    }
}
