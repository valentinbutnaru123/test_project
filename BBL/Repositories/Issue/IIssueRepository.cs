using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBL.Common;
using BBL.DTO.IssueDTO;
using BLL.DTO.IssueDTO;
using DAL.Entities;


namespace BLL.Repositories.Issue
{
	public interface IIssueRepository
	{
		GetIssuesStatusDTO GetIssuesStatus();
		int AddIssue(AddIssuesDTO issue);
		GetIssuesDTO GetIssueById(int id);
		List<GetIssuesDTO> GetAllIssues();
		void UpdateIssue(UpdateIssuesDTO issue);
		void DeleteIssue(int id);
		IQueryable<IssueEntity> GetValidIssues();
		GetIssuessDTO QueryIssue(QueryPaginatedRequestDTO criteria);
		List<GetIssuesTypeDTO> GetAllIssuesType();
		List<GetAllPriority> GetPriority();
		List<GetAllStatus> GetStatuses();
		List<GetAllUserType> GetUserType();
		GetLogsDTO QueryLogs(QueryPaginatedRequestDTO criteria);

	}
}
