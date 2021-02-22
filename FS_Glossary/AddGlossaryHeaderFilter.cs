using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace FS_Glossary {
    public class AddGlossaryHeaderFilter : IOperationFilter {
        public void Apply(Operation operation, OperationFilterContext context) {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter {
                Name = "BusinessTerms",
                In = "header",
                Type = "string",
                Enum = new List<object> { options.Include.ToString(), options.Exclude.ToString() },
                Required = true,
                Default = options.Include.ToString(),
                Description = "Set to Include to get business definition of matching terms from glossary in the response."
            });
        }
    }

    public enum options {
        Include,
        Exclude
    }
}
