using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskHandler : MonoBehaviour
{
    [SerializeField] private GameObject maskObject;

    private void Start()
    {
        maskObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MaskContactCollider"))
            maskObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MaskContactCollider"))
            maskObject.SetActive(false);
    }
}
