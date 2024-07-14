using BLL.DTO.IssueDTO;
using BLL.DTO.PosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBL.DTO.IssueDTO
{
	public class GetIssuessDTO
	{
		public List<GetIssuesDTO> IssueDTO { get; set; }
		public int Total { get; set; }
		public int TotalFiltered { get; set; }
	}
}
