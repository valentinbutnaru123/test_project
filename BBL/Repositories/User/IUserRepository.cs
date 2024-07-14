
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BBL.Common;
using BLL.DTO.UserDTO;
using BBL.DTO.UserDTO;

namespace BLL.Repositories.User
{
    public interface IUserRepository
    {
        int AddUser(AddUserDTO addUser);
        GetUsersDTO GetAllUsers();
        GetUserDTO GetUserById(int id);
        void UpdateUser(UpdateUserDTO updateUser);
        void DeleteUser(int Id);
        GetUserDTO LoginUser(string Login, string Password);
        bool ExistUserByEmail(string email);
        IQueryable<UserEntity> GetValidUser();
        bool ExistLogin(string login);
        List<GetUsersTypeDTO> GetAllUsersType();
        GetUsersDTO QueryUsers(QueryPaginatedRequestDTO criteria);
    }
}
