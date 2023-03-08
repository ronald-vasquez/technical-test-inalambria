using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Core.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var NameProperty = bindingContext.ModelName;
            var ProviderofValue = bindingContext.ValueProvider.GetValue(NameProperty);

            if (ProviderofValue == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var ValueDeserialize = JsonConvert.DeserializeObject<T>(ProviderofValue.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(ValueDeserialize);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(NameProperty, "Invalid value");
            }
            return Task.CompletedTask;
        }

        Task IModelBinder.BindModelAsync(ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}
