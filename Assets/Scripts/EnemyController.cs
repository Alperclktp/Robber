using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class EnemyController : MonoBehaviour
{
    [Header("Target References")]
    [SerializeField] private GameObject enemy;

    [SerializeField] private GameObject target;

    [HideInInspector] public Animator anim;

    private float nextFire;

    [Header("References")]
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform firePoint;

    //private Sequence sequence;

    [Header("Movement")]
    [SerializeField] private float runSpeed;

    [Header("Fire Settings")]

    [SerializeField] private float damage;

    [SerializeField] private float fireDistance;

    [SerializeField] private float fireRate;

    [Header("Rotate")]
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float rotateTime;

    //public float turnCheckTime;

    public Vector2 goingBetweenTwoTransformsValue;

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem muzzleEffect;

    public ParticleSystem exclamationMarkEffect;

    [Header("Debuggers")]
    public bool canFollowMove;

    public bool isRotating;

    public bool canFire;

    public Coroutine rotateCoroutine;

    private void Awake() => anim = GetComponent<Animator>();

    private void Start()
    {
        nextFire = Time.time;

        RotateEnemy(goingBetweenTwoTransformsValue.x);

        isRotating = true;
    }

    private void Update()
    {
        if (PlayerController.Instance.isDie)
        {
            canFire = false;
        }
    }

    public void LookAtPlayer(Transform player)
    {
        enemy.transform.LookAt(player);
    }

    public void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > fireDistance)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target.transform.position, runSpeed * Time.deltaTime);

            canFollowMove = true;

            canFire = false;

            StopRotate();
            
            muzzleEffect.Stop();

            SetAnimation();
        }
        else
        {
            canFollowMove = false;

            canFire = true;

            SetAnimation();
        }
    }

    public void Fire()
    {
        CheckTimeToFire();
    }

    public void CheckTimeToFire()
    {
        if (!PlayerController.Instance.isDie)
        {
            if (Time.time > nextFire)
            {
                Instantiate(bullet, firePoint.position, Quaternion.identity);

                muzzleEffect.Play();

                PlayerController.Instance.TakeDamage(damage);

                nextFire = Time.time + fireRate;
            }
        }
        else
        {
            muzzleEffect.Stop();
        }
    }

    public void RotateEnemy(float value)
    {
        //sequence = DOTween.Sequence();

        //if (!canFollowMove && !canFire && isRotating)
        //{
        //    sequence.Append(transform.DOLocalRotate(new Vector3(0, value, 0), rotateTime).OnComplete(() =>
        //    {
        //        if (!canFollowMove && !canFire)
        //        {
        //            if (goingBetweenTwoTransformsValue.x == value)
        //            {
        //                RotateEnemy(goingBetweenTwoTransformsValue.y);
        //            }
        //            else
        //            {
        //                RotateEnemy(goingBetweenTwoTransformsValue.x);
        //            }
        //        }
        //    }));
        //}

        rotateCoroutine = ToRotate(rotateTime, value,
        () =>
        {
            if (!canFollowMove && !canFire && isRotating)
            {
                if (goingBetweenTwoTransformsValue.x == value)
                {
                    RotateEnemy(goingBetweenTwoTransformsValue.y);
                }
                else
                {
                    RotateEnemy(goingBetweenTwoTransformsValue.x);
                }
            }
        });
    }


    private Coroutine ToRotate(float timer, float value, Action onComplete)
    {
        return StartCoroutine(IERotateTime(timer, value, onComplete));
    }

    private IEnumerator IERotateTime(float timer, float value, Action onComplete)
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //value += ((endValue - value) / timer) * Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, value, 0), Time.deltaTime * rotateSpeed);
            yield return null;
        }
        onComplete.Invoke();
    }

    public void StopRotate()
    {
        StopCoroutine(rotateCoroutine);
    }

    public void SetAnimation()
    {
        if (canFollowMove)
        {
            anim.SetBool("CanRun", true);
        }
        else
        {
            anim.SetBool("CanRun", false);
        }

        if (canFire)
        {
            anim.SetBool("CanFire", true);
        }
        else
        {
            anim.SetBool("CanFire", false);
        }
    }
}
