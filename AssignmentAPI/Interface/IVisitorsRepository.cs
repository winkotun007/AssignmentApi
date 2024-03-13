using System;
using AssignmentAPI.DTO.VisitorsDTO;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Interface
{
	public interface IVisitorsRepository
	{
        Task<ResponseModel<IEnumerable<GetVisitorsDTO>>> GetVisitorsAsync();
        Task<VisitorsModel> GetVisitorsByIdAsync(string id);
        Task<ResponseModel<VisitorsModel>> CreateVisitorsAsync(CreateVisitorsDTO visitorsDTO);
        Task<ResponseModel<VisitorsModel>> UpdateVisitorsAsync(string id, UpdateVisitorsDTO updateVisitorsDTO);
        Task<IActionResult> DeleteVisitorsAsync(string id);
    }
}

