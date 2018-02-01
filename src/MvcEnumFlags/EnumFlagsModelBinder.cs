using System;
using System.Web.Mvc;

namespace MvcEnumFlags
{
    public class EnumFlagsModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Fetch value to bind.
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null)
            {
                // Get type of value.
                var valueType = bindingContext.ModelType;

                // Try to parse enum values if present
                if (value.RawValue is string[] rawValues)
                {
                    // Try to parse variable.
                    try
                    {
                        // Parse.
                        return (Enum)Enum.Parse(valueType, string.Join(",", rawValues));
                    }
                    catch
                    {
                        return base.BindModel(controllerContext, bindingContext);
                    }

                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
