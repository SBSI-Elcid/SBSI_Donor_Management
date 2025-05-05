namespace BBIS.Application.DTOs.UserAccount
{
    public class UserProfileUpdateDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
