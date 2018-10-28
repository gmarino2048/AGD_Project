using UnityEngine;

namespace Monsters
{
    public class CombatChoiceStatus
    {
        private readonly int _MinPower;
        private readonly float _DecayRate;

        /// <summary>
        /// The amount of power this combat choice will have on the manager meter
        /// </summary>
        public int Power { get; private set; }

        /// <summary>
        /// Creates a new instance of CombatChoiceStatus
        /// </summary>
        /// <param name="startPower">The starting power of the attack</param>
        /// <param name="decayRate">The decay rate of power each time the attack is used</param>
        /// <param name="minPower">The minimum power level for this attack</param>
        public CombatChoiceStatus(int startPower, float decayRate, int minPower)
        {
            _MinPower = minPower;
            _DecayRate = decayRate;
            Power = startPower;
        }
        
        /// <summary>
        /// Updates the power for the attack according to its decay rate and minimum value
        /// </summary>
        public void UpdatePower()
        {
            Power = Mathf.CeilToInt(Mathf.Max(Power * _DecayRate, _MinPower));
        }
    }
}