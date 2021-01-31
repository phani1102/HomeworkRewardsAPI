using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WorkRewards.DTO;
using WorkRewards.Manager.Interface;

namespace WorkRewardsAPI.Controllers
{
    [Route("v1")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ILogger logger;
        private ITaskManager _taskManager;

        public TaskController(ITaskManager taskManager, ILoggerFactory deploggerFactory)
        {
            this.logger = deploggerFactory.CreateLogger("Controllers.TaskController");
            _taskManager = taskManager;
        }

        [HttpPost]
        [Route("AssignedTaskDetailsByUserAndStatusGet")]
        public async Task<ActionResult> AssignedTaskDetailsByUserAndStatusGet(TaskRequestDTO req)
        {
            try
            {
                var result = await _taskManager.AssignedTaskDetailsByUserAndStatusGet(req);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }
        [HttpPost]
        [Route("CreateTask")]
        public async Task<ActionResult> CreateTask(TaskDTO req)
        {
            try
            {
                var result = await _taskManager.CreateTask(req);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }
        [HttpPost]
        [Route("UpdateTask")]
        public async Task<ActionResult> UpdateTaskDetails(TaskDTO req)
        {
            try
            {
                var result = await _taskManager.UpdateTaskDetails(req);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }
        [HttpPost]
        [Route("UpdateTaskDetails")]
        public async Task<ActionResult> UpdateTask(TaskRequestDTO req)
        {
            try
            {
                var result = await _taskManager.UpdateTask(req);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }

        [HttpGet]
        [Route("ApproveTask/{userId}/{taskId}")]
        public async Task<ActionResult> ApproveTask(int userId, int taskId)
        {
            try
            {
                var result = await _taskManager.ApproveTask(userId, taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }
    }
}