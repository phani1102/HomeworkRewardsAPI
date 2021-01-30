using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkRewards.DTO;

namespace WorkRewards.Manager.Interface
{
    public interface ITaskManager
    {
        Task<List<TaskDTO>> AssignedTaskDetailsByUserAndStatusGet(TaskRequestDTO task);
        Task<bool> UpdateTask(TaskRequestDTO task);
        Task<bool> CreateTask(TaskDTO task);
        Task<bool> UpdateTaskDetails(TaskDTO task);
        Task<bool> ApproveTask(int userId, int taskId);
    }
}
