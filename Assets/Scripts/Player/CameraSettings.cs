using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] CinemachineVirtualCamera zoomOutCam;
    [SerializeField] CinemachineVirtualCamera zoomInCam;

    [Header("Sensitivities")]
    [SerializeField] [Range(10f, 1000f)] float zoomedOutSensitivity = 300f;
    [SerializeField] [Range(10f, 1000f)] float zoomedInSensitivity = 300f;

    [Header("Invert Aim")]
    [SerializeField] bool zoomedOutXInvert = false;
    [SerializeField] bool zoomedOutYInvert = true;
    [SerializeField] bool zoomedInXInvert = false;
    [SerializeField] bool zoomedInYInvert = true;

    CinemachineTransposer transposer;

    CinemachinePOV pov;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        zoomOutCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = zoomedOutSensitivity;
        zoomOutCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = zoomedOutSensitivity;
        zoomOutCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InvertInput = zoomedOutXInvert;
        zoomOutCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InvertInput = zoomedOutYInvert;

        zoomInCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = zoomedInSensitivity;
        zoomInCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = zoomedInSensitivity;
        zoomInCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InvertInput = zoomedInXInvert;
        zoomInCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InvertInput = zoomedInYInvert;
    }

    public void SetZoomedOutSensitivity(float sensitivity)
    {
        zoomedOutSensitivity = sensitivity;
    }

    public void SetZoomedInSensitivity(float sensitivity)
    {
        zoomedInSensitivity = sensitivity;
    }

    public void SetZoomedOutInvert(bool x, bool y)
    {
        zoomedOutXInvert = x;
        zoomedOutYInvert = y;
    }

    public void SetZoomedInInvert(bool x, bool y)
    {
        zoomedInXInvert = x;
        zoomedInYInvert = y;
    }
}
