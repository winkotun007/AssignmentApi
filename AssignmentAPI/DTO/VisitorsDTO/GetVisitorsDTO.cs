using System;
namespace AssignmentAPI.DTO.VisitorsDTO
{
         public class GetVisitorsDTO
		{

            public string VisitorId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string NRICNumber { get; set; }
            public string PlateNumber { get; set; }
            public string CompanyName { get; set; }
            public string Designation { get; set; }
            public string BuildingId { get; set; }
            public string LevelId { get; set; }
            public string RoomId { get; set; }
            public string BuildingName { get; set; }
            public string RoomName { get; set; }
            public string LevelName { get; set; }
            public bool isStayHomeNotice { get; set; }
            public bool isConfirmed14Day { get; set; }
            public bool isFever { get; set; }
            public bool isAcknowledged { get; set; }
        }
}

