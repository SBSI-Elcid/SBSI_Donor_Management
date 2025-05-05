namespace BBIS.Application.DTOs.UserAccount
{
    public class CreateUserAccountDto: UserAccountBaseDto
    {
        public string Password { get; set; }
        public List<Guid> RoleIds { get; set; }
        public List<Guid> UserModuleIds { get; set; }
    }
}
