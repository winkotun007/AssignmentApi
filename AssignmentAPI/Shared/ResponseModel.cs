using System;
namespace AssignmentAPI.Shared
{
	public class ResponseModel<T>
	{
		public int Code { get; set; }
		public string? Message { get; set; }
		public T? Data { get; set; }
	}
}
