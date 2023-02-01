using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageUP : MonoBehaviour
{
   public void ChangeUP(int valor)
    {
        PowerUpController.Instance.m_player.BulletDamage += valor;
        GameManager.Instance.Wave();
        Destroy(gameObject);
    }

   

    
}
