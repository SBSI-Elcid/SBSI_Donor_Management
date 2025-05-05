namespace BBIS.Domain.Models
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserAccountId { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
