using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Game Settings/Player Stat Settings")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float sneakWalkSpeed;
    public float SneakWalkSpeed { get { return sneakWalkSpeed; } }

    [SerializeField] private float runSpeed;
    public float RunSpeed { get { return runSpeed; } }

    [SerializeField] private float rotationSpeed;
    public float RotationSpeed { get { return rotationSpeed; } }

}
