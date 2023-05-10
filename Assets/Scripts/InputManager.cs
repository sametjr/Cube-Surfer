using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float currentTouchX;
    private float previousTouchX = Screen.width / 2;
    [SerializeField] private CharacterMovement characterMovement; 

    private void Update() {
        if(!GameManager.Instance.isPlaying) return;
        if (Input.GetMouseButton(0))
        {
            float currentTouchX = Input.mousePosition.x;
            if(currentTouchX > previousTouchX)
            {   
                characterMovement.MoveRight();
            }
            else if(currentTouchX < previousTouchX)
            {   
                characterMovement.MoveLeft();
            }
            previousTouchX = currentTouchX;
        }

        if(Input.GetMouseButtonUp(0))
        {
            previousTouchX = Screen.width / 2;
        }
    }
}
