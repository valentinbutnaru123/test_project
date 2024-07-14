using DAL.Entities;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.PosDTO;
using BBL.DTO.PosDTO;
using BBL.Common;

namespace BLL.Repositories.Pos
{
	public interface IPosRepository
	{
		int AddPos(AddPOSDTO addPos);
		GetPossDTO QueryPos(QueryPaginatedRequestDTO criteria);
		GetPosDTO GetPosById(int id);
        List<GetPosDTO> GetAllPos();
		void UpdatePos(UpdatePosDTO updatePos);
		void DeletePos(int Id);
		IQueryable<PosEntity> GetValidPos();
		List<GetConnectionsTypeDTO> GetAllConnectionType();
		List<GetCitiesDTO> GetAllCitites();

	}
}
