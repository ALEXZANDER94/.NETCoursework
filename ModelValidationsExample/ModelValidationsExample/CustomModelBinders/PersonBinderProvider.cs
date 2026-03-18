using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationsExample.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ModelValidationsExample.CustomModelBinders
{
    public class PersonBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(Person))
            {
                return new BinderTypeModelBinder(typeof(PersonModelBinder));
            }
        }
    }
}
