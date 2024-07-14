using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DAL.Entities;
using BLL.Repositories.User;
using System.Text.RegularExpressions;
using System;
using BLL.DTO.UserDTO;
using DAL.Common;
using System.Data.Entity;
using BBL.DTO.UserDTO;
using System.Security.AccessControl;
using BBL.Common;


namespace BLL.Repositories
{
    public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext _context)
		{
		   this._context = _context;
		}

		public GetUsersDTO QueryUsers(QueryPaginatedRequestDTO criteria)
		{
			var queryable = GetValidUser();

			if (!string.IsNullOrEmpty(criteria.SearchValue))
			{
				var search = criteria.SearchValue.ToLower();
				queryable = queryable.Where(x => x.Name.ToLower().Contains(search) ||
				                                 x.Email.ToLower().Contains(search) ||
				                                 x.Login.ToLower().Contains(search) ||
				                                 x.Telephone.ToLower().Contains(search) ||
				                                 x.UserType.UserType.ToLower().Contains(search));
			}

			if (!string.IsNullOrEmpty(criteria.OrderBy))
			{
				var orderByDesc = !string.IsNullOrEmpty(criteria.Direction) && criteria.Direction.ToLower() == "desc";
				var orderBy = criteria.OrderBy.Replace(" ", "").ToLower();

				switch (orderBy)
				{
					case "name":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Name)
							: queryable.OrderBy(x => x.Name);
						break;
					case "email":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Email)
							: queryable.OrderBy(x => x.Email);
						break;
					case "login":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Login)
							: queryable.OrderBy(x => x.Login);
						break;
					case "telephone":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Telephone)
							: queryable.OrderBy(x => x.Telephone);
						break;
					case "usertypename":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.UserType.UserType)
							: queryable.OrderBy(x => x.UserType.UserType);
						break;
				}
			}

			var filteredCount = queryable.Count();

			var views = queryable.AsEnumerable().Skip(criteria.Page.Value).Take(criteria.PageSize.Value).ToList();

			var result = new GetUsersDTO
			{
				Total = GetValidUser().Count(),
				TotalFiltered = filteredCount,
				UserDTO = new List<GetUserDTO>(views.Select(x => new GetUserDTO
				{
					Id = x.Id,
					Name = x.Name,
					Email = x.Email,
					Login = x.Login,
					Telephone = x.Telephone,
					UserTypeId = x.UserTypeId,
					UserType = x.UserType.UserType,
				}))
			};

			return result;
		}
		
		public int AddUser(AddUserDTO command)
		{
			byte[] salt = PasswordHasher.GenerateSalt();
			byte[] hash = PasswordHasher.HashPassword(command.Password, salt);

			var userEntity = new UserEntity()
			{
				Name = command.Name,
				Email= command.Email,
				Salt = salt,
				PasswordHash = hash,
				Login=command.Login,
				Telephone=command.Telephone,	
				UserTypeId = command.UserTypeId,	
			};
			_context.Users.Add(userEntity);
			_context.SaveChanges();	
			return userEntity.Id;	
		}

		public GetUsersDTO GetAllUsers() 
		{
			var result = new GetUsersDTO
			{
				UserDTO = new List<GetUserDTO>()
			};
			var usersEntity= GetValidUser().ToList();

			foreach(var x in usersEntity)
			{
				result.UserDTO.Add(new GetUserDTO
				{
					Id = x.Id,
					Name = x.Name,
					Email = x.Email,
					Login = x.Login,
					Telephone = x.Telephone,
					UserTypeId = x.UserTypeId,
					UserType = x.UserType.UserType,
				}); 
			}

			return result;
		}

		public GetUserDTO GetUserById(int id)
		{
			var userEntity= GetValidUser().FirstOrDefault(u => u.Id == id);
			//verificare null?
			var result = new GetUserDTO()
			{
				Id = userEntity.Id,
				Name = userEntity.Name,
				Email = userEntity.Email,
				Login = userEntity.Login,
				Telephone = userEntity.Telephone,
				UserTypeId= userEntity.UserTypeId,
				UserType = userEntity.UserType.UserType
			};

			return result;
		}

		public void UpdateUser(UpdateUserDTO updateUser)
		{
			var user = _context.Users.FirstOrDefault(x => x.Id == updateUser.Id);
			if (user == null)
			{
				return;
			}

	        user.Name = updateUser.Name;
			user.Telephone = updateUser.Telephone;
			user.UserTypeId = updateUser.UserTypeId;
			user.Email = updateUser.Email;

			_context.Entry(user).State = System.Data.Entity.EntityState.Modified;
			_context.SaveChanges();
		}

		public void DeleteUser(int id)
		{
			var user = GetValidUser().FirstOrDefault(x => x.Id == id);

			if (user == null) 
			{
				return;
			}

			user.DeleteAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

			_context.Entry(user).State = System.Data.Entity.EntityState.Modified;
			_context.SaveChanges();
		}
		
		public GetUserDTO LoginUser(string Login,string Password)
		{
			var userEntity = GetValidUser().FirstOrDefault(u => u.Login == Login);

			if (userEntity != null)
			{
				if (PasswordHasher.VerifyPassword(Password, userEntity.Salt, userEntity.PasswordHash))
				{

					return new GetUserDTO
					{
						Id = userEntity.Id,
						Name = userEntity.Name,
						Email = userEntity.Email,
						Login = userEntity.Login,
						UserTypeId = userEntity.UserTypeId,
						Telephone = userEntity.Telephone,
						UserType = userEntity.UserType.UserType
					};
				}

			}
			return null;
		}
		public bool ExistUserByEmail(string email)
		{
			return GetValidUser().Any(u => u.Email.ToLower() == email.ToLower());
			
		}
		public bool ExistLogin(string login)
		{
			return GetValidUser().Any(x => x.Login == login);
		}
		
		public IQueryable<UserEntity> GetValidUser()
		{
			return _context.Users
				.Include(u => u.UserType)
				.Where(x => x.DeleteAt == null);
		}

		public List<GetUsersTypeDTO> GetAllUsersType()
		{
			var usersType = _context.UserTypes.ToList();
			var result = new List<GetUsersTypeDTO>();

			foreach(var user in usersType) 
			{
				result.Add(new GetUsersTypeDTO
				{
					Id = user.Id,
					UserType=user.UserType
				});
			}
			return result;
		}
	}
}
