using UnityEditor;
using UnityEngine;
using Utility;

namespace Surviblewilderness
{
    public class PassiveAnimalSpawner : SpawningManager
    {

        private  const int MAX_AMOUNT = 1000000;

        override protected int MaxAmount ()
        {
            return MAX_AMOUNT;
        }

        override protected float GetProbability ()
        {
            return 1;
        }
    }
}
