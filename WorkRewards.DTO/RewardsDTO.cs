using System;
using System.Collections.Generic;
using System.Text;

namespace WorkRewards.DTO
{
    public class RewardsDTO
    {
        public long RewardId { get; set; }
        public string RewardName { get; set; }
        public string Description { get; set; }
        public long? TaskId { get; set; }
        public string RewardStatus { get; set; }
     
    }

    public class RewardsImageDTO
    {
        public int RewardImageId { get; set; }
        public string Image { get; set; }
        public int RewardId { get; set; }

    }

    public class RewardDetailsDTO
    {
       public List<RewardsDTO> Reqards { get; set; }
        public List<RewardsImageDTO> ReqardImages { get; set; }

    }
}
