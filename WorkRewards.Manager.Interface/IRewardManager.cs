using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkRewards.DTO;

namespace WorkRewards.Manager.Interface
{
    public interface IRewardManager
    {
        Task<List<RewardsDTO>> GetRewardsByUser(long userId);
    }
}
