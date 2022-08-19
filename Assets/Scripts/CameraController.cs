using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Insntace;

    [SerializeField] CameraSettings cameraSettings;

    [SerializeField] private Transform target;

    private void Awake() => Insntace = this;

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + cameraSettings.Offset, Time.deltaTime * 5);
        }
    }

    public void CameraShakeEffect()
    {
        Camera.main.transform.DOShakeRotation(0.1f, 0.2f, 1, 1, true);
    }
}
