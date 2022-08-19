using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    [SerializeField] private Transform player;

    private void Awake() => player = GameObject.Find("Player").transform;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!PlayerController.Instance.isDie)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, 0.5f, 0), Time.deltaTime * bulletSpeed);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Flower"))
        {
            Destroy(this.gameObject);

            Debug.Log("Fire");

            CameraController.Insntace.CameraShakeEffect();
        }
    }
}
