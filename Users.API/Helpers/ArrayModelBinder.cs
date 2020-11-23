using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Users.API.Helpers
{
    // IModelBinder: https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.imodelbinder?view=aspnet-mvc-5.2
    // BindModelAsync: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.modelbinding.imodelbinder.bindmodelasync?view=aspnetcore-5.0
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Check if non-enumerable, binder works with enumerable types.
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // If value isn't null, model is enumerable then get type and convert
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0]; // GenericTypeArguments for us is GUID,
            var converter = TypeDescriptor.GetConverter(elementType);                         // Create converter for GUID

            // Convert value list to enumerable type
            var values = value.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries) 
                .Select(x => converter.ConvertFromString(x.Trim())).ToArray(); // GUID string to GUID

            // Create array of type, set as model value
            var typedValues = Array.CreateInstance(elementType, values.Length); // Create array of GUID
            values.CopyTo(typedValues, 0); // Copy new values to new array
            bindingContext.Model = typedValues; // Set model on binding context to typed values, model is now an array of GUID

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
