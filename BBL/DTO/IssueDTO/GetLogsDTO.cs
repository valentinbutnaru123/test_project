using System.Collections.Generic;

namespace BBL.DTO.IssueDTO
{
	public class GetLogsDTO
	{
		public List<GetLogDTO> Logs { get; set; }
		public int Total { get; set; }
		public int TotalFiltered { get; set; }
	}
}
