using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    //[field:] serve pra deixar variaveis que tem esse { get; set; } no final, visiveis no editor;
    [field:SerializeField] public float CurrentHealth { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }
    public bool IsDie { get ; set ; }

    //diz que esse carinha alterou a vida (dano ou cura), e passa a vida atual
    public event Action<float, float> OnChangeHealth;
    //avisa que o gameObject foi morto
    public event Action OnDie;
    public event Action OnTakeDamage;
    public event Action OnHeal;

    [SerializeField] private bool destroyOnDie;

    [SerializeField] private int enemyXp;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
    }

    /// <summary>
    /// Tomar dano
    /// </summary>
    /// <param name="damage">dano</param>
    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        CurrentHealth -= damage;

        if (CurrentHealth < 0)
        {
            Die();
            return;
        }

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        OnTakeDamage?.Invoke();
    }

    public void Die()
    {
        OnDie?.Invoke();
        IsDie = true;
        TreeController.Instance.SetAddXp(enemyXp);

        if(destroyOnDie) //evita que o player seja destruido
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Para curar
    /// </summary>
    /// <param name="amount">quantidade</param>
    public void Heal(float amount)
    {
        if (amount <= 0)
            return;

        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        OnHeal?.Invoke();
    }
}
