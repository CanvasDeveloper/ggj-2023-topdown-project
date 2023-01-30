using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    //[field:] serve pra deixar variaveis que tem esse { get; set; } no final, visiveis no editor;
    [field:SerializeField] public float CurrentHealth { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }

    //diz que esse carinha alterou a vida (dano ou cura), e passa a vida atual
    public event Action<float, float> OnChangeHealth;
    //avisa que o gameObject foi morto
    public event Action OnDie;

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
        CurrentHealth -= damage;

        if (CurrentHealth < 0)
        {
            Die();
            return;
        }

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
    }

    public void Die()
    {
        OnDie?.Invoke();
    }

    /// <summary>
    /// Para curar
    /// </summary>
    /// <param name="amount">quantidade</param>
    public void Heal(float amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
    }
}
