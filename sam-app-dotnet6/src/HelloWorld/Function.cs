using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HelloWorld
{

    public class Function
    {

        private static readonly HttpClient client = new HttpClient();

        private static async Task<string> GetCallingIP()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "AWS Lambda .Net Client");

            var msg = await client.GetStringAsync("http://checkip.amazonaws.com/").ConfigureAwait(continueOnCapturedContext:false);

            return msg.Replace("\n","");
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            var c = context.InvokedFunctionArn;

            var location = await GetCallingIP();
            var body = new Dictionary<string, string>
            {
                { "message", "hello world" },
                { "location", location },
                { "ItemIds", GetSelectedItemId(apigProxyEvent) },
                { "context", context.ToString() },
                { "arn", c }
            };

            return new APIGatewayProxyResponse
            {
                Body = JsonSerializer.Serialize(body),
                StatusCode = 200,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        private string GetSelectedItemId(APIGatewayProxyRequest request) {
            return GetValidItemId(request);
        }

        private string GetValidItemId(APIGatewayProxyRequest request) {
            var s = GetQueryStringItem(request, "ItemId");
            string[] validIds = { 
                "03b90a57-c061-4edd-99ed-21133ae4c04f", 
                "3e866916-7ed1-4eb6-8427-093eb621af93"
                };
            
            if (
                !String.IsNullOrEmpty(s)
                && s.Length == 36
                && validIds.Contains(s)
            )
            {
                return s;
            } else {
                return string.Empty;
            }
        }

        private string GetQueryStringItem(APIGatewayProxyRequest request, string key) {
            var q = GetQueryString(request);
            return q.ContainsKey(key) 
                ? q[key] 
                : string.Empty;
        }

        private IDictionary<string, string> GetQueryString(APIGatewayProxyRequest request) {
            return request.QueryStringParameters ?? new Dictionary<string, string>();
        }
    }
}
