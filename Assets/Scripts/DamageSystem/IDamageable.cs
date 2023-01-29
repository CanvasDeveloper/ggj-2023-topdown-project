using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public event Action<float, float> OnChangeHealth; //current / max
    public event Action OnDie;
    public void TakeDamage(float damage); //pra tomar dano
    public void Heal(float amount); //pra curar
    public void Die();
}
