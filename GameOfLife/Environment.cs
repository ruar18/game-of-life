﻿/*
 * Nicole Beri
 * January 15, 2019
 * Base class for the environments (subclasses --> tundra, rainforest, greenhouse, desert)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Used for drawing graphics in the Environment
using System.Drawing; 

namespace GameOfLife
{
    public abstract class Environment
    {
        // **** ENVIRONMENTAL PARAMETERS ****/
        // Atmospheric composition -- used to determine evolution into animal or plant
        protected int carbonDioxideLevel;
        protected int oxygenLevel;
        // (Rudy) the default amount of food and water initially available in the environment
        // read-only -- can be initialized in the constructor but nowhere else
        public virtual double DefaultFood { get; }
        public virtual int DefaultWater { get; }
        // the amount of food and water in the environment during the simulation
        protected double foodAvailability;
        protected int waterAvailability;
        // the current temperature of the environment
        protected int temperature;
        // visual indicator for the environment
        private Image environmentImage;

        // **** EVENT INFORMATION ****
        // the chance of rain as a percent
        protected int probabilityOfRain;
        // the chance of an event occurring as a percent
        public const int PROBABILITY_OF_EVENT = 2;
        // state variable for if an event is occurring
        protected bool eventOccurring;
        // determines how many generations left for an occurring event
        protected int eventGenerationsLeft;
        // images for various events
        private Image eventImage;
        private Image rainImage;

        // generates random numbers to determine if probabalistic events occur
        Random numberGenerator = new Random();

        /// <summary>
        /// (Tiffanie) Create a new Environment with the given values unique to a chosen type of Environment
        /// </summary>
        /// <param name="defaultFood"> The default amount of food in this type of environment </param>
        /// <param name="defaultWater"> The default amount of water in this type of environment </param>
        /// <param name="oxygenLevel"> The oxygen level of this environment </param>
        /// <param name="carbonDioxideLevel"> The carbon dioxide level in this environment </param>
        /// <param name="temperature"> The temperature of this environment </param>
        /// <param name="probOfRain"> The probability of rain in this environment </param>
        public Environment(double defaultFood, int defaultWater,
                           int oxygenLevel, int carbonDioxideLevel,
                           int temperature, int probOfRain)
        {
            // **** INITIALIZE ENVIRONMENT PARAMETERS BASED ON TYPE OF ENVIRONMENT ****
            // The default/base amount of food and water in this biome
            DefaultFood = defaultFood;
            DefaultWater = defaultWater;
            // Amount of food and water initially available for the simulation (this can be modified by the user)
            foodAvailability = DefaultFood;
            waterAvailability = DefaultWater;
            // Atmospheric composition
            CarbonDioxideLevel = carbonDioxideLevel;
            OxygenLevel = oxygenLevel;
            // Temperature
            Temperature = temperature;
            // Probability of rain as a percent
            probabilityOfRain = probOfRain;
        }
        
        // getter for event images
        public Image EventImage
        {
            get { return this.eventImage; }
        }

        // getter for rain images
        public Image RainImage
        {
            get { return this.rainImage; }
        }

        // getter for environment images
        public Image EnvironmentImage
        {
            get { return this.environmentImage; }
        }

        // getter for carbon dioxide level
        public int CarbonDioxideLevel
        {
            get { return this.carbonDioxideLevel; }
            set { this.carbonDioxideLevel = value; }
        }

        // get and set food availability 
        public double FoodAvailability
        {
            get { return this.foodAvailability; }
            set { this.foodAvailability = value; }
        }

        // get and set oxyegen level 
        public int OxygenLevel
        {
            get { return this.oxygenLevel; }
            set { this.oxygenLevel = value; }
        }

        // get and set temperature
        public int Temperature
        {
            get { return this.temperature; }
            set { this.temperature = value; }
        }

        // get and set water availability
        public int WaterAvailability
        {
            get { return this.waterAvailability; }
            set { this.waterAvailability = value; }
        }

        // **** FOOD AND WATER PROCESSING **** //

        /// <summary>
        /// Increase the amount of food in the Environment
        /// </summary>
        /// <param name="toAdd"> The amount of food being returned to the environment </param>
        public void IncreaseFood(int toAdd)
        {
            FoodAvailability += toAdd;
        }

        /// <summary>
        /// Decrease the amount of food in the Environment after being consumed by a Unit
        /// </summary>
        /// <param name="consumed"> The amount of food being consumed </param>
        public void DecreaseFood(double consumed)
        {
            FoodAvailability -= consumed;
        }

        /// <summary>
        /// Decrease the amount of water in the Environment after being consumed by a Unit
        /// </summary>
        /// <param name="consumed"> The amount of water being consumed </param>
        public void DecreaseWater(int consumed)
        {
            WaterAvailability -= consumed;
        }


        // **** EVENT PROCESSING **** //

        // A unique environmental event that changes the environmental parameters
        abstract protected void EnvironmentalEvent();

        /// <summary>
        /// Determine if an event should occur in the environment
        /// </summary>
        /// <returns> True if a new event should start and false otherwise </returns>
        public bool EventOccurs()
        {
            // If an event is already occurring, do not try to start a new event
            if (eventOccurring)
            {
                return false;
            }
            // Otherwise generate a random number of 100 possible values (0-99)
            int randomNumber = numberGenerator.Next(0, 100);
            // The chance of generating a number less than the percent probability
            // of an event occuring is equal to the probability of the event occuring
            // -- (probability of event) favourable cases out of 100
            // If it meets this condition, start a new event
            if (randomNumber < PROBABILITY_OF_EVENT)
            {
                return true;
            }
            // otherwise, an event should occur
            return false;
        }

        /// <summary>
        /// (Tiffanie) Enact raining in the environment
        /// </summary>
        protected void Rain()
        {
            // Increase water availability in the environment by 10% of the default amount 
            WaterAvailability += 10 * DefaultWater;
        }
    }
}

