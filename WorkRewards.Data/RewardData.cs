using WorkRewards.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using WorkRewards.Data.Interface;
using WorkRewards.DTO;
using WorkRewards.Data.Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace WorkRewards.Data
{
    public class RewardData : BaseData, IRewardData
    {
        private readonly string ConnectionString;
        SummitDBUtils dbUtil;//= new SummitDBUtils();
        ILogger<UserData> logger;
        IConfiguration config;

        public RewardData(IConfiguration _config, ILogger<UserData> logger)
        {
            this.logger = logger;
            config = _config;
            this.ConnectionString = config.GetSection("AppSettings").GetSection("ConnectionString").Value;
            //dbUtil.ConnectionString = this.CbmsConnectionString;
            dbUtil = new SummitDBUtils(_config, logger);
        }


    
        public List<RewardsDTO> GetRewardsByUser(long userId)
        {
            SqlParameter[] spParams;
            List<RewardsDTO> lstRewardDetails = new List<RewardsDTO>();

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@User_Id ",userId),
                };
                var res = dbUtil.ExecuteSQLQuery("Rewards_By_User_Get", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        var query = from objdata in res.Tables[0].AsEnumerable()
                                    select new RewardsDTO()
                                    {
                                        TaskId = objdata.Field<long>("Task_Id"),
                                        RewardId= objdata.Field<long>("Reward_Id"),
                                        RewardName = objdata.Field<string>("Reward_Name"),
                                        Description= objdata.Field<string>("Description"),
                                        RewardStatus= objdata.Field<string>("Reward_Status")
                                    };
                        lstRewardDetails = query.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("GetRewardsByUser" + " " + ex.Message.ToString());
            }
            return lstRewardDetails;
        }

        public bool UpdateRewardRedemptionDate(long userId, long taskId)
        {
            SqlParameter[] spParams;
            bool isSuccess = false;

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@Task_Id", taskId),
                };
                var res = dbUtil.ExecuteSQLQuery("Task_Reward_Redemption_Date_Update", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        if (res.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
                        {
                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("UpdateRewardRedemptionDate" + " " + ex.Message.ToString());
            }
            return isSuccess;
        }
    }
}
