using System;
using AssignmentAPI.DTO.GuestAccessDTO;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Interface
{
	public interface IGuestAcessRepository
	{
        Task<ResponseModel<IEnumerable<GuestAccessModel>>> GetGuestAccesssAsync();
        Task<ActionResult<ResponseModel<IEnumerable<GuestAccessModel>>>> GetGuestAccessByBuilding(string guessAccessId);
        Task<GuestAccessModel> GetGuestAccessByIdAsync(string id);
        Task<ResponseModel<GuestAccessModel>> CreateGuestAccessAsync(CreateGuestAccessDTO guestAccessDTO);
        Task<ResponseModel<GuestAccessModel>> UpdateGuestAccessAsync(string id, UpdateGuestAccessDTO updateGuestAccessDTO);
        Task<IActionResult> DeleteGuestAccessAsync(string id);
        Task<bool> isAccessByPath(string Path, string Method);
    }
}

