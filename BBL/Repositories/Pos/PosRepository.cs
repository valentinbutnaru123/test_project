using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DAL.Entities;
using BLL.DTO.PosDTO;
using System.Runtime.InteropServices;
using BBL.DTO.PosDTO;
using BBL.Common;
using BLL.DTO.UserDTO;


namespace BLL.Repositories.Pos
{
	public class PosRepository : IPosRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public PosRepository(ApplicationDbContext _dbContext)
		{
			this._dbContext = _dbContext;
		}


		public GetPossDTO QueryPos(QueryPaginatedRequestDTO criteria)
		{
			var queryable = GetValidPos();

			if (!string.IsNullOrEmpty(criteria.SearchValue))
			{
				var search = criteria.SearchValue.ToLower();
				queryable = queryable.Where(x => x.Name.ToLower().Contains(search) ||
												 x.Telephone.ToLower().Contains(search) ||
												 x.Address.ToLower().Contains(search) ||
												 x.Id.ToString().Contains(search) ||
												 x.CellPhone.ToString().Contains(search));

			}
			if (!string.IsNullOrEmpty(criteria.OrderBy))
			{
				var orderByDesc = !string.IsNullOrEmpty(criteria.Direction) && criteria.Direction.ToLower() == "desc";
				var orderBy = criteria.OrderBy.Replace(" ", "").ToLower();

				switch (orderBy)
				{
					case "id":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Id)
							: queryable.OrderBy(x => x.Id);
						break;
					case "posname":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Name)
							: queryable.OrderBy(x => x.Name);
						break;
					case "telephone":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Telephone)
							: queryable.OrderBy(x => x.Telephone);
						break;
					case "address":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.Address)
							: queryable.OrderBy(x => x.Address);
						break;
					case "cellphone":
						queryable = orderByDesc
							? queryable.OrderByDescending(x => x.CellPhone)
							: queryable.OrderBy(x => x.CellPhone);
						break;
				}
			}

			var filteredCount = queryable.Count();

			var views = queryable.AsEnumerable().Skip(criteria.Page.Value).Take(criteria.PageSize.Value).ToList();

			var result = new GetPossDTO
			{
				Total = GetValidPos().Count(),
				TotalFiltered = filteredCount,
				PossDTO = new List<GetPosDTO>(views.Select(x => new GetPosDTO
				{
					Id = x.Id,
					Name = x.Name,
					Telephone = x.Telephone,
					Address = x.Address,
					CellPhone = x.CellPhone,
					Status = x.Issues.Count == 0 ? "No issues" : $"{x.Issues.Count} active issues"
				}))
			};

			return result;

		}

		public int AddPos(AddPOSDTO command)
		{
			var posEntity = new PosEntity()
			{
				Name = command.Name,
				Telephone = command.Telephone,
				CellPhone = command.CellPhone,
				Address = command.Address,
				City_Id = command.City_Id,
				Brand = command.Brand,
				Model = command.Model,
				ConnType_Id = command.ConnType_Id,
				MorningClosing = command.MorningClosing,
				MorningOperning = command.MorningOperning,
				AfternonClosing = command.AfternonClosing,
				AfternoonOpening = command.AfternoonOpening

			};

			_dbContext.Pos.Add(posEntity);
			_dbContext.SaveChanges();


			foreach (var day in command.SelectedDays)
			{
				var weekEntity = new WeekDaysPos()
				{
					IdPos = posEntity.Id,
					WeekDays = day
				};

				_dbContext.WeekDaysPOS.Add(weekEntity);

			}
			_dbContext.SaveChanges();
			return posEntity.Id;


		}

		public GetPosDTO GetPosById(int id)
		{

			var posEntity = _dbContext.Pos.Include(x => x.WeekDaysPos).FirstOrDefault(u => u.Id == id);

			var result = new GetPosDTO()
			{
				Id = posEntity.Id,
				Name = posEntity.Name,
				Telephone = posEntity.Telephone,
				Address = posEntity.Address,
				CellPhone = posEntity.CellPhone,
				Brand = posEntity.Brand,
				Model = posEntity.Model,
				City_Id = posEntity.City_Id,
				ConnType_Id = posEntity.ConnType_Id,
				MorningClosing = posEntity.MorningClosing,
				MorningOperning = posEntity.MorningOperning,
				AfternonClosing = posEntity.AfternonClosing,
				AfternoonOpening = posEntity.AfternoonOpening,
				SelectedDays = posEntity.WeekDaysPos.Select(x => x.WeekDays).ToList()

			};

			return result;
		}

		public List<GetPosDTO> GetAllPos()
		{

			var posEntities = _dbContext.Pos.ToList();
			var result = new List<GetPosDTO>();

			foreach (var i in posEntities)
			{

				result.Add(new GetPosDTO()
				{
					Id = i.Id,
					Name = i.Name,
					Telephone = i.Telephone,
					Address = i.Address
					//status
				});

			}
			return result;

		}

		public void UpdatePos(UpdatePosDTO updatePos)
		{

			var pos = _dbContext.Pos.FirstOrDefault(x => x.Id == updatePos.Id);

			if (pos == null)
			{
				return;
			}

			pos.Name = updatePos.Name;
			pos.Telephone = updatePos.Telephone;
			pos.CellPhone = updatePos.CellPhone;
			pos.Address = updatePos.Address;
			pos.City_Id = updatePos.City_Id;
			pos.Brand = updatePos.Brand;
			pos.Model = updatePos.Model;
			pos.ConnType_Id = updatePos.ConnType_Id;
			pos.MorningOperning = updatePos.MorningOperning;
			pos.MorningClosing = updatePos.MorningClosing;
			pos.AfternonClosing = updatePos.AfternonClosing;
			pos.AfternoonOpening = updatePos.AfternoonOpening;

			_dbContext.Entry(pos).State = System.Data.Entity.EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void DeletePos(int id)
		{
			var pos = _dbContext.Pos.FirstOrDefault(x => x.Id == id);

			if (pos == null)
			{
				return;
			}
			pos.DeleteAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

			_dbContext.Entry(pos).State = System.Data.Entity.EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public IQueryable<PosEntity> GetValidPos()
		{
			return _dbContext.Pos
				.Include(x => x.Issues)
				.Include(x => x.Cities)
				.Include(x => x.ConnectionType)
				.Include(x => x.WeekDaysPos)
				.Where(x => x.DeleteAt == null);
		}


		public List<GetCitiesDTO> GetAllCitites()
		{
			var cityEntity = _dbContext.Cities.ToList();

			var listCities = new List<GetCitiesDTO>();

			foreach (var city in cityEntity)
			{
				listCities.Add(new GetCitiesDTO
				{

					Id = city.Id,
					Name = city.CityName
				});
			}
			return listCities;
		}

		public List<GetConnectionsTypeDTO> GetAllConnectionType()
		{
			var conType = _dbContext.ConnectionTypes.ToList();
			var listConType = new List<GetConnectionsTypeDTO>();

			foreach (var type in conType)
			{
				listConType.Add(new GetConnectionsTypeDTO
				{
					Id = type.Id,
					ConectionType = type.ConnectionType
				});
			}
			return listConType;
		}




	}
}
