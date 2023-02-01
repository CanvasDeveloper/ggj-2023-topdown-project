using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;
using System;

public class TreeController : Singleton<TreeController>, IDamageable
{
    [HorizontalLine(1, EColor.Green)]
    [SerializeField] private Image xpBar;
    [SerializeField] private int xpBarMax;
     public int xpBarCurrent;
    [HorizontalLine(1, EColor.Green)]

    [HorizontalLine(1, EColor.Red)]
    [SerializeField] private Image lifeBar;

    public event Action<float, float> OnChangeHealth;
    public event Action OnTakeDamage;
    public event Action OnHeal;
    public event Action OnDie;

    [field: SerializeField] public float CurrentHealth { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }
    public bool IsDie { get; set; }

    private void Start()
    {
      
        CurrentHealth = MaxHealth;
        SetlifeBar(CurrentHealth,MaxHealth);
    }

    private void OnEnable()
    {
        OnChangeHealth += SetlifeBar;
    }

    private void OnDisable()
    {
        OnChangeHealth -= SetlifeBar;
    }

    public void SetAddXp(int xp)
    {
     
            xpBarCurrent += xp;
            if(xpBarCurrent >= xpBarMax)
            {
                GameManager.Instance.PowerUP();
                xpBarCurrent = xpBarMax;
            }
        
        xpBar.fillAmount = (float)xpBarCurrent / xpBarMax;
    }

    public void SetlifeBar(float currrentHealth, float maxHealth)
    {
        lifeBar.fillAmount = currrentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            Die();
            return;
        }

        OnTakeDamage?.Invoke();
        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
    }

    public void Heal(float amount)
    {

        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        OnHeal?.Invoke();
    }

    public void Die()
    {
        IsDie = true;
        OnDie?.Invoke();
        GameManager.Instance.GameOver();
    }
}
