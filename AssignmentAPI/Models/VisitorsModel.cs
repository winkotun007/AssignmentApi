using System;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models
{
    public class VisitorsModel : BaseModel
    {
        public string VisitorId { get; set; } = Guid.NewGuid().ToString();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NRICNumber { get; set; }
        public string? PlateNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? Designation { get; set; }
        public string? BuildingId { get; set; }
        public string? LevelId { get; set; }
        public string? RoomId { get; set; }
        public bool isStayHomeNotice { get; set; }
        public bool isConfirmed14Day { get; set; }
        public bool isFever { get; set; }
        public bool isAcknowledged { get; set; }

        public virtual BuildingModel? Building { get; set; }

        public virtual LevelModel? Level { get; set; }

        public virtual RoomModel? Room { get; set; }

    }
}


