using BBIS.Common;
using BBIS.Common.Enums;
using BBIS.Common.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BBIS.Api.Controllers
{
    public class ApiControllerBase: ControllerBase
    {
        protected readonly ILoggerManager logger;
        private readonly IConfiguration configuration;

        public ApiControllerBase(ILoggerManager loggerManager, IConfiguration configuration)
        {
            this.logger = loggerManager;
            this.configuration = configuration;
        }

        protected bool IsOffline 
        { 
            get
            {
                var externalBaseUrl = configuration.GetValue<string>("ExternalBaseUrl");
                var location = new Uri($"{Request.Scheme}://{Request.Host}");
                                               
                return !string.Equals(externalBaseUrl, location.AbsoluteUri);
            }
        }

        protected ClaimsPrincipal CurrentAccount { get { return HttpContext.User; } }
        protected Guid UserId
        {
            get
            {
                var claimObj = CurrentAccount.Claims.FirstOrDefault(c => c.Type == AuthClaimTypes.UserId);
                return claimObj == null ? Guid.Empty : Guid.Parse(claimObj.Value);
            }
        }

        protected List<string> UserRoles
        {
            get
            {
                var roles = CurrentAccount.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                return roles == null ? new List<string>() : roles;
            }
        }
        protected List<string> UserRoleAccess
        {
            get
            {
                var roles = CurrentAccount.Claims.Where(c => c.Type == AuthClaimTypes.RoleAccess).Select(c => c.Value).ToList();
                return roles == null ? new List<string>() : roles;
            }
        }

        protected JsonResult Json(object data = null, string message = "", bool success = true, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new JsonResult(new RequestResult<object>
            {
                Data = data,
                Message = message,
                StatusCode = (int)statusCode,
                Success = success
            });
        }

        protected JsonResult Json(object data, HttpStatusCode statusCode)
        {
            return Json(data, success: true, statusCode: statusCode);
        }

        protected JsonResult JsonError(string message, HttpStatusCode statusCode, object? data = null)
        {
            return Json(data: data, message: message, success: false, statusCode: statusCode);
        }
    }
}
