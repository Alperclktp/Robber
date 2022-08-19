using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectedItem = new List<GameObject>();

    [SerializeField] private Transform itemHolder;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            AddItem(collectable.Collect());

            collectable.CollectEffect(collision);

            Debug.Log("Collected Item: " + collision.gameObject.name);
        }
    }

    public void AddItem(GameObject obj)
    {
        if (!collectedItem.Contains(obj))
        {
            obj.transform.SetParent(itemHolder);

            obj.transform.DOLocalJump(Vector3.zero, 1, 1, 0.5f).OnComplete(() => {

                obj.transform.gameObject.SetActive(false);

                //obj.transform.localPosition = new Vector3(0,itemList.Count,0);

                obj.transform.rotation = new Quaternion(0, 0, 0, 0);

                collectedItem.Add(obj);
            });
        }               
    }
}
