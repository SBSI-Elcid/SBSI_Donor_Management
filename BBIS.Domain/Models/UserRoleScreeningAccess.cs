using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column(TypeName = "varchar(255)")]
        public string ScreeningTabName { get; set; }

        public string ScreeningStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Role Role { get; set; }
    }
}
