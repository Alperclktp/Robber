using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class RadialRadar : MonoBehaviour
{
    [SerializeField] private RadialRadarSettings radialRadarSettings;

    [SerializeField] private EnemyController enemyController;

    [ReadOnly]
    public float alerttedTime;

    [SerializeField] private float alertCooldown;

    [Header("Debuggers")]
    [SerializeField] private bool isDetected;

    [SerializeField] private bool isAlert;

    private void Update()
    {
        SetRadialRadarScale();

        if (PlayerController.Instance.isDie)
        {
            NotDetectPlayer();
        }

        if (alerttedTime >= alertCooldown)
            alerttedTime = alertCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            DetectPlayer(other);

            player.inSight = true;

            enemyController.isRotating = false;

            enemyController.StopRotate();

            player.isSafe = true;

            player.isTrigger = true;

            player.rippleCoroutine = StartCoroutine(PlayerController.Instance.IERippleEffectBetweenTime());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DetectPlayer(other);

            PlayerController.Instance.isSafe = false;
        }
        else if (!PlayerController.Instance.isSafe && other.CompareTag("Flower"))
        {
            DetectPlayer(other);

            PlayerController.Instance.isSafe = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Flower"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            NotDetectPlayer();

            player.inSight = false;

            player.isSafe = true;

            player.isTrigger = false;

            player.StopRippleEffect();

            //Invoke("EnemyReturnRotateTime", enemyController.turnCheckTime);

            enemyController.RotateEnemy(enemyController.goingBetweenTwoTransformsValue.x);

            enemyController.isRotating = true;

            enemyController.canFire = false;

            enemyController.SetAnimation();
        }
    }

    private void EnemyReturnRotateTime()
    {
        enemyController.RotateEnemy(enemyController.goingBetweenTwoTransformsValue.x);

        enemyController.isRotating = true;
    } //Not used funcition.

    private void DetectPlayer(Collider other)
    {
        alerttedTime += Time.deltaTime;

        Alert(other, alertCooldown);

        SetColor(GetComponent<Renderer>());

        SetAlphaColor(GetComponent<Renderer>(), radialRadarSettings.Opacity);
    }

    private void NotDetectPlayer()
    {
        isDetected = false;

        isAlert = false;

        alerttedTime = 0;

        enemyController.exclamationMarkEffect.Stop();

        SetDefaultColor(GetComponent<Renderer>());

        SetAlphaColor(GetComponent<Renderer>(), radialRadarSettings.Opacity);

        enemyController.canFollowMove = false;

        //StartCoroutine(enemyController.IERadarCheckReturnTime());

        enemyController.SetAnimation();
    }

    private void SetDefaultColor(Renderer renderer)
    {
        renderer.material.SetColor("_Color", radialRadarSettings.DefaultColor);

        SetAlphaColor(GetComponent<Renderer>(), radialRadarSettings.Opacity);
    }

    private void SetColor(Renderer renderer)
    {
        renderer.material.SetColor("_Color", radialRadarSettings.DetectColor);
    }

    private void SetAlphaColor(Renderer renderer, int value)
    {
        Color32 color = renderer.material.GetColor("_Color");

        color.a = (byte)value;

        renderer.material.SetColor("_Color", color);
    }

    private void SetRadialRadarScale()
    {
        Vector3 scale = transform.localScale = new Vector3(radialRadarSettings.RadialRadarScale, transform.localScale.y, radialRadarSettings.RadialRadarScale);
        transform.localScale = scale;
    }

    private void Alert(Collider other, float alertEndTime)
    {
        if (alerttedTime >= alertEndTime)
        {
            enemyController.FollowPlayer();

            enemyController.StopRotate();

            enemyController.LookAtPlayer(other.gameObject.transform);

            isDetected = true;

            isAlert = false;
        }
        else
        {
            enemyController.exclamationMarkEffect.Play();

            isAlert = true;
        }
    }
}
