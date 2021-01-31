using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkRewards.Manager.Interface;

namespace WorkRewardsAPI.Controllers
{
    //[Authorize]
    [Route("v1")]
    [ApiController]
    public class RewardController : ControllerBase
    {
        ILogger logger;
        private IRewardManager _rewardManager;

        public RewardController(IRewardManager rewardManager, ILoggerFactory deploggerFactory)
        {
            this.logger = deploggerFactory.CreateLogger("Controllers.RewardController");
            _rewardManager = rewardManager;
        }

        [HttpGet]
        [Route("GetRewardsByUser/{userId}")]
        public async Task<ActionResult> GetRewardsByUser(long userId)
        {
            try
            {
                var result = await _rewardManager.GetRewardsByUser(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException.ToString());
            }
            return null;
        }

        [HttpPost]
        [Route("UpdateRewardRedemptionDate")]
        public async Task<ActionResult> UpdateRewardRedemptionDate(long userId, long taskId)
        {
            try
            {
                var result = await _rewardManager.UpdateRewardRedemptionDate(userId, taskId);
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
