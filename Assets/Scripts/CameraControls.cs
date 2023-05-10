using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControls : MonoBehaviour
{
    private float maxFov = 80f;
    private float minFov = 60f;
    private float destinationFollowOffsetY;
    private float minFollowOffsetY = 2f;
    private float maxFollowOffsetY = 3.5f;
    private float destinationFov;
    private bool shouldLerpFov = false;
    private bool shouldLerpFollowOffset = false;

    CinemachineVirtualCamera vcam;
    CinemachineTransposer vcamTransposer;

    private void Start() {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcamTransposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }

    

    private void Update() {
        if(shouldLerpFov)
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, destinationFov, SerializedVariables.Instance.lerpValue);
            if(Mathf.Abs(vcam.m_Lens.FieldOfView - destinationFov) < 0.01f)
            {
                shouldLerpFov = false;
            }
        }

        if(shouldLerpFollowOffset)
        {
            vcamTransposer.m_FollowOffset.y = Mathf.Lerp(vcamTransposer.m_FollowOffset.y, destinationFollowOffsetY, SerializedVariables.Instance.lerpValue);
            if(Mathf.Abs(vcamTransposer.m_FollowOffset.y - destinationFollowOffsetY) < 0.01f)
            {
                shouldLerpFollowOffset = false;
            }
        }
    }


    public void CubeAdded()
    {
        if(vcam.m_Lens.FieldOfView >= maxFov) return;
        shouldLerpFov = true;
        destinationFov = vcam.m_Lens.FieldOfView + SerializedVariables.Instance.fovIncrement;

        if(vcamTransposer.m_FollowOffset.y >= maxFollowOffsetY) return; 
        shouldLerpFov = true;
        destinationFollowOffsetY = vcamTransposer.m_FollowOffset.y + SerializedVariables.Instance.followOffsetIncrement;
    }

    public void CubeRemoved()
    {
        if(vcam.m_Lens.FieldOfView <= minFov) return;
        shouldLerpFov = true;
        destinationFov = vcam.m_Lens.FieldOfView - SerializedVariables.Instance.fovIncrement;

        if(vcamTransposer.m_FollowOffset.y <= minFollowOffsetY) return;
        shouldLerpFov = true;
        destinationFollowOffsetY = vcamTransposer.m_FollowOffset.y - SerializedVariables.Instance.followOffsetIncrement;
    }


}
