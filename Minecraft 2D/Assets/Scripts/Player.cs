using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInventory inventory;

    private void Awake()
    {
        inventory = new PlayerInventory(32);
    }
}
