using System;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models
{ 
	public class LevelModel : BaseModel
    {
        public string LevelId { get; set; } =  Guid.NewGuid().ToString();
        public string? LevelName { get; set; }
        public string? LevelCode { get; set; }

        public string? BuildingId { get; set; }
        [JsonIgnore]
        public virtual  BuildingModel? Building { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoomModel>? Rooms { get; set; }
    }
}

