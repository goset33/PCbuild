using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxer : MonoBehaviour, IInteractable
{
    public GameObject componentInBox;

    public void Interact()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.DOScale(transform.localScale * 1.5f, 1f);
        Instantiate(componentInBox, transform.position, Quaternion.identity);
        Destroy(gameObject, 1f);
    }
}
