using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public class filters
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public int? DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public DateTime? DobFrom { get; set; }
        public DateTime? DobTo { get; set; }

        public string? sortColumn { get; set; }

        public string? sortDir { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

}
