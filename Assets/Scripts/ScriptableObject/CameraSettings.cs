using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camera Settings", menuName = "Game Settings/Player Camera Settings")]
public class CameraSettings : ScriptableObject
{
    [SerializeField] private Vector3 offset;
    public Vector3 Offset { get { return offset; } }

}
