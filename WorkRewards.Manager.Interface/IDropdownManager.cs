using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkRewards.DTO;

namespace WorkRewards.Manager.Interface
{
    public interface IDropdownManager
    {
        Task<List<UserDetailsDTO>> GetStudentListByParentOrTeacher(int userId);
        Task<List<TaskStatusDTO>> GetTaskStatus(int? roleId);
        Task<RewardDetailsDTO> GetRewards(int? rewardId);
    }
}
