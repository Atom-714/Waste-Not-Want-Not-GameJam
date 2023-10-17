using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cmFreeCam;

    private void Awake()
    {
        cmFreeCam = GetComponent<CinemachineVirtualCamera>();
    }

    private IEnumerator _ProcessShake(float shakeIntensity = 3f, float shakeTiming = 0.5f)
    {
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeIntensity;
        yield return new WaitForSeconds(shakeTiming);
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }

    public void DoCameraShake()
    {
        StartCoroutine(_ProcessShake());

    }
}
