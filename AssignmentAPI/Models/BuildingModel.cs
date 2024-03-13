using System;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models
{
	public class BuildingModel : BaseModel
    {
        public string BuildingId { get; set; } = Guid.NewGuid().ToString();
        public string? BuildingName { get; set; }
        public string? BuildingCode { get; set; }
        [JsonIgnore]
        public ICollection<LevelModel>? Levels { get; set; }
    }
}

