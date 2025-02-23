using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool {
    [CreateAssetMenu(fileName = "ObjectPooler", menuName = "Scriptable Objects/ScriptablePoolList")]
    public class PoolListObjects : ScriptableObject 
    {
        [SerializeField] private List<PoolObject> objects =  new List<PoolObject>();

        public GameObject GetPoolObject (string tag) {
            try {
                return objects.Find(x => x.tag.Equals(tag)).prefab;
            }catch (Exception ex)
            {
                Debug.LogWarning($"Not possible find element with the information summited. Tag {tag}. error: {ex}");
                return objects[0].prefab;
            }
        }

        public GameObject GetPoolObject (string tag, int subType) {
            try {
                return objects.Find(x => x.tag.Equals(tag) && x.subType == subType).prefab;
            } catch (Exception ex) 
            {
                Debug.LogWarning($"Not possible find element with the information summited. Tag {tag}, Subtype {subType}. error: {ex}");
                return GetPoolObject(tag);
            }
        }

        public List<PoolObject> GetPoolObjects (string tag) 
        {
            return objects.FindAll(x => x.tag.Equals(tag));
        }

        public List<PoolObject> GetAllObjects ()
            => objects;
    }

    [Serializable]
    public class PoolObject 
    {
        public GameObject prefab;
        public string tag;
        public int subType;
    }

    [Serializable]
    public struct ObjectInstanceInfo {
        public string tag;
        public int subType;
        public Vector3 position;
        public Vector3 rotation;
        public string additionalInfo;
    }
}

