using UnityEngine;

namespace Race
{
    [CreateAssetMenu(fileName = "AIDriverData", menuName = "Kart/AIDriverData")]
    public class AIDriverData : ScriptableObject
    {
        public float proximityThreshold = 20f;  // threshold for how close are we
        public float updateCornerRange = 50f; // how close we need to get to a corner before we update the corner waypoint.
        public float brakeRange = 80f; // how close we need to get to a corner before we start braking.
        public float spinThreshold = 100f; // Angular velocity at which the AI will start counter-steering.
        public float speedWhileDrifting = 0.5f; // Speed when the AI drifting
        public float timeToDrift = 0.5f; // Drift time
    }
}