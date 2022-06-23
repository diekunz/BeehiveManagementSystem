using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    abstract class Bee : IWorker
    {
        /// <summary>
        /// Return value of honey that eat this worker
        /// </summary>
        public abstract float CostPerShift { get; }
        public string Job { get; private set; }

        public void WorkTheNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift)) DoJob();
        }

        protected abstract void DoJob();

        public Bee(string job)
        {
            Job = job;
        }
    }
}
