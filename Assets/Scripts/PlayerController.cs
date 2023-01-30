using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReference))]
public class PlayerController : MonoBehaviour
{
    //Como não vai ter save até então,bobeira Scriptable
    [Header("Player Status")]
    [SerializeField] private float moveSpeed = 5f;


    [Header("Bullets")]
    //Para fins de teste
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoisiton;
    //Ambos ainda colocados aqui para registro de variaveis
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float BulletDamage = 5f;
    [SerializeField] private float reloadWeapon = 5f;
    [SerializeField] private float delayWeapon = 5f;

    private bool isCanShoot = true;
    private Camera main;

    private InputReference _inputReference;
    private Rigidbody2D _rigidbody2D;

    private IDamageable health;

    private void Awake()
    {
        _inputReference = GetComponent<InputReference>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        health = GetComponent<IDamageable>();
    }

    private void Start()
    {
        main = Camera.main;
    }

    private void Update()
    {
        if (health.IsDie)
            return;

        UpdatePlayerRotation();

        if(_inputReference.PauseButton.IsPressed)
        {
            //Preguica :)
            GameManager.Instance.PauseResume();
        }

        if (_inputReference.ShootButton.IsPressed)
        {
            if (!isCanShoot)
                return;

            GameObject temp = Instantiate(bulletPrefab, bulletPoisiton.position, bulletPoisiton.rotation);
            Debug.Log("apertou mouse");
            StartCoroutine(IE_CanShoot());
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _inputReference.Movement * moveSpeed;
    }

    /// <summary>
    /// Rotaciona o player
    /// </summary>
    private void UpdatePlayerRotation()
    {
        Vector3 mouseWorldPosition = main.ScreenToWorldPoint(_inputReference.MousePosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private IEnumerator IE_CanShoot()
    {
        isCanShoot = false;
        yield return new WaitForSeconds(0.5f);
        isCanShoot = true;
    }
}
