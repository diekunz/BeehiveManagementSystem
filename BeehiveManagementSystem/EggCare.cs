using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal class EggCare : Bee
    {
        private Queen queen;
        const float CARE_PROGRESS_PER_SHIFT = .15f;
        public override float CostPerShift { get { return 1.35f; } }

        protected override void DoJob()
        {
            queen.CareForEggs(CARE_PROGRESS_PER_SHIFT);
        }

        public EggCare(Queen queen) : base("Egg Care")
        {
            this.queen = queen;
        }
    }
}
