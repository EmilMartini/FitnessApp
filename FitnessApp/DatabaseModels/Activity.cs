using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker
{
    public class Activity
    {
        public virtual int UserId { get; set; }
        public int Id { get; set; }
        public int Length { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
