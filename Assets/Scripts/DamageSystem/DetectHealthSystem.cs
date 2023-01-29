using UnityEngine;

public class DetectHealthSystem : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type != DetectionType.Collision)
            return;

        if(collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
            damageable.Heal(heal);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type != DetectionType.Trigger)
            return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
            damageable.Heal(heal);
        }
    }
}
