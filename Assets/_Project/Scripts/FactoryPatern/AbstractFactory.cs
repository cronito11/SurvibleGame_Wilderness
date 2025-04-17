using System.Collections.Generic;
using UnityEngine;

namespace Platformer397
{
    public abstract class AbstractFactory : MonoBehaviour
    {
        public List<GameObject> agentPrefabs;
        public Transform spawnLocation;
        public Transform spawnTarget;

        public abstract void GenerateAgent();
    }
}
