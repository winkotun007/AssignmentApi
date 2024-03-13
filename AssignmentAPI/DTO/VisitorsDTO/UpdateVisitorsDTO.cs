using System;
using FluentValidation;

namespace AssignmentAPI.DTO.VisitorsDTO
{

    public class UpdateVisitorsDTO
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
        public bool isStayHomeNotice { get; set; }
        public bool isConfirmed14Day { get; set; }
        public bool isFever { get; set; }
        public bool isAcknowledged { get; set; }
    }

    public class VisitorsUpdateDTOValidator : AbstractValidator<UpdateVisitorsDTO>
    {
        public VisitorsUpdateDTOValidator()
        {
            RuleFor(x => x.VisitorId).NotNull().NotEmpty().WithMessage("Visitor ID is required.");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name is required.");
            RuleFor(x => x.NRICNumber).NotNull().NotEmpty().WithMessage("NRICNumber is required.");
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("Company Name is required.");
            RuleFor(x => x.BuildingId).NotNull().NotEmpty().WithMessage("Building is required.");
            RuleFor(x => x.LevelId).NotNull().NotEmpty().WithMessage("Level is required.");
            RuleFor(x => x.RoomId).NotNull().NotEmpty().WithMessage("Room is required.");
            RuleFor(x => x.isStayHomeNotice).NotNull().NotEmpty().WithMessage("Stay Home Notice is required.");
            RuleFor(x => x.isConfirmed14Day).NotNull().NotEmpty().WithMessage("Confirmed 14 Day is required.");
            RuleFor(x => x.isFever).NotNull().NotEmpty().WithMessage("Fever or Not is required.");
            RuleFor(x => x.isAcknowledged).NotNull().NotEmpty().WithMessage("Acknowledge is required.");
        }
    }

}

