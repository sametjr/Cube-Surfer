using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrangeStairs : MonoBehaviour
{
    [SerializeField] private GameObject stairPrefab;
    [SerializeField] private Transform firstStair;

    private IEnumerator Start() {
        var xPos = firstStair.position.x;
        var yPos = firstStair.position.y + CubeController.Instance.cubeSize;
        var zPos = firstStair.position.z + CubeController.Instance.cubeSize;

        yield return new WaitForSeconds(0.2f);
        for(int i = 1; i < 10; i++)
        {
            GameObject stair = Instantiate(stairPrefab, this.transform);
            stair.transform.position = new Vector3(xPos, yPos, zPos);
            stair.GetComponentInChildren<TextMeshPro>().text = (i+1).ToString();

            yPos += CubeController.Instance.cubeSize;
            zPos += CubeController.Instance.cubeSize;
        }
    }
}
