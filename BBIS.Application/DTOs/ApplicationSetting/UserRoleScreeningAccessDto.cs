using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Common.Enums;

namespace BBIS.Application.DTOs.ApplicationSetting
{

    public class UserRoleScreeningAccessDto
    {
        public Guid UserRoleScreeningAccessId { get; set; }
        public Guid RoleId { get; set; }
        public string ScreeningTabName { get; set; }
        public string ScreeningStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
