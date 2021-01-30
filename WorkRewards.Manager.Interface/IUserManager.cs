using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkRewards.DTO;

namespace WorkRewards.Manager.Interface
{
    public interface IUserManager
    {
        Task<bool> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<UserDetailsDTO> ValidateUser(UserDetailsDTO user);
        Task<List<RoleDTO>> GetRoles(int? roleId);
        Task<long> RegisterUser(UserDetailsDTO user);
    }
}
