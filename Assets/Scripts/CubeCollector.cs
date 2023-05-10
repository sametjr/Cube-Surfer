using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollector : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Cube"))
        {
            CubeController.Instance.AddCube(other.gameObject);
        }

        else if(other.CompareTag("Wall"))
        {
            CubeController.Instance.RemoveCube(this.gameObject.transform.parent.gameObject);
        }

        else if(other.CompareTag("Diamond"))
        {
            CubeController.Instance.DiamondCollected();
            Destroy(other.gameObject);
        }

        else if(other.CompareTag("Finish"))
        {
            if(GameManager.Instance.isPlaying) GameManager.Instance.GameOver();
        }
    }


    
}
