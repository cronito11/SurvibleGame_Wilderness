using UnityEngine;

namespace Surviblewilderness
{
    // generic class for entity data
    // only class structure is here with some common entity data 
    // this class will be inherited by other entity data classes which are specific to entities
    [System.Serializable]
    public class EntityData
    {
        public EntityPosition entityPosition;
    }


    
    [System.Serializable]
    public class EntityPosition
    {
        public float x;
        public float y;
        public float z;
    }

    /* this calss will be inherited by other entity data classes which have their stats 
     * like player,enenmy, passive animal, damageable game entities etc    
    */

    [System.Serializable]
    public class EntityStats
    {
        public float health;
    }

}
