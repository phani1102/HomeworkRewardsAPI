using System;
using System.Collections.Generic;
using System.Text;
using WorkRewards.DTO;

namespace WorkRewards.Data.Interface
{
    public interface IUserData
    {
        bool ChangePassword(int userId, string oldPassword, string newPassword);
        UserDetailsDTO ValidateUser(UserDetailsDTO user);
        List<RoleDTO> GetRoles(int? roleId);
        long RegisterUser(UserDetailsDTO user);
        bool UpdateProfile(UserDetailsDTO user);
    }
}
