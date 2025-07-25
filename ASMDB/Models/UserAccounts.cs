using System;

namespace ASMDB.Models
{
    public class UserAccounts
    {
        public int UserAccount_ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int? Employee_ID { get; set; }
        public int? Cus_ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}