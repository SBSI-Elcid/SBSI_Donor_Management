using BBIS.Application.DTOs.Module;

namespace BBIS.Application.DTOs.UserAccount
{
    public class UserAccountViewDto: UserAccountBaseDto
    {
        public Guid UserAccountId { get; set; }
        public bool IsActive { get; set; }
        public List<Guid> RoleIds { get; set; }
        public List<ModuleDto> UserModules { get; set; }
    }
}
