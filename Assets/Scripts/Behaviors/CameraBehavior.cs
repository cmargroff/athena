
using System.Collections;
using Cinemachine;
using UnityEngine;
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraBehavior:AthenaMonoBehavior
{
    private CinemachineVirtualCamera _virtualCamera;
    

    protected override void Start()
    {
        base.Start();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        StartCoroutine(ShakeWorker(intensity, time));
    }

    private IEnumerator ShakeWorker(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain=intensity;
        yield return new WaitForSeconds(time);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

    }

}

