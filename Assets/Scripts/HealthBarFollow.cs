using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerController.Instance.playerModel.transform.position.x, transform.position.y, PlayerController.Instance.playerModel.transform.position.z), Time.deltaTime * 60f);
    }
}
