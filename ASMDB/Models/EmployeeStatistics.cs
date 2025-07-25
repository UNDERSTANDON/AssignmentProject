using System;

namespace ASMDB.Models
{
    public class EmployeeStatistics
    {
        public int Employee_ID { get; set; }
        public string Employee_Name { get; set; }
        public int SoldWeek { get; set; }
        public int SoldMonth { get; set; }
        public int SoldYear { get; set; }
        public int SoldAllTime { get; set; }
    }
} 