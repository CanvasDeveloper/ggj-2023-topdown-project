using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TreeController : MonoBehaviour
{
    [SerializeField] private Image xpBar;
    [SerializeField] private int xpBarMax;
    [SerializeField] private int xpBarCurrent;


public void SetAddXp()
    {
        xpBar.fillAmount = (float)xpBarCurrent / xpBarMax;
    }


}
