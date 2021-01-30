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
    public class DropdownManager : IDropdownManager
    {
        private readonly IDropdownData _data;
        ILogger<DropdownManager> logger;
        public DropdownManager(IDropdownData userData, ILogger<DropdownManager> logger)
        {
            this.logger = logger;
            _data = userData;
        }
        public Task<List<UserDetailsDTO>> GetStudentListByParentOrTeacher(int userId)
        {
            return Task.Run(() => _data.GetStudentListByParentOrTeacher(userId));
        }

        public Task<List<TaskStatusDTO>> GetTaskStatus(int? roleId)
        {
            return Task.Run(() => _data.GetTaskStatus(roleId));
        }

        public Task<RewardDetailsDTO> GetRewards(int? rewardId)
        {
            return Task.Run(() => _data.GetRewards(rewardId));
        }
    }
}
