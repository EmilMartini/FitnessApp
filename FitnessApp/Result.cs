using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FitnessTracker
{
    public class Result
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }

        public Result()
        {
            ErrorMessages = new List<string>();
        }
    }
}
