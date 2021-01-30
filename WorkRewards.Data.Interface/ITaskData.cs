using System;
using System.Collections.Generic;
using System.Text;
using WorkRewards.DTO;

namespace WorkRewards.Data.Interface
{
    public interface ITaskData
    {
        List<TaskDTO> AssignedTaskDetailsByUserAndStatusGet(TaskRequestDTO task);
        bool UpdateTask(TaskRequestDTO task);
        bool CreateTask(TaskDTO task);
        bool UpdateTaskDetails(TaskDTO task);
        bool ApproveTask(int userId, int taskId);
    }
}
