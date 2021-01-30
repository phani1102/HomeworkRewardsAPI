using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkRewards.Data.Interface;
using WorkRewards.DTO;
using WorkRewards.Manager.Interface;

namespace WorkRewards.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserData _data;
        ILogger<UserManager> logger;
        public UserManager(IUserData userData, ILogger<UserManager> logger)
        {
            this.logger = logger;
            _data = userData;
        }


        public Task<bool> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            return Task.Run(() => _data.ChangePassword(userId, oldPassword, newPassword));
        }

        public Task<List<RoleDTO>> GetRoles(int? roleId)
        {
            return Task.Run(() => _data.GetRoles(roleId));
        }

        public Task<int> RegisterUser(UserDetailsDTO user)
        {
            return Task.Run(() => _data.RegisterUser(user));
        }

        public Task<UserDetailsDTO> ValidateUser(UserDetailsDTO user)
        {
            return Task.Run(() => _data.ValidateUser(user));
        }
    }
}
