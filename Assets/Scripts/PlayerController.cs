using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReference))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
   
    private InputReference _inputReference;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _inputReference = GetComponent<InputReference>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _inputReference.Movement * moveSpeed;
    }
}
