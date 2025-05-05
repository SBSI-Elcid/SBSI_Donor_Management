
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Module;
using BBIS.Application.DTOs.UserAccount;
using BBIS.Common;
using BBIS.Common.Exceptions;
using BBIS.Common.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BBIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAccountsController : ApiControllerBase
    {
        private readonly IUserAccountService accountService;
       
        public UserAccountsController(IUserAccountService accountService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Authorize(Policy = ApplicationRoles.AdminPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> Create([FromBody] CreateUserAccountDto dto)
        {
            try
            {
                var result = await this.accountService.CreateUserAccount(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordExistException ex)
            {
                logger.LogError($"Something went wrong creating user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong creating user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Authorize(Policy = ApplicationRoles.AdminPolicy)]
        public async Task<ActionResult<RequestResult>> Update([FromBody] UpdateUserAccountDto dto)
        {
            try
            {
                await this.accountService.UpdateUserAccount(dto, this.UserId);
                return this.Json();
            }
            catch (RecordExistException ex)
            {
                logger.LogError($"Something went wrong updating user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong updating user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = ApplicationRoles.AdminPolicy)]
        public async Task<ActionResult<RequestResult>> Delete(Guid id)
        {
            try
            {
                await this.accountService.DeleteUserAccount(id, this.UserId);
                return this.Json();
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong deleting user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong deleting user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestResult<UserAccountViewDto>>> GetUser(Guid id)
        {
            try
            {
                var result = await this.accountService.GetUserAccount(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went retrieving user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("list")]
        [Authorize(Policy = ApplicationRoles.AdminPolicy)]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<UserAccountViewDto>>>> GetList([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.accountService.GetPagedListAsync(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving user list: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("update-profile")]
        public async Task<ActionResult<RequestResult<(string,string)>>> UpdateProfile([FromBody] UserProfileUpdateDto dto)
        {
            try
            {
                var result = await this.accountService.UpdateUserProfile(dto);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong updating user profile: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("modules/{id}")]
        public async Task<ActionResult<RequestResult<List<ModuleDto>>>> GetUserModules(Guid id)
        {
            try
            {
                var result = await this.accountService.GetUserModules(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went retrieving user modules: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving user modules: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("all-modules")]
        public async Task<ActionResult<RequestResult<List<ModuleDto>>>> GetAllModules()
        {
            try
            {
                var result = await this.accountService.GetAllModules();
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving modules: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
