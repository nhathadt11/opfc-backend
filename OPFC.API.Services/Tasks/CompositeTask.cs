using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPFC.API.ServiceModel.Tasks
{
    public class CompositeTask : ITask
    {
        private readonly IEnumerable<ITask> _tasks;

        public CompositeTask(params ITask[] tasks)
        {
            _tasks = tasks.ToArray();
        }

        void ITask.Execute()
        {
            foreach (var task in _tasks)
            {
                task.Execute();
            }
        }
    }
}
