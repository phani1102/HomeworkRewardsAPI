using WorkRewards.Data.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using WorkRewards.DTO;
using WorkRewards.Data.Interface;
using WorkRewards.Data;

namespace WorkRewards.Data
{
    public class UserData: BaseData, IUserData
    {
        private readonly string ConnectionString;
        SummitDBUtils dbUtil;//= new SummitDBUtils();
        ILogger<UserData> logger;
        IConfiguration config;

        public UserData(IConfiguration _config, ILogger<UserData> logger)
        {
            this.logger = logger;
            config = _config;
            this.ConnectionString = config.GetSection("AppSettings").GetSection("ConnectionString").Value;
            //dbUtil.ConnectionString = this.ConnectionString;
            dbUtil = new SummitDBUtils(_config, logger);
        }
        public long RegisterUser(UserDetailsDTO user)
        {
            SqlParameter[] spParams;
            long userId = 0;

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@First_Name", user.First_Name),
                    new SqlParameter("@Last_Name", user.Last_Name),
                    new SqlParameter("@Middle_Name", user.Middle_Name),
                    new SqlParameter("@Username", user.UserName),
                    new SqlParameter("@Password", user.Password),
                    new SqlParameter("@Email", user.Email),
                    new SqlParameter("@Mobile_No", user.MobileNumber),
                    new SqlParameter("@Role_Id", user.RoleId),
                    new SqlParameter("@Relationship_Id", user.RelationShipId),
                    new SqlParameter("@DOB", user.DOB),
                    new SqlParameter("@Gender", user.Gender)

                };
                var res = dbUtil.ExecuteSQLQuery("User_Details_Insert", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        if (res.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
                        {
                            userId = Convert.ToInt64(res.Tables[0].Rows[0]["UserId"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("RegisterUser" + " " + ex.Message.ToString());
            }
            return userId;
        }

        public List<RoleDTO> GetRoles(int? roleId)
        {
            SqlParameter[] spParams;
            List<RoleDTO> roles = new List<RoleDTO>();

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@RoleId",roleId),


                };
                var res = dbUtil.ExecuteSQLQuery("Role_Details_Get", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        var query = from objdata in res.Tables[0].AsEnumerable()
                                    select new RoleDTO()
                                    {
                                        RoleID = objdata.Field<int>("Role_Id"),
                                        RoleName = objdata.Field<string>("Role_Name")
                                    };
                        roles = query.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("GetRoles" + " " + ex.Message.ToString());
            }
            return roles;
        }

        public UserDetailsDTO ValidateUser(UserDetailsDTO user)
        {
            SqlParameter[] spParams;
            UserDetailsDTO userdetails = new UserDetailsDTO();

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@Username",user.UserName),
                    new SqlParameter("@Password",user.Password),
                };
                var res = dbUtil.ExecuteSQLQuery("Validate_User", spParams);
                if (res.Tables.Count > 0)
                {
                    if (res.Tables[0].Rows.Count > 0)
                    {
                        var query = from objdata in res.Tables[0].AsEnumerable()
                                    select new UserDetailsDTO()
                                    {
                                        UserId = objdata.Field<int>("Role_Id"),
                                        First_Name = objdata.Field<string>("First_Name"),
                                        Last_Name = objdata.Field<string>("Last_Name"),
                                        Middle_Name = objdata.Field<string>("Middle_Name"),
                                        Email = objdata.Field<string>("Email"),
                                        MobileNumber = objdata.Field<string>("Mobile_No"),
                                        RoleId = objdata.Field<int>("Role_Id"),
                                        RoleName = objdata.Field<string>("Role_Name"),
                                        DOB = objdata.Field<DateTime>("DOB"),
                                        Gender = objdata.Field<string>("Gender")
                                    };
                        userdetails = query.FirstOrDefault();
                    }
                }
                else
                    userdetails = null;
            }
            catch (Exception ex)
            {
                this.logger.LogError("ValidateUser" + " " + ex.Message.ToString());
            }
            return userdetails;
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            SqlParameter[] spParams;
            bool isSuccess = false;

            try
            {
                dbUtil.ConnectionString = this.ConnectionString;
                spParams = new SqlParameter[] {
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@OldPassword", oldPassword),
                    new SqlParameter("@NewPassword", newPassword),

                };
                var res = dbUtil.ExecuteSQLQuery("User_Password_Update", spParams);
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
                this.logger.LogError("ChangePassword" + " " + ex.Message.ToString());
            }
            return isSuccess;
        }
    }
}
