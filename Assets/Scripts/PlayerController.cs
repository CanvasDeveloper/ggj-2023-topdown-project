using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class SpriteRotationHandler
{
    public GameObject sprite;
    public Vector3 startRotation;
    public Vector3 targetRotation;
}

[RequireComponent(typeof(InputReference))]
public class PlayerController : MonoBehaviour
{
    //Como não vai ter save até então,bobeira Scriptable
    [Header("Player Status")]
    [SerializeField] private float moveSpeed = 5f;

    [HorizontalLine(1, EColor.Green)]

    [Header("Rotate Sprites")]
    [SerializeField] private SpriteRotationHandler playerSpriteRotation;
    [SerializeField] private SpriteRotationHandler gunSpriteRotation;

    [HorizontalLine(1, EColor.Green)]

    [Header("Bullets")]
    //Para fins de teste
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoisiton;
    [SerializeField] private Transform gunPivot;

    //Ambos ainda colocados aqui para registro de variaveis
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float BulletDamage = 5f;
    [SerializeField] private float reloadWeapon = 5f;
    [SerializeField] private float delayWeapon = 5f;

    private bool isCanShoot = true;
    private Camera main;

    private Vector3 mouseWorldPosition;
    [SerializeField] private bool isLookingLeft;

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

        if (_inputReference.PauseButton.IsPressed)
        {
            //Preguica :)
            Debug.Log("Pause");
            GameManager.Instance.PauseResume();
        }

        if (GameManager.Instance && GameManager.Instance.Paused)
            return;

        mouseWorldPosition = main.ScreenToWorldPoint(_inputReference.MousePosition);

        UpdatePlayerRotation();
        UpdateSpriteSide();

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
        Vector3 targetDirection = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    /// <summary>
    /// Flipa a arma e o player
    /// </summary>
    private void UpdateSpriteSide()
    {
        //olhando para esquerda, mouse na direita
        if (isLookingLeft && mouseWorldPosition.x > transform.position.x)
        {
            isLookingLeft = false;
            playerSpriteRotation.sprite.transform.localRotation = Quaternion.Euler(playerSpriteRotation.startRotation);
            gunSpriteRotation.sprite.transform.localRotation = Quaternion.Euler(gunSpriteRotation.startRotation);
        }

        if(!isLookingLeft && mouseWorldPosition.x < transform.position.x)
        {
            isLookingLeft = true;
            playerSpriteRotation.sprite.transform.localRotation = Quaternion.Euler(playerSpriteRotation.targetRotation);
            gunSpriteRotation.sprite.transform.localRotation = Quaternion.Euler(gunSpriteRotation.targetRotation);
        }
    }

    private IEnumerator IE_CanShoot()
    {
        isCanShoot = false;
        yield return new WaitForSeconds(reloadWeapon);
        isCanShoot = true;
    }
}
