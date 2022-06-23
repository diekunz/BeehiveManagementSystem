using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal static class HoneyVault
    {
        const float NECTAR_CONVERSION_RATIO = .19f;
        const float LOW_LEVEL_WARNING = 10f; 
        static float honey = 25f;
        static float nectar = 100f;

        public static string StatusReport
        {
            get
            {
                string status = $"{honey:0.0} units of honey" + 
                    $"\n{nectar:0.0} units of nectar";
                string warnings = "";
                if (honey < LOW_LEVEL_WARNING)
                    warnings += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (nectar < LOW_LEVEL_WARNING)
                    warnings += "\nLOW NECTAR - ADD A NECTAR MANUFACTURER";
                return status + warnings;
            }
        }

        public static void CollectNectar(float amount)
        {
            if (amount >= 0) nectar += amount; 
        }
        public static void ConvertNectarToHoney(float amount)
        {
            float nectarToConvert = amount;
            if (nectarToConvert > nectar) nectarToConvert = nectar;
            nectar -= nectarToConvert;
            honey += nectarToConvert * NECTAR_CONVERSION_RATIO;
        }
        
        /// <summary>
        /// Return true if we have honey and substracts amount from honey
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool ConsumeHoney(float amount)
        {
            if (honey > amount)
            {
                honey -= amount;
                return true;
            }
            return false;
        }
    }
}
