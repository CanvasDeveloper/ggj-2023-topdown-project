using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemySystem : MonoBehaviour
{
    public enum DetectionType
    {
        Collision,
        Trigger,
    }

    [SerializeField] private DetectionType type;
    [Tooltip("Quantidade de dano")]
    [SerializeField] private float damage;
    [Tooltip("Quantidade de cura")]
    [SerializeField] private float heal;

    [SerializeField] private bool destroyOnCollide = true;

    [ShowIf("destroyOnCollide")]
    [SerializeField] private GameObject spawnEffectPrefab;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type != DetectionType.Collision)
            return;

        switch (collision.gameObject.tag)
        {
            case "Player":
                TreeController.Instance.TakeDamage(damage);
                if (collision.gameObject.TryGetComponent(out IDamageable damageable) && collision.gameObject.CompareTag("Player"))
                {
                    damageable.TakeDamage(damage);
                    
                }

                break;
            case "Tree":
                  TreeController.Instance.TakeDamage(damage);
                if (collision.gameObject.TryGetComponent(out IDamageable damageable1) && collision.gameObject.CompareTag("Tree"))
                {
                    damageable1.TakeDamage(damage);
                   
                }

                break;
        }
       
        DestroyObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type != DetectionType.Trigger)
            return;

        switch (collision.gameObject.tag)
        {
            case "Player":
                TreeController.Instance.TakeDamage(damage);
                if (collision.gameObject.TryGetComponent(out IDamageable damageable) && collision.gameObject.CompareTag("Player"))
                {
                    damageable.TakeDamage(damage);

                }

                break;
            case "Tree":
                TreeController.Instance.TakeDamage(damage);
                if (collision.gameObject.TryGetComponent(out IDamageable damageable1) && collision.gameObject.CompareTag("Tree"))
                {
                    damageable1.TakeDamage(damage);

                }

                break;
        }

        DestroyObject();
    }

    public void SetChangeDamage(float valor)
    {
        damage += valor;
    }

    private void DestroyObject()
    {
        if (!destroyOnCollide)
            return;

        if (spawnEffectPrefab)
            Instantiate(spawnEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
