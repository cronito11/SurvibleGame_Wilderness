using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool {
    public class PoolObjects_Instantiator : MonoBehaviour 
    {
        [SerializeField] private PoolListObjects [] pools;

        private Dictionary<GameObject, string> inUse = new Dictionary<GameObject, string>();
        private Dictionary<string, Queue<GameObject>> inPool = new Dictionary<string, Queue<GameObject>>();

        public GameObject CreateObject (string tag, int subtype, Vector3 position, Quaternion rotation, Transform parent = null) 
        {
            string key = $"{tag}_{subtype}";

            GameObject prefab = null;

            if (!inPool.ContainsKey(key))
                inPool [key] = new Queue<GameObject>();

            if (inPool [key].Count > 0) 
                prefab = inPool [key].Dequeue();                
            else 
            {
                for (int idx = 0; idx < pools.Length; idx++) 
                {
                    prefab = pools [idx].GetPoolObject(tag, subtype);
                    if (prefab  != null) {
                        //Initialize Object
                        prefab = Instantiate(prefab);
                        break;
                    }
                }
            }

            if (prefab == null) {
                Debug.LogWarning($"No prefab found for key: {key}");
                return null;
            }

            //Initialize Object
            prefab.transform.parent = parent;
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;
            prefab.SetActive(true);
            inUse.Add(prefab, key);

            return prefab;
        }

        public void ReleaseObject (GameObject prefab) {
            if (!inUse.TryGetValue(prefab, out string key))
            {
                Debug.LogWarning($"The element {prefab.name} not exist in elements used in the pool", prefab);
                return;
            }
            prefab.transform.parent = transform;
            prefab.SetActive(false);
            inPool [key].Enqueue(prefab);
            inUse.Remove(prefab);
        }

        public void ReleaseAll () 
        {
            Dictionary<GameObject, string> tempInUse =new Dictionary<GameObject, string>(inUse);

            foreach (GameObject key in tempInUse.Keys)
                ReleaseObject(key);
        }
    }
}

