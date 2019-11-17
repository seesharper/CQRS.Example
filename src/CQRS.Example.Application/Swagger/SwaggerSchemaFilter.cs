using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CQRS.Example.Application.Swagger
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.ApiModel.Type.Name.EndsWith("Command") || context.ApiModel.Type.Name.EndsWith("Query"))
            {
                schema.Title = Regex.Replace(context.ApiModel.Type.Name, "^(.+)(Command|Query)$", "$1Request");
            }

            var requiredProperties = context.ApiModel.Type.GetProperties()
                .Where(p => p.IsDefined(typeof(RequiredAttribute))).Select(p => ToCamelCase(p.Name));
            foreach (var requiredProperty in requiredProperties)
            {
                schema.Properties[requiredProperty].Nullable = false;
            }

            var ignoredProperties = context.ApiModel.Type.GetProperties()
                .Where(p => p.IsDefined(typeof(JsonIgnoreAttribute))).Select(p => ToCamelCase(p.Name));
            foreach (var ignoredProperty in ignoredProperties)
            {
                schema.Properties.Remove(ignoredProperty);
            }
        }

        private string ToCamelCase(string value)
        {
            return $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }
    }
}