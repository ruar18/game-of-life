﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    [Serializable]
    class Desert : Environment
    {
        /// <summary>
        /// Create a Desert with its unique environmental parameters for the simulation's environment
        /// </summary>
        public Desert() : base(3000, 1500, 50, 50, 45, 5, Properties.Resources.Desert, Properties.Resources.Sandstorm)
        {
            // (Nicole) assign the specific environment type
            environmentType = EnvironmentTypeEnum.Desert;
        }

        /// <summary>
        /// Enacts the Desert's unique environmental event of a sandstorm
        /// </summary>
        public override void EnvironmentalEvent(Unit[,] units)
        {
            // Wind decreases temperature by 5℃
            Temperature -= 5;
            // Lose access to 5% of available food
            FoodAvailability -= 0.10 * FoodAvailability;
            // Indicate that the event has stopped once it should not continue for the next generation
            if (--EventGenerationsLeft == 0)
            {
                EnvEventOccurring = false;
            }
        }
    }
}
