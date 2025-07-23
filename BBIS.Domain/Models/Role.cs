namespace BBIS.Domain.Models
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserRoleScreeningAccess> UserRoleScreeningAccesses { get; set; }
    }
}
