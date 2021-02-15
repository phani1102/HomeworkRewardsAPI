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
    public class TaskManager : ITaskManager
    {
        private readonly ITaskData _data;
        ILogger<TaskManager> logger;
        public TaskManager(ITaskData userData, ILogger<TaskManager> logger)
        {
            this.logger = logger;
            _data = userData;
        }

        public Task<bool> ApproveTask(int userId, int taskId)
        {
            return Task.Run(() => _data.ApproveTask(userId, taskId));
        }

        public Task<List<TaskDTO>> AssignedTaskDetailsByUserAndStatusGet(TaskRequestDTO task)
        {
            return Task.Run(() => _data.AssignedTaskDetailsByUserAndStatusGet(task));
        }

        public Task<List<TaskDTO>> TasksPendingForApprovalByUser(long userId)
        {
            return Task.Run(() => _data.TasksPendingForApprovalByUser(userId));
        }

        public Task<bool> CreateTask(TaskDTO task)
        {
            return Task.Run(() => _data.CreateTask(task));
        }

        public Task<bool> UpdateTask(TaskRequestDTO task)
        {
            return Task.Run(() => _data.UpdateTask(task));
        }

        public Task<bool> UpdateTaskDetails(TaskDTO task)
        {
            return Task.Run(() => _data.UpdateTaskDetails(task));
        }
    }
}
