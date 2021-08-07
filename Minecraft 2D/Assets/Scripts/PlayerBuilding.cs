using DevKacper.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    [SerializeField] private Transform pointer;

    private void Update()
    {
        pointer.transform.position = UtilityClass.GetGridMousePosition();
        if(Input.GetMouseButtonDown(0))
        {
            var colliders = Physics2D.OverlapPointAll(pointer.transform.position);
            foreach(Collider2D collider in colliders)
            {
                var chunk = collider.GetComponent<ChunkObject>();
                if(chunk != null)
                {
                    chunk.DestroyBlock(pointer.transform.position);
                }
            }
        }
    }
}
