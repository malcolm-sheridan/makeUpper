using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace MakeUpper
{
    public class Function
    {
        public APIGatewayProxyResponse CreateApiGatewayProxyResponse(string returnBody, ILambdaContext context)
        {
            context.Logger.LogLine(returnBody);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = returnBody,
                Headers = new Dictionary<string, string> { { "Content-Type", "text/json" } }
            };            
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>        
        public APIGatewayProxyResponse MakeUpper(string input, ILambdaContext context)
        {
            string response = "DEFAULT STRING";
            context.Logger.LogLine("MakeUpper request\n");

            if (!string.IsNullOrWhiteSpace(input))
            {
                response = input.ToUpper();
            }

            context.Logger.LogLine("MakeUpper done\n");
            return CreateApiGatewayProxyResponse(response, context);
        }        
    }
}
