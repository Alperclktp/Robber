using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Game Settings/Enemy Stat Settings")]
public class EnemySettings : ScriptableObject
{
    /*
    [SerializeField] private float walkSpeed;
    public float WalkSpeed { get { return walkSpeed; } }
    */

    [Header("Movement")]
    [SerializeField] private float runSpeed;
    public float RunSpeed { get { return RunSpeed; } }

    [Header("Fire Settings")]

    [SerializeField] private float damage;
    public float Damage { get { return damage; } }

    [SerializeField] private float fireDistance;
    public float FireDistance { get { return fireDistance; } }

    [SerializeField] private float fireRate;
    public float FireRate { get { return fireRate; } }

    [Header("Rotate")]
    [SerializeField] private float rotateTime;
    public float RotateTime { get { return rotateTime; } }

    [SerializeField] private Vector2 goingBetweenTwoTransformsValue; //rotateValue
    public Vector2 GoingBetweenTwoTransformsValue { get { return goingBetweenTwoTransformsValue; } }


}
