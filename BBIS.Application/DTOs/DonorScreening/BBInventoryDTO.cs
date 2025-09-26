using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class BBInventoryDTO
    {
        public string bag_no { get; set; } = string.Empty;         // required|string
        public string reg_no { get; set; } = string.Empty;         // required|string
        public string blood_group { get; set; } = string.Empty;    // required|string
        public string component { get; set; } = string.Empty;     // required|string
        public string volume { get; set; } = string.Empty;     // required|string|max:25
        public DateTime? collection_date { get; set; }             // nullable|date
        public DateTime expiry { get; set; }                      // required|date
        public string source { get; set; } = string.Empty;        // required|string
        public string? bgroup { get; set; }                       // nullable|string
        public string? rh { get; set; }                           // nullable|string
        public string? bp_statusid { get; set; }                   // nullable|string
        public bool? has_allocation { get; set; }                  // nullable|boolean
        public string? firstname_alloc { get; set; }               // nullable|string
        public string? middlename_alloc { get; set; }              // nullable|string
        public string? lastname_alloc { get; set; }                // nullable|string
        public string? segmentserialno { get; set; }              // nullable|string
    }
}
