namespace BBIS.Domain.Models
{
    public class UserModule
    {
        public Guid UserModuleId { get; set; }
        public Guid ModuleId { get; set; }
        public Guid UserAccountId { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual Module Module { get; set; }
    }
}
