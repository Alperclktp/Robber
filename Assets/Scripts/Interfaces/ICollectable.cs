using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    GameObject Collect();

    void CollectEffect(Collision collision);
}
