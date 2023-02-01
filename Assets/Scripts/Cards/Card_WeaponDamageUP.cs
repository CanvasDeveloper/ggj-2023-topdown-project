using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_WeaponDamageUP : MonoBehaviour
{
   public void ChangeUPWeapon(int valor)
    {
        PowerUpController.Instance.m_player.BulletDamage += valor;
        GameManager.Instance.Wave();
      
    }

    public void SetChangeDelayWeapon(float valor)
    {
        PowerUpController.Instance.m_player.delayWeapon -= valor;
        GameManager.Instance.Wave();

    }



}
