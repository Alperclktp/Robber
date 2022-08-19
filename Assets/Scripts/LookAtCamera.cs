using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        LookCamera();
    }

    private void LookCamera()
    {
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}
