using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Common.Enums;

namespace BBIS.Domain.Models
{
    public class UserRoleScreeningAccess
    {
        public Guid UserRoleScreeningAccessId { get; set; }
        public Guid RoleId { get; set; }
        public TabNames ScreeningTabName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Role Role { get; set; }
    }
}
