namespace BBIS.Application.DTOs.UserAccount
{
    public class UpdateUserAccountDto: UserAccountBaseDto
    {      
        public Guid UserAccountId { get; set; }
        public bool PasswordHasChange { get; set; }
        public string? UpdatedPassword { get; set; }
        public bool IsActive { get; set; }
        public List<Guid> RoleIds { get; set; }
        public List<Guid> UserModuleIds { get; set; }
    }
}
