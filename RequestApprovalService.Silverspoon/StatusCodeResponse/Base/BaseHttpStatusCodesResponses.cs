using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RequestApprovalService.Silverspoon.ExceptionBase;


namespace RequestApprovalService.Silverspoon.StatusCodeResponse.Base
{
    public abstract class BaseHttpStatusCodesResponses : IActionResult, IBaseHttpStatusCodesResponses
    {
        public string? Text { get; set; } = null;
        private readonly Exception _exception;

        protected BaseHttpStatusCodesResponses(EitherException exception)
        {
            _exception = exception;
        }

        protected BaseHttpStatusCodesResponses(ExceptionContext context)
        {
            _exception = context.Exception;
        }

        public ActionResult GetActionResult()
        {
            var httpStatusCode = GetHttpStatusCode(_exception);
            var translatorKey = GetTranslatorKey(_exception);
            var translatorValues = GetTranslatorValues(_exception);
            var responseErrorText = Translator(translatorKey, translatorValues);

            var objectResult = new ObjectResult(responseErrorText)
            {
                StatusCode = httpStatusCode
            };

            return objectResult;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = GetActionResult();
            await objectResult.ExecuteResultAsync(context);
        }

        private int GetHttpStatusCode(Exception exception)
        {
            var result = (int)HttpStatusCode.InternalServerError;
            if (exception.Data.Contains("HttpStatusCode")) result = (int)exception.Data["HttpStatusCode"];

            return result;
        }

        private string? GetTranslatorKey(Exception exception)
        {
            var result = "Unknown";
            if (exception.Data.Contains("TranslatorKey")) result = (string)exception.Data["TranslatorKey"];

            return result;
        }

        private Dictionary<string, string>? GetTranslatorValues(Exception exception)
        {
            var result = new Dictionary<string, string>();
            if (exception.Data.Contains("TranslatorValues"))
                result = (Dictionary<string, string>)exception.Data["TranslatorValues"];

            return result;
        }

        private string? Translator(string? translatorKey, Dictionary<string, string>? translatorValues)
        {
            return translatorKey;
        }
    }

}
