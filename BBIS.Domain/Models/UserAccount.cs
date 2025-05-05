namespace BBIS.Domain.Models
{
    public class UserAccount
    {
        public Guid UserAccountId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool ChangePasswordOnLogin { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        public virtual UserRefreshToken UserRefreshToken { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserModule> UserModules { get; set; }

        public virtual ICollection<DonorBloodCollection> DonorBloodCollections { get; set; }
    }
}
