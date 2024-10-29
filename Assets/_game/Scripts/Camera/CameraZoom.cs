using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    [SerializeField] [Range(1f,10f)] float defaultDistance = 6f;        
    [SerializeField] [Range(1f,10f)] float minDistance = 1f;        
    [SerializeField] [Range(1f,10f)] float maxDistance = 6f;   

    [SerializeField] [Range(1f,10f)] float smoothing = 4f;        
    [SerializeField] [Range(1f,10f)] float zoomSensitivity = 1f;        

    CinemachineFramingTransposer framingTransposer;
    CinemachineInputProvider inputProvider;

    float currnTargetDistance;
    public void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();
        currnTargetDistance = defaultDistance;
    }
    public void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomvalue = inputProvider.GetAxisValue(2) * zoomSensitivity;

        currnTargetDistance = Mathf.Clamp(currnTargetDistance + zoomvalue,minDistance,maxDistance);

        float currentDistance = framingTransposer.m_CameraDistance;
        if(currentDistance == currnTargetDistance)
        {
            return;
        }

        float lerpZoomvalue = Mathf.Lerp(currentDistance, currnTargetDistance, Time.deltaTime * smoothing);
        
        framingTransposer.m_CameraDistance = lerpZoomvalue;
    }
}
