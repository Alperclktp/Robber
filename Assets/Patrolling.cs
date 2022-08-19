using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy patrolling test script.
public class Patrolling : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float timeSpentAtPoint;

    [SerializeField] private float rotateSpeed;

    [Header("Reference Points")]
    [SerializeField] private Transform point1;

    [SerializeField] private Transform point2;

    [Header("Debuggers")]
    public bool point1Check;

    public bool point2Check;

    private float yPos;

    private void Start()
    {
        yPos = transform.eulerAngles.y;

        StartCoroutine(IEPathFollow());
    }

    private IEnumerator IEPathFollow()
    {
        Transform target = point1;

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if(Vector3.Distance(transform.position,target.position) <= 0)
            {
                target = target == point1 ? point2 : point1;

                if(target == point1)
                {   
                    point2Check = true;
                    point1Check = false;

                }
                else if(target == point2)
                {
                    point1Check = true;
                    point2Check = false;
                }

                if (point1Check)
                {
                    Debug.Log("You are now at point one");

                    StartCoroutine(IERotate(180));
                }
                else if (point2Check)
                {
                    Debug.Log("You are now at point two");

                    StartCoroutine(IERotate(180));
                }

                yield return new WaitForSeconds(timeSpentAtPoint);
            }
            yield return null;          
        }
    }

    private IEnumerator IERotate(float value)
    {
        yPos += value;

        while (transform.eulerAngles.y != yPos)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yPos, 0f), rotateSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
