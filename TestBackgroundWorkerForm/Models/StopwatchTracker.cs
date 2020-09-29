using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBackgroundWorkerForm.Models
{
    public class StopwatchTracker
    {
        public StopwatchTracker(DateTime _elapsedTime)
        {
            ElapsedTime = _elapsedTime.ToString("dd/MM/yyyy hh:mm:ss.fff");
            TimeCreated = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");
        }
        /// <summary>
        /// Generate ElapsedTime = DateTime.Now
        /// </summary>
        public StopwatchTracker()
        {
            ElapsedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");
            TimeCreated = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; private set; }
        public string ElapsedTime { get; private set; }

        public string TimeCreated
        {
            get; private set;
        }
    }
}
