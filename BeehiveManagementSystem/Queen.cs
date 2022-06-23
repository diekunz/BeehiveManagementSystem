using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace BeehiveManagementSystem
{
    internal class Queen : Bee, INotifyPropertyChanged
    {
        const float EGGS_PER_SHIFT = .45f;
        const float HONEY_PER_UNASSIGNED_WORKER = .5f;

        private IWorker[] workers = new IWorker[0];

        private float unassignedWorkers = 3;
        private float eggs = 0;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string StatusReport { get; private set; }
        public override float CostPerShift { get { return 2.15f; } }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void AddWorker(IWorker worker)
        {
            if (unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }

        private string WorkerStatus(string job)
        {
            int count = 0;
            foreach (IWorker worker in workers)
                if (worker.Job == job) count++;
            string s = "s";
            if (count == 1) s = "";
            return $"{count} {job} bee{s}";
        }

        public void AssignBee(string job)
        {
            switch (job)
            {
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;
                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;
            }
            UpdateStatusReport();
        }

        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }

        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach (Bee worker in workers)
            {
                worker.WorkTheNextShift();
            }
            HoneyVault.ConsumeHoney(unassignedWorkers * HONEY_PER_UNASSIGNED_WORKER);
            UpdateStatusReport();
        }

        private void UpdateStatusReport()
        {
            StatusReport = $"Vault report:\n{HoneyVault.StatusReport}\n" +
                $"\nEgg count: {eggs:0.0}\nUnassigned workers: {unassignedWorkers:0.0}" +
                $"\n{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufacturer")}" +
                $"\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS: {workers.Length}";
            OnPropertyChanged("StatusReport");
        }

        public Queen() : base("Queen")
        {
            AssignBee("Egg Care");
            AssignBee("Nectar Collector");
            AssignBee("Honey Manufacturer");
        }
    }
}
