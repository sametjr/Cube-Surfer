using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private bool canRemoveCube = true;
    Coroutine removeCubeCoroutine;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Cube"))
        {
            if(removeCubeCoroutine != null) return;
            canRemoveCube = true;
            removeCubeCoroutine = StartCoroutine(RemoveCube());
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Cube"))
        {
            canRemoveCube = false;
            StopCoroutine(removeCubeCoroutine);
        }
    }


    private IEnumerator RemoveCube()
    {
        while(canRemoveCube)
        {
            if(GameManager.Instance.isPlaying) CubeController.Instance.DestroyLastCube();
            yield return new WaitForSeconds(SerializedVariables.Instance.timeToDestroyCubeOnLava);
        }

        removeCubeCoroutine = null;
    }
}
