using System;
using System.Linq;
using System.Reflection;
using RequestApprovalService.Silverspoon.StatusCodeResponse;

namespace RequestApprovalService.Silverspoon.ExceptionBase
{
    public class RootException<T> : EitherException
    {
        public RootException(Type type, T request, BaseHttpStatusCodes statusCode = BaseHttpStatusCodes.Status400BadRequest,
            string message = "") : base(message)
        {
            if (type?.FullName != null)
            {
                this.Data.Add("TranslatorKey", type.FullName?.Replace(".", "_"));
            }

            if (type != null && request != null)
            {
                var translatorValues = request.GetType().GetProperties(BindingFlags.Public
                                                                       | BindingFlags.Instance
                                                                       | BindingFlags.DeclaredOnly)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(request)?.ToString());

                this.Data.Add("TranslatorValues", translatorValues);
            }

            this.Data.Add("HttpStatusCode", statusCode);

            this.Data.Add("Message", message);
        }
    }

}
