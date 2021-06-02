using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    class ScheduleItem
    {
        public string Name { get; set; }
        public ItemCategories Category { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EstimatedHours { get; set; }
        public bool IsFinished { get; set; }

        public ScheduleItem(string name, ItemCategories category, DateTime startDate, DateTime endDate, int estimatedHours, bool isFinished = false)
        {
            Name = name;
            Category = category;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedHours = estimatedHours;
            IsFinished = isFinished;
        }
    }
}
