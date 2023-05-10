using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRigidbody : MonoBehaviour
{
    LayerMask layerMask = 10; // Wall layer

    private void Start() {
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, .3f, layerMask))
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // Freeze all axes except Y
        }

    }

}
