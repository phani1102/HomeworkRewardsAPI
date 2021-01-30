using System;
using System.Collections.Generic;
using System.Text;
using WorkRewards.DTO;

namespace WorkRewards.Data.Interface
{
    public interface IRewardData
    {
        List<RewardsDTO> GetRewardsByUser(long userId);

        bool UpdateRewardRedemptionDate(long userId, long taskId);
    }
}
