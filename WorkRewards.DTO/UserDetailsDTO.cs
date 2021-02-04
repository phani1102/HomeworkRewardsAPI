using System;
using System.Collections.Generic;
using System.Text;

namespace WorkRewards.DTO
{
    public class UserDetailsDTO
    {
        public long? UserId { get; set; }
        public String FullName { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public string UserName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string GenderName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string RelationshipType { get; set; }

        public long? RelationShipId { get; set; }
    }
}
