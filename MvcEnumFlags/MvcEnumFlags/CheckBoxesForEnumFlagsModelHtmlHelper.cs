using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcEnumFlags
{
    public static class CheckBoxesForEnumFlagsModelHtmlHelper
    {
        /// <summary>
        /// Creates checkboxes for flag enums.
        /// Based on implementation at http://stackoverflow.com/questions/9264927/model-bind-list-of-enum-flags.
        /// </summary>
        /// <typeparam name="TModel">Type of flag enum.</typeparam>
        /// <param name="htmlHelper">Html helper.</param>
        /// <returns></returns>
        public static IHtmlString CheckBoxesForEnumFlagsFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumModelType = metadata.ModelType;

            // Check to make sure this is an enum.
            if (!enumModelType.IsEnum)
            {
                throw new ArgumentException("This helper can only be used with enums. Type used was: " + enumModelType.FullName.ToString() + ".");
            }

            // Create string for Element.
            var sb = new StringBuilder();
            foreach (Enum item in Enum.GetValues(enumModelType))
            {
                if (Convert.ToInt32(item) != 0)
                {
                    var ti = htmlHelper.ViewData.TemplateInfo;
                    var id = ti.GetFullHtmlFieldId(item.ToString());
                    var name = ti.GetFullHtmlFieldName(string.Empty);
                    var label = new TagBuilder("label");
                    label.Attributes["for"] = id;
                    var field = item.GetType().GetField(item.ToString());

                    // Add checkbox.
                    var checkbox = new TagBuilder("input");
                    checkbox.Attributes["id"] = id;
                    checkbox.Attributes["name"] = name;
                    checkbox.Attributes["type"] = "checkbox";
                    checkbox.Attributes["value"] = item.ToString();
                    var model = metadata.Model as Enum;
                    if ((model != null) && (model.HasFlag(item)))
                    {
                        checkbox.Attributes["checked"] = "checked";
                    }
                    sb.AppendLine(checkbox.ToString());

                    // Check to see if DisplayName attribute has been set for item.
                    var displayName = field.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                        .FirstOrDefault() as DisplayNameAttribute;
                    if (displayName != null)
                    {
                        // Display name specified.  Use it.
                        label.SetInnerText(displayName.DisplayName);
                    }
                    else
                    {
                        // Check to see if Display attribute has been set for item.
                        var display = field.GetCustomAttributes(typeof(DisplayAttribute), true)
                            .FirstOrDefault() as DisplayAttribute;
                        if (display != null)
                        {
                            label.SetInnerText(display.Name);
                        }
                        else
                        {
                            label.SetInnerText(item.ToString());
                        }
                    }
                    sb.AppendLine(label.ToString());

                    // Add line break.
                    sb.AppendLine("<br />");
                }
            }

            return new HtmlString(sb.ToString());
        }
    }
}