using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkRewards.Manager.Interface;

namespace WorkRewardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {

        ILogger logger;
        private IDropdownManager _dropdownManager;

        public DropdownController(IDropdownManager dropdownManager, ILoggerFactory deploggerFactory)
        {
            this.logger = deploggerFactory.CreateLogger("Controllers.BudgetAccountsController");
            _dropdownManager = dropdownManager;
        }

        [HttpGet]
        [Route("GetTaskStatus/{taskStatusId:int}")]
        [Route("GetTaskStatus")]
        public async Task<ActionResult> GetTaskStatus(int? taskStatusId)
        {
            try
            {
                var result = await _dropdownManager.GetTaskStatus(taskStatusId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }
        [HttpGet]
        [Route("GetStudentListByParentOrTeacher/{userId}")]
        public async Task<ActionResult> GetStudentListByParentOrTeacher(int userId)
        {
            try
            {
                var result = await _dropdownManager.GetStudentListByParentOrTeacher(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }
       [HttpGet]
        [Route("GetRewards/{rewarId}")]
        [Route("GetRewards")]
        public async Task<ActionResult> GetRewards(int? rewarId)
        {
            try
            {
                var result = await _dropdownManager.GetRewards(rewarId);
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