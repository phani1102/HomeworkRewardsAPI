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
    public class RewardManager :IRewardManager
    {
        private readonly IRewardData _data;
        ILogger<RewardManager> logger;
        public RewardManager(IRewardData rewardData, ILogger<RewardManager> logger)
        {
            this.logger = logger;
            _data = rewardData;
        }
        public Task<List<RewardsDTO>> GetRewardsByUser(long userId)
        {
            return Task.Run(() => _data.GetRewardsByUser(userId));
        }

        public Task<bool> UpdateRewardRedemptionDate(long userId, long taskId)
        {
            return Task.Run(() => _data.UpdateRewardRedemptionDate(userId, taskId));
        }
    }
}
