using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waker
{
    [Serializable]
    public class AlarmJob
    {
        public string Time { get; set; } = "00:00:00";

        public AlarmJob()
        {
        }

        public AlarmJob(string span)
        {
            Time = span;
        }

        public override string ToString()
        {
            return Time.ToString();
        }
    }

    public class DoingAlarmJobs
    {
        private List<AlarmJob> jobs = new List<AlarmJob>();
        public List<AlarmJob> Jobs {
            get {return jobs; }
            set { jobs = new List<AlarmJob>(value); } 
        }

        public delegate void StartAJobHandle(AlarmJob job);
        public void DoJob(StartAJobHandle startAJob)
        {
            if(Jobs.Count>0)
            {
                startAJob(Jobs.First());
                Jobs.RemoveAt(0);
            }
        }

    }
}
