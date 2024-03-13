using System;
namespace AssignmentAPI.Shared
{
    public interface IUserIdProvider
    {
        public string? GetUserId();
    }
}

