using UnityEditor;
using UnityEngine;
using Utility;

namespace Surviblewilderness
{
    public class PassiveAnimalSpawner : SingletonBase<PassiveAnimalSpawner>  
    {
        public int activePassiveAnimals { get; private set; } = 5;
    }
}
