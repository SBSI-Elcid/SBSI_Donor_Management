namespace BBIS.Domain.Models
{
    public class UserRefreshToken
    {
        public Guid UserRefreshTokenId { get; set; }
        public Guid UserAccountId { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
