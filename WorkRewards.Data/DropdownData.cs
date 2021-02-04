using WorkRewards.Data.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WorkRewards.DTO;
using System.Linq;
using WorkRewards.Data;
using WorkRewards.Data.Interface;

namespace WorkRewards.Data
{
    public class DropdownData : BaseData, IDropdownData
    {
        private readonly string ConnectionString;
        SummitDBUtils dbUtil;//= new SummitDBUtils();
        ILogger<UserData> logger;
        IConfiguration config;

        public DropdownData(IConfiguration _config, ILogger<UserData> logger)
        {
            this.logger = logger;
            config = _config;
            this.ConnectionString = config.GetSection("AppSettings").GetSection("ConnectionString").Value;
            //dbUtil.ConnectionString = this.ConnectionString;
            dbUtil = new SummitDBUtils(_config, logger);
        }


        public List<UserDetailsDTO> GetStudentListByParentOrTeacher(int userId)
        {
            SqlParameter[] spParams;
            List<UserDetailsDTO> userDetails = new List<UserDetailsDTO>();

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@User_Id ",userId),


                };
                var res = dbUtil.ExecuteSQLQuery("Mapped_Users_By_User_Get", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        var query = from objdata in res.Tables[0].AsEnumerable()
                                    select new UserDetailsDTO()
                                    {
                                        UserId = objdata.Field<long>("User_Id"),
                                        FullName = objdata.Field<string>("First_Name") + " " + objdata.Field<string>("Last_Name"),
                                        First_Name = objdata.Field<string>("First_Name"),
                                        Last_Name = objdata.Field<string>("Last_Name"),
                                        Middle_Name = objdata.Field<string>("Middle_Name"),
                                        UserName = objdata.Field<string>("Username"),
                                        Email = objdata.Field<string>("Email"),
                                        DOB = objdata.Field<DateTime>("DOB"),
                                        Gender = objdata.Field<string>("Gender"),
                                        GenderName = objdata.Field<string>("Gender").GetGender(),
                                        MobileNumber = objdata.Field<string>("Mobile_No"),
                                        RoleId = objdata.Field<int>("Role_Id"),
                                        RoleName = objdata.Field<string>("Role_Name"),
                                        RelationshipType = objdata.Field<string>("Relationship_Type"),
                                        DOB = objdata.Field<DateTime>("DOB"),
                                        Gender = objdata.Field<string>("Gender")
                                    };
                        userDetails = query.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("LoadEditBudget" + " " + ex.Message.ToString());
            }
            return userDetails;
        }

        public List<TaskStatusDTO> GetTaskStatus(int? roleId)
        {
            SqlParameter[] spParams;
            List<TaskStatusDTO> taskStatus = new List<TaskStatusDTO>();

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@RoleId ",roleId),


                };
                var res = dbUtil.ExecuteSQLQuery("Task_Status_Get", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        var query = from objdata in res.Tables[0].AsEnumerable()
                                    select new TaskStatusDTO()
                                    {
                                        TaskStatusId = objdata.Field<int>("Task_Status_Id"),
                                        TaskStatusName = objdata.Field<string>("Task_Status_Name")
                                    };
                        taskStatus = query.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("LoadEditBudget" + " " + ex.Message.ToString());
            }
            return taskStatus;
        }
        public RewardDetailsDTO GetRewards(int? rewardId)
        {
            SqlParameter[] spParams;
            RewardDetailsDTO taskStatus = new RewardDetailsDTO ();

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@Reward_Id ",rewardId),


                };
                var res = dbUtil.ExecuteSQLQuery("Reward_Details_Get", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        var query = from objdata in res.Tables[0].AsEnumerable()
                                    select new RewardsDTO()
                                    {
                                        RewardId = objdata.Field<int>("Reward_Id"),
                                        RewardName = objdata.Field<string>("Reward_Name"),
                                        Description = objdata.Field<string>("Description"),
                                    };
                        var query1 = from objdata in res.Tables[0].AsEnumerable()
                                    select new RewardsImageDTO()
                                    {
                                        RewardImageId = objdata.Field<int>("Reward_Image_Id"),
                                        Image = objdata.Field<string>("Image"),
                                        RewardId = objdata.Field<int>("Reward_Id"),
                                    };
                        taskStatus.Rewards = query.ToList();
                        taskStatus.RewardImages = query1.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("LoadEditBudget" + " " + ex.Message.ToString());
            }
            return taskStatus;
        }
    }
}
