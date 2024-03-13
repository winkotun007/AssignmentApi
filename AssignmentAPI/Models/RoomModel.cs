using System;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models
{
	public class RoomModel : BaseModel
    {
        public string RoomId { get; set; } =  Guid.NewGuid().ToString();
        public string? RoomName { get; set; }
        public string? RoomCode { get; set; }

        public string? LevelId { get; set; }

        [JsonIgnore]
        public virtual LevelModel? Levels { get; set; }
    }
}

