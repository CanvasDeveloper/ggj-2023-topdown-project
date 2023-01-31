using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;
using System;

public class TreeController : MonoBehaviour, IDamageable
{
    [HorizontalLine(1, EColor.Green)]
    [SerializeField] private Image xpBar;
    [SerializeField] private int xpBarMax;
    [SerializeField] private int xpBarCurrent;
    [HorizontalLine(1, EColor.Green)]

    [HorizontalLine(1, EColor.Red)]
    [SerializeField] private Image lifeBar;
    [SerializeField] private int lifeBarMax;
    [SerializeField] private int lifeBarCurrent;

    public event Action<float, float> OnChangeHealth;
    public event Action OnTakeDamage;
    public event Action OnHeal;
    public event Action OnDie;

    public float CurrentHealth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MaxHealth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsDie { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private void Start()
    {
        //so para teste
        lifeBarCurrent = lifeBarMax;
        SetlifeBar();
    }

    public void SetAddXp()
    {
        xpBar.fillAmount = (float)xpBarCurrent / xpBarMax;
    }

    public void SetlifeBar()
    {
        lifeBar.fillAmount = (float)lifeBarCurrent / lifeBarMax;
    }

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }

    public void Heal(float amount)
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        throw new NotImplementedException();
    }
}
