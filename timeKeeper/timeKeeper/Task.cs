using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timeKeeper
{
    public class Task
    {
        private string _taskName;
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
            }
        }

        private int _originalEstimation;
        public int OriginalEstimation
        {
            get
            {
                return _originalEstimation;
            }
            set
            {
                _originalEstimation = value;
            }
        }

        public Task(string taskName, int originalEstimation)
        {
            TaskName = taskName;
            OriginalEstimation = originalEstimation;
        }
    }
}
