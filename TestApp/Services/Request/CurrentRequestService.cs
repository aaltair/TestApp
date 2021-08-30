using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TestApp.Services.RequestService.Interfaces;

namespace TestApp.Services.RequestService
{
    public class CurrentRequestService : ICurrentRequestService
    {

        public CurrentRequestService(IHttpContextAccessor context )
        {
            UserId = context.HttpContext?.User?.FindFirstValue("Id");
            Locale = string.IsNullOrEmpty(context.HttpContext?.Request.Headers["Locale"]) ? "en": (string) context.HttpContext?.Request.Headers["Locale"];
        }
        public string UserId { get; set; }
        public string Locale { get; set; }
    }
}