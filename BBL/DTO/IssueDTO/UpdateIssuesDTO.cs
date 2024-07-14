using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.IssueDTO
{
	public class UpdateIssuesDTO
	{
		public int Id { get; set; }
		public int IdPos { get; set; }
		public string PosName { get; set; }
		public int IdType { get; set; }
		public string IssueType { get; set; }
		public int IdStatus { get; set; }
		public int IdSubType { get; set; }
		public string SubType { get; set; }
		public int IdProblem { get; set; }
		public string Problem { get; set; }
		//public int ProblemId { get; set; }
		public string Status { get; set; }
		public string Priority { get; set; }
		public int PriorityId { get; set; }
		public string Solution { get; set; }
		public string Memo { get; set; }
		public int IdUserCreated { get; set; }
		public int IdUserType { get; set; }
		public string UserType { get; set; }
		public string UserName { get; set; }
		public string ProblemDescription { get; set; }
		public long CreationDate { get; set; }
		public string PosTelephone { get; set; }
		public string PosCellPhone { get; set; }
		public string PosAddress { get; set; }

	}
}
