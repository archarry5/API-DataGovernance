using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace FS_Glossary {
    public static class GlossaryMiddlewareExtension {
        public static IApplicationBuilder UseGlossary(this IApplicationBuilder applicationBuilder) {
            return applicationBuilder.UseMiddleware<GlossaryMiddleware>();
        }
    }
}
