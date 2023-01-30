using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public event Action<float, float> OnChangeHealth; //current / max usado somente para UI
    public event Action OnTakeDamage; //usado para parar o player e VFX
    public event Action OnHeal; //usado para VFX
    public event Action OnDie; //usado para parar o player
    public void TakeDamage(float damage); //pra tomar dano
    public void Heal(float amount); //pra curar
    public void Die();
}
