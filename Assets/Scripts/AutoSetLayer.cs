using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sRenderer;
    [SerializeField] private Transform reference;
    [SerializeField] private int startIndex;
    [SerializeField] private int newIndex;

    private void Update()
    {
        sRenderer.sortingOrder = transform.position.y > reference.position.y ? newIndex : startIndex;
    }
}

