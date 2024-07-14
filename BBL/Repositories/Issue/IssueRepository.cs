using System;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO.IssueDTO;
using DataAccessLayer;
using DAL.Entities;
using BBL.Common;
using BBL.DTO.IssueDTO;
using System.Data.Entity;
using BBL.Helpers;

namespace BLL.Repositories.Issue
{
	public class IssueRepository : IIssueRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public IssueRepository(ApplicationDbContext _dbContext)
		{
			this._dbContext = _dbContext;
		}

		public GetIssuesStatusDTO GetIssuesStatus()
		{
			var issuesStatus = GetValidIssues().GroupBy(x => x.Status).Select(x => new { Status = x.Key, Count = x.Count() }).ToList();

			return new GetIssuesStatusDTO
			{
				IssuesStatuses = new List<GetIssueStatusDTO>(issuesStatus.Select(x => new GetIssueStatusDTO
				{
					Status = x.Status.Status,
					TotalIssues = x.Count
				}))
			};
		}

		public GetIssuessDTO QueryIssue(QueryPaginatedRequestDTO criteria)
		{
			var queryable = GetValidIssues();

			if (!string.IsNullOrEmpty(criteria.SearchValue))
			{
				var search = criteria.SearchValue.ToLower();
				queryable = queryable.Where(x => x.Id.ToString().Contains(search) ||
												 x.Pos.Id.ToString().Contains(search) ||
												 x.Pos.Name.ToLower().Contains(search) ||
												 x.User.Name.ToLower().Contains(search) ||
												 x.CreationDate.ToString().Contains(search) ||
												 x.IssuesType.Name.ToLower().Contains(search) ||
												 x.Status.Status.ToLower().Contains(search) ||
												 x.UserType.UserType.ToLower().Contains(search) ||
												 x.Memo.ToLower().Contains(search));
			}

			if (!string.IsNullOrEmpty(criteria.OrderBy))
			{
				var orderByDesc = !string.IsNullOrEmpty(criteria.Direction) && criteria.Direction.ToLower() == "desc";
				var orderBy = criteria.OrderBy.Replace(" ", "").ToLower();

				switch (orderBy)
				{
					case "issue":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Id)
							: queryable.OrderBy(x => x.Id);
						break;
					case "pos.id":
						queryable = orderByDesc
						   ? queryable.OrderByDescending(x => x.Pos.Id)
						   : queryable.OrderBy(x => x.Pos.Id);
						break;
					case "pos.name":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Pos.Name)
							: queryable.OrderBy(x => x.Pos.Name);
						break;
					case "createdby":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.User.Name)
							: queryable.OrderBy(x => x.User.Name);
						break;
					case "date":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.CreationDate)
							: queryable.OrderBy(x => x.CreationDate);
						break;
					case "issuestype":
						queryable = orderByDesc
						   ? queryable.OrderByDescending(x => x.IssuesType.Name)
						   : queryable.OrderBy(x => x.IssuesType.Name);
						break;
					case "status":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Status.Status)
							: queryable.OrderBy(x => x.Status.Status);
						break;
					case "assignedto":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.UserType.UserType)
							: queryable.OrderBy(x => x.UserType.UserType);
						break;
					case "memo":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Memo)
							: queryable.OrderBy(x => x.Memo);
						break;

				}
			}

			var filteredCount = queryable.Count();

			var views = queryable.AsEnumerable().Skip(criteria.Page.Value).Take(criteria.PageSize.Value).ToList();

			var result = new GetIssuessDTO
			{

				Total = GetValidIssues().Count(),
				TotalFiltered = filteredCount,
				IssueDTO = new List<GetIssuesDTO>(views.Select(x => new GetIssuesDTO
				{
					Id = x.Id,
					IdPos = x.IdPos,
					PosName = x.Pos.Name,
					UserName = x.User.Name,
					CreationDate = x.CreationDate,
					IssueType = x.IssuesType.Name,
					Status = x.Status.Status,
					UserType = x.UserType.UserType,
					Memo = x.Memo,

				}))

			};

			return result;
		}




		public GetLogsDTO QueryLogs(QueryPaginatedRequestDTO criteria)
		{
			var queryable = GetValidLog();

			if (!string.IsNullOrEmpty(criteria.SearchValue))
			{
				var search = criteria.SearchValue.ToLower();
				queryable = queryable.Where(x => x.InsertDate.ToString().Contains(search) ||
												 x.Action.ToString().Contains(search) ||
												 x.User.Name.ToLower().Contains(search) ||
												 x.Notes.ToLower().Contains(search));

			}

			if (!string.IsNullOrEmpty(criteria.OrderBy))
			{
				var orderByDesc = !string.IsNullOrEmpty(criteria.Direction) && criteria.Direction.ToLower() == "desc";
				var orderBy = criteria.OrderBy.Replace(" ", "").ToLower();

				switch (orderBy)
				{
					case "insertdate":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.InsertDate)
							: queryable.OrderBy(x => x.InsertDate);
						break;
					case "action":
						queryable = orderByDesc
						   ? queryable.OrderByDescending(x => x.Action)
						   : queryable.OrderBy(x => x.Action);
						break;
					case "user":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.User.Name)
							: queryable.OrderBy(x => x.User.Name);
						break;
					case "notes":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Notes)
							: queryable.OrderBy(x => x.Notes);
						break;

				}
			}

			var filteredCount = queryable.Count();

			var views = queryable.AsEnumerable().Skip(criteria.Page.Value).Take(criteria.PageSize.Value).ToList();

			var result = new GetLogsDTO
			{

				Total = GetValidLog().Count(),
				TotalFiltered = filteredCount,
				Logs = new List<GetLogDTO>(views.Select(x => new GetLogDTO
				{
					Id = x.Id,
					InsertDate = x.InsertDate.ConvertToDateTimeOffsetToStringDate(),
					Action = x.Action,
					User = x.User.Name,
					Notes = x.Notes,

				}))

			};

			return result;
		}



		public int AddIssue(AddIssuesDTO issue)
		{

			var issueEntity = new IssueEntity()
			{
				IdPos = issue.IdPos,
				IdType = issue.IdType,
				IdSubType = issue.IdSubType,
				IdProblem = issue.IdProblem,
				PriorityId = issue.PriorityId,
				IdStatus = issue.IdStatus,
				Description = issue.Description,
				Solotion = issue.Solution,
				IdUserCreated = issue.IdUserCreated,
				IdUserType = issue.IdUserType,
				AssignedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				CreationDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				Memo = issue.Memo


			};

			_dbContext.Issues.Add(issueEntity);
			_dbContext.SaveChanges();

			var logs = new LogEntity()
			{
				IdIssue = issueEntity.Id,
				InsertDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				Action = "Added",
				IdUser = issue.IdUserCreated,
				Notes = "log"

			};
			_dbContext.Logs.Add(logs);
			_dbContext.SaveChanges();

			return issueEntity.Id;
		}

		public GetIssuesDTO GetIssueById(int id)
		{

			var issueEntity = GetValidIssues().FirstOrDefault(x => x.Id == id);
			var result = new GetIssuesDTO()
			{
				Id = issueEntity.Id,
				IdPos = issueEntity.Pos.Id,
				PosName = issueEntity.Pos.Name,
				PosTelephone = issueEntity.Pos.Telephone,
				PosCellPhone = issueEntity.Pos.CellPhone,
				PosAddress = issueEntity.Pos.Address,
				IdUserCreated = issueEntity.IdUserCreated,
				CreationDate = issueEntity.CreationDate,
				IdType = issueEntity.IdType,
				IdSubType = issueEntity.IdSubType,
				IdProblem = issueEntity.IdProblem,
				IdStatus = issueEntity.IdStatus,
				PriorityId = issueEntity.PriorityId,
				Solution = issueEntity.Solotion,
				ProblemDescription = issueEntity.Description,
				IdUserType = issueEntity.IdUserType,
				UserType = issueEntity.UserType.UserType,
				Memo = issueEntity.Memo
			};
			return result;
		}

		public List<GetIssuesDTO> GetAllIssues()
		{
			var issuesEntity = GetValidIssues();
			var result = new List<GetIssuesDTO>();

			foreach (var x in issuesEntity)
			{
				result.Add(new GetIssuesDTO
				{
					Id = x.Id,
					IdPos = x.IdPos,
					PosName = x.Pos.Name,
					IdType = x.IdType,
					IssueType = x.IssuesType.Name,
					IdSubType = x.IdSubType,
					SubType = x.IssuesType.Name,
					IdProblem = x.IdProblem,
					Priority = x.Priority.PriorityName,
					Solution = x.Solotion,
					IdStatus = x.Status.Id,
					Status = x.Status.Status,
					PriorityId = x.Priority.Id,
				});

			}
			return result;

		}


		public List<GetIssuesTypeDTO> GetAllIssuesType()
		{
			var issueType = _dbContext.IssuesType.ToList();
			var result = new List<GetIssuesTypeDTO>();

			foreach (var x in issueType)
			{
				result.Add(new GetIssuesTypeDTO
				{
					Id = x.Id,
					IssueLevel = x.IssueLevel,
					ParentIssues = x.ParentIssues,
					Name = x.Name,



				});
			}
			return result;
		}

		public List<GetAllPriority> GetPriority()
		{
			var priority = _dbContext.Priorities.ToList();
			var result = new List<GetAllPriority>();

			foreach (var x in priority)
			{
				result.Add(new GetAllPriority
				{
					Id = x.Id,
					Priority = x.PriorityName
				});
			}
			return result;
		}


		public List<GetAllStatus> GetStatuses()
		{
			var status = _dbContext.Statuses.ToList();
			var result = new List<GetAllStatus>();

			foreach (var x in status)
			{
				result.Add(new GetAllStatus
				{
					Id = x.Id,
					Status = x.Status
				});
			}
			return result;
		}

		public void UpdateIssue(UpdateIssuesDTO updateIssue)
		{

			var issueEntity = _dbContext.Issues.FirstOrDefault(x => x.Id == updateIssue.Id);

			if (issueEntity == null)
			{
				return;
			}

			if (issueEntity.IdType != updateIssue.IdType)
			{
				issueEntity.IdType = updateIssue.IdType;
			}

			if (issueEntity.IdSubType != updateIssue.IdSubType)
			{
				issueEntity.IdSubType = updateIssue.IdSubType;
			}

			if (issueEntity.IdProblem != updateIssue.IdProblem)
			{
				issueEntity.IdProblem = updateIssue.IdProblem;
			}

			if (issueEntity.Description != updateIssue.ProblemDescription)
			{
				issueEntity.Description = updateIssue.ProblemDescription;
			}

			if (issueEntity.PriorityId != updateIssue.PriorityId)
			{
				issueEntity.PriorityId = updateIssue.PriorityId;
			}

			if (issueEntity.IdStatus != updateIssue.IdStatus)
			{
				issueEntity.IdStatus = updateIssue.IdStatus;
			}

			if (issueEntity.Solotion != updateIssue.Solution)
			{
				issueEntity.Solotion = updateIssue.Solution;
			}

			if (issueEntity.IdUserType != updateIssue.IdUserType)
			{
				issueEntity.IdUserType = updateIssue.IdUserType;
			}

			if (issueEntity.Memo != updateIssue.Memo)
			{
				issueEntity.Memo = updateIssue.Memo;
			}


			_dbContext.Entry(issueEntity).State = System.Data.Entity.EntityState.Modified;
			_dbContext.SaveChanges();

			var updateLog = new LogEntity()
			{
				IdIssue = issueEntity.Id,
				InsertDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				Action = "Update",
				IdUser = issueEntity.IdUserCreated,
				Notes = "closed"

			};
			_dbContext.Logs.Add(updateLog);
			_dbContext.SaveChanges();
		}

		public void DeleteIssue(int id)
		{
			var issue = _dbContext.Issues.FirstOrDefault(x => x.Id == id);

			if (issue == null)
			{
				return;
			}

			issue.DeleteAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			_dbContext.Entry(issue).State = System.Data.Entity.EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public IQueryable<IssueEntity> GetValidIssues()
		{
			return _dbContext.Issues.Include(x => x.Pos)
									.Include(x => x.IssuesType)
									.Include(x => x.Status)
									.Include(x => x.Priority).Where(u => u.DeleteAt == null);

		}

		public IQueryable<LogEntity> GetValidLog()
		{
			return _dbContext.Logs.Include(x => x.User).Where(u => u.DeleteAt == null);

		}

		public List<GetAllUserType> GetUserType()
		{
			var userType = _dbContext.UserTypes.ToList();
			var result = new List<GetAllUserType>();

			foreach (var x in userType)
			{
				result.Add(new GetAllUserType
				{
					Id = x.Id,
					UserType = x.UserType
				});
			}
			return result;
		}

	}
}
