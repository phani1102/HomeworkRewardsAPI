using System;
using System.Collections.Generic;
using System.Text;
using WorkRewards.DTO;

namespace WorkRewards.Data.Interface
{
    public interface IDropdownData
    {
        List<UserDetailsDTO> GetStudentListByParentOrTeacher(int userId);
        List<TaskStatusDTO> GetTaskStatus(int? roleId);
        RewardDetailsDTO GetRewards(int? rewardId);
    }
}
