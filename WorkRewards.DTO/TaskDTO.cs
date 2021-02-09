using System;
using System.Collections.Generic;
using System.Text;

namespace WorkRewards.DTO
{
    public class TaskDTO
    {
        public long Task_Id { get; set; }
        public string Task_Name { get; set; }
        public string Task_Description { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public long Assigned_To { get; set; }
        public string Assigned_To_Name { get; set; }
        public int Task_Status_Id { get; set; }
        public string Task_Status_Name { get; set; }
        public DateTime? Task_Completed_Date { get; set; }
        public bool Is_Approved { get; set; }
        public long? Reward_Id { get; set; }
        public string Reward_Name { get; set; }
        public DateTime? Reward_Redemption_Date { get; set; }
        public bool Is_Active { get; set; }
        public long UserId { get; set; }
    }

    public class TaskRequestDTO
    {
        public long UserId { get; set; }
        public long? TaskId { get; set; }
        public int? StatusId { get; set; }
    }
}
