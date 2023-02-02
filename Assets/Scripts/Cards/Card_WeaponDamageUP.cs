using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_WeaponDamageUP : MonoBehaviour
{

    private int bulletIndexPosition;

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

    public void SetActiveCenourita()
    {
        PowerUpController.Instance.m_player.patrono.SetActive(true);
        GameManager.Instance.Wave();
    }

    public void SetCenouritaDelay(float valor)
    {
        CenouritaController var = PowerUpController.Instance.m_player.patrono.GetComponent<CenouritaController>();
        var.delayAttack -= valor;
        GameManager.Instance.Wave();
    }

    public void SetCenouritaUpDamage(float valor)
    {
        //Sim... preguiça 
        CenouritaController var = PowerUpController.Instance.m_player.patrono.GetComponent<CenouritaController>();
        var.prefabBullet.GetComponent<DetectHealthSystem>().SetChangeDamage(valor);
        GameManager.Instance.Wave();
    }


    public void SetActiveBulletPosition()
    {
        if(PowerUpController.Instance.m_player.bulletPoisiton[PowerUpController.Instance.m_player.bulletPoisiton.Length].gameObject.activeSelf == true)
        {
            GameManager.Instance.Wave();
            return;
        }

        foreach (Transform go in PowerUpController.Instance.m_player.bulletPoisiton)
        {
            if(go.gameObject.activeSelf == false)
            {
                go.gameObject.SetActive(true);
                GameManager.Instance.Wave();
                return;
            }
           
        }
       
      
    }

}
