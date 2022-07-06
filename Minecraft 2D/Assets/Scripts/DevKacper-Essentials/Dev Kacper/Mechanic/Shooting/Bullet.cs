using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevKacper.Mechanic
{
    public class Bullet : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float movementSpeed = 0;

        public float MovementSpeed
        {
            get => movementSpeed;
            set { movementSpeed = value; }
        }

        private void Update()
        {
            transform.position += transform.right * movementSpeed * Time.deltaTime;
        }
    }
}



