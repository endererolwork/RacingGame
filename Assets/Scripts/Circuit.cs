using UnityEngine;

namespace Race
{
    [CreateAssetMenu(fileName = "CircuitData", menuName = "Race/CircuitData")]
    public class Circuit : ScriptableObject {
        public Transform[] waypoints;
        public Transform[] spawnPoints;
    }
}