using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace WalletApp.Presentation.Filters;

public class AuthenticationFilter : IActionFilter
{
    private readonly string _secrectKey;

    public AuthenticationFilter(IConfiguration configuration)
    {
        _secrectKey = configuration.GetSection("UserSecretKey").Value;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public async void OnActionExecuting(ActionExecutingContext context)
    {
        if (context != null)
        {
            var customHeader = context.HttpContext.Request.Headers["Custom-Params"].ToString();

            var requestBody = JsonSerializer.Serialize(context.ActionArguments["request"]);
            string userId = customHeader.Split(',')[1];
            string providedDigest = customHeader.Split(',')[3];
            


            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(providedDigest))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Расчет ожидаемого хэша HMAC-SHA1 для тела запроса
            string expectedDigest = HmacSha1Helper.CalculateHmacSha1(requestBody, _secrectKey);

            // Сравнение предоставленного хэша с ожидаемым хэшем
            if (!string.Equals(providedDigest, expectedDigest, StringComparison.OrdinalIgnoreCase))
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("X-Digest and converted body don't match.");
                return;
            }
        }
    }
}
