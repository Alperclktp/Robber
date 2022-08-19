using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public GameObject Collect() { return gameObject; }

    public void CollectEffect(Collision collision)
    {
        Destroy(VFXManager.SpawnEffect(VFXType.Money, collision.transform.position, Quaternion.identity), 1f);
    }
}

