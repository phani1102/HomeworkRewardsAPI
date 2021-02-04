using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkRewards.DTO;
using WorkRewards.Manager.Interface;

namespace WorkRewardsAPI.Controllers
{
    [Route("v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ILogger logger;
        private IUserManager _user;

        public UserController(IUserManager user, ILoggerFactory deploggerFactory)
        {
            this.logger = deploggerFactory.CreateLogger("Controllers.BudgetAccountsController");
            _user = user;
        }

        /// <summary>
        /// Get GetRoles
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoles/{roleId:int}")]
        [Route("GetRoles")]
        public async Task<ActionResult> GetRoles(int? roleId)
        {
            try
            {
                var result = await _user.GetRoles(roleId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }

        /// <summary>
        /// RegisterUser
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromBody]UserDetailsDTO requestObj)
        {
            try
            {
                var result = await _user.RegisterUser(requestObj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }

        /// <summary>
        /// ValidateUser
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost]
        
        public async Task<ActionResult> ValidateUser([FromBody]UserDetailsDTO requestObj)
        {
            try
            {
                var result = await _user.ValidateUser(requestObj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }

        /// <summary>
        /// ValidateUser
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword(int userId,string oldPwd,string newPwd)
        {
            try
            {
                var result = await _user.ChangePassword(userId,oldPwd,newPwd);
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