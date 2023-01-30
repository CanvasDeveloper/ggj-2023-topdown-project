using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReference))]
public class PlayerController : MonoBehaviour
{
    //Como não vai ter save até então,bobeira Scriptable
    [Header("Player Status")]
    [SerializeField] private float moveSpeed = 5f;

    //Ambos ainda colocados aqui para registro de variaveis
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float BulletDamage = 5f;
    [SerializeField] private float reloadWeapon = 5f;
    [SerializeField] private float delayWeapon = 5f;


    private InputReference _inputReference;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _inputReference = GetComponent<InputReference>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_inputReference.PauseButton.IsPressed)
        {
            //Preguica :)
            GameManager.Instance.PauseResume();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _inputReference.Movement * moveSpeed;
    }
}
