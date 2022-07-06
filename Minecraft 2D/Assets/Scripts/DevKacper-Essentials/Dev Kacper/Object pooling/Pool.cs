using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKacper.ObjectPooler
{
    [System.Serializable]
    public class Pool
    {
        [Header("General")]
        [SerializeField] private string tag;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int size;

        [System.NonSerialized] public Transform parent;

        public string Tag => tag;
        public GameObject Prefab => prefab;
        public int Size => size;
    }
}