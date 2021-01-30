using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WorkRewards.Data.Utility;
using WorkRewards.DTO;
using System.Linq;
using WorkRewards.Data.Interface;

namespace WorkRewards.Data
{
    public class TaskData : BaseData, ITaskData
    {
        private readonly string ConnectionString;
        SummitDBUtils dbUtil;//= new SummitDBUtils();
        ILogger<UserData> logger;
        IConfiguration config;

        public TaskData(IConfiguration _config, ILogger<UserData> logger)
        {
            this.logger = logger;
            config = _config;
            this.ConnectionString = config.GetSection("AppSettings").GetSection("ConnectionString").Value;
            //dbUtil.ConnectionString = this.ConnectionString;
            dbUtil = new SummitDBUtils(_config, logger);
        }

        public List<TaskDTO> AssignedTaskDetailsByUserAndStatusGet(TaskRequestDTO task)
        {
            SqlParameter[] spParams;
            List<TaskDTO> taskList = new List<TaskDTO>();

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@User_Id", task.UserId),
                    new SqlParameter("@Task_Id", task.TaskId),
                    new SqlParameter("@Status_Id", task.StatusId),
                };
                var res = dbUtil.ExecuteSQLQuery("Assigned_Task_Details_By_User_And_Status_Get", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        var query = from objdata in res.Tables[0].AsEnumerable()
                                    select new TaskDTO()
                                    {
                                        Task_Id = objdata.Field<int>("Task_Id"),
                                        Task_Name = objdata.Field<string>("Task_Name"),
                                        Task_Status_Name = objdata.Field<string>("Task_Status_Name"),
                                        Assigned_To = objdata.Field<int>("Role_Name"),
                                        Assigned_To_Name = objdata.Field<string>("Assigned_To_Name"),
                                        End_Date = MakeSafeDate(objdata.Field<DateTime?>("End_Date")),
                                        Is_Active = objdata.Field<bool>("Is_Active"),
                                        Is_Approved = objdata.Field<bool>("Is_Approved"),
                                        Reward_Id = objdata.Field<int>("Reward_Id"),
                                        Reward_Name = objdata.Field<string>("Reward_Name"),
                                        Reward_Redemption_Date = MakeSafeDate(objdata.Field<DateTime?>("Reward_Redemption_Date")),
                                        Start_Date = MakeSafeDate(objdata.Field<DateTime?>("Start_Date")),
                                        Task_Completed_Date = MakeSafeDate(objdata.Field<DateTime?>("Task_Completed_Date")),
                                        Task_Description = objdata.Field<string>("Task_Description"),
                                        Task_Status_Id = objdata.Field<int>("Task_Status_Id"),
                                    };
                        taskList = query.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("AssignedTaskDetailsByUserAndStatusGet" + " " + ex.Message.ToString());
            }
            return taskList;
        }

        public bool UpdateTask(TaskRequestDTO task)
        {
            SqlParameter[] spParams;
            bool isUpdate = false;

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@User_Id", task.UserId),
                    new SqlParameter("@Task_Id", task.TaskId),
                    new SqlParameter("@Status_Id", task.StatusId),
                };
                var res = dbUtil.ExecuteSQLQuery("Assigned_Task_Status_Update", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        if (res.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
                        {
                            isUpdate = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return isUpdate;
        }

        public bool CreateTask(TaskDTO task)
        {
            SqlParameter[] spParams;
            bool isSaved = false;

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@Task_Name", task.Task_Name),
                    new SqlParameter("@Task_Description", task.Task_Description),
                    new SqlParameter("@Start_Date", task.Start_Date),
                    new SqlParameter("@End_Date", task.End_Date),
                    new SqlParameter("@Assigned_To", task.Assigned_To),
                    new SqlParameter("@Reward_Id", task.Reward_Id),
                    new SqlParameter("@User_Id", task.UserId)
                };
                var res = dbUtil.ExecuteSQLQuery("Task_Details_Insert", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        if (res.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
                        {
                            isSaved = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return isSaved;
        }

        public bool UpdateTaskDetails(TaskDTO task)
        {
            SqlParameter[] spParams;
            bool isSaved = false;

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                     new SqlParameter("@Task_Id", task.Task_Id),
                    new SqlParameter("@Task_Name", task.Task_Name),
                    new SqlParameter("@Task_Description", task.Task_Description),
                    new SqlParameter("@Start_Date", task.Start_Date),
                    new SqlParameter("@End_Date", task.End_Date),
                    new SqlParameter("@Assigned_To", task.Assigned_To),
                    new SqlParameter("@Reward_Id", task.Reward_Id),
                    new SqlParameter("@User_Id", task.UserId)
                };
                var res = dbUtil.ExecuteSQLQuery("Task_Details_Update", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        if (res.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
                        {
                            isSaved = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return isSaved;
        }


        public bool ApproveTask(int userId, int taskId)
        {
            SqlParameter[] spParams;
            bool isSaved = false;

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                     new SqlParameter("@User_Id", userId),
                    new SqlParameter("@Task_Id", taskId),

                };
                var res = dbUtil.ExecuteSQLQuery("Approve_Completed_Task", spParams);
                isSaved = true;
            }
            catch (Exception ex)
            {
                isSaved = false;

            }
            return isSaved;
        }
    }
}
