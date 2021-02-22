using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FS_Glossary {
    public class GlossaryMiddleware {
        private readonly RequestDelegate _next;

        public GlossaryMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            string glossaryHeader = httpContext.Request.Headers["BusinessTerms"];
            if (!string.IsNullOrEmpty(glossaryHeader) && glossaryHeader.Equals(options.Include.ToString(), StringComparison.OrdinalIgnoreCase)) {
                Stream originalBody = httpContext.Response.Body;

                try {
                    using (var memStream = new MemoryStream()) {
                        httpContext.Response.Body = memStream;
                        await _next.Invoke(httpContext);

                        memStream.Position = 0;
                        string responseBody = new StreamReader(memStream).ReadToEnd();
                        // TODO: add some API call here
                        string url = "";
                        var payload = new {
                            TextContent = responseBody.Replace("\"", " \" "),
                            MatchType = "Partial"
                        };
                        httpContext.Response.Body = originalBody;
                        var client = new HttpClient();
                        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                        var glossaryResult = client.PostAsync(url, content).Result;
                        var glossary = glossaryResult.Content.ReadAsStringAsync().Result;
                        var apiResultObj = JsonConvert.DeserializeObject(responseBody);
                        if (apiResultObj == null) {
                            await httpContext.Response.WriteAsync(responseBody + "\nGlossary: " + glossary);

                        } else {
                            var result = new { data = apiResultObj, FS_Glossary = JsonConvert.DeserializeObject(glossary) };
                            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
                        }
                    }

                } catch {
                    httpContext.Response.Body = originalBody;
                }
            } else {
                await _next.Invoke(httpContext);
            }
        }
    }
}
