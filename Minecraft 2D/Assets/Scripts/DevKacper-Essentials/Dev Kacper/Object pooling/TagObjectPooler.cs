using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKacper.ObjectPooler
{
    public class TagObjectPooler : MonoBehaviour
    {
        public List<Pool> poolList;
        public static Dictionary<string, Queue<GameObject>> poolQueue;

        public static TagObjectPooler Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            poolQueue = new Dictionary<string, Queue<GameObject>>();

            foreach(Pool pool in poolList)
            {
                var newQueue = new Queue<GameObject>();
                pool.parent = new GameObject($"{pool.Tag}Content").transform;

                for(int i = 0; i < pool.Size; ++i)
                {
                    var newPrefabObject = Instantiate(pool.Prefab, pool.parent);
                    newPrefabObject.SetActive(false);
                    newQueue.Enqueue(newPrefabObject);
                }

                poolQueue.Add(pool.Tag, newQueue);
            }
        }

        private GameObject CreateObject(string tag)
        {
            GameObject newObject = null;
            foreach(Pool pool in poolList)
            {
                if(pool.Tag == tag)
                {
                    newObject = Instantiate(pool.Prefab, pool.parent);
                    break;
                }
            }
            return newObject;
        }

        public static GameObject Spawn(string tag)
        {
            if (poolQueue[tag].Count == 0)
            {
                Debug.Log("Pool extended! Creating new object!");
                return Instance.CreateObject(tag);
            }

            var spawnedObject = poolQueue[tag].Dequeue();
            spawnedObject.SetActive(true);
            return spawnedObject;
        }

        public static GameObject Spawn(string tag, Vector3 position)
        {
            var spawnedObject = Spawn(tag);
            spawnedObject.transform.position = position;
            return spawnedObject;
        }

        public static GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
        {
            var spawnedObject = Spawn(tag, position);
            spawnedObject.transform.position = position;
            return spawnedObject;
        }

        public static GameObject Spawn(string tag, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            var spawnedObject = Spawn(tag, position, rotation);
            spawnedObject.transform.localScale = scale;
            return spawnedObject;
        }

        public static void DestroyObject(string tag, GameObject objectToDestroy)
        {
            objectToDestroy.SetActive(false);
            poolQueue[tag].Enqueue(objectToDestroy);
        }
    }
}
