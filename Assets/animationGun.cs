using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationGun : MonoBehaviour
{
    public PlayerController m_player;

   
    public void animShoot()
    {
        for (int i = 0; i < m_player.bulletPoisiton.Length; i++)
        {
            if (m_player.bulletPoisiton[i].gameObject.activeSelf == true)
            {
                GameObject temp = Instantiate(m_player.bulletPrefab, m_player.bulletPoisiton[i].position, m_player.bulletPoisiton[i].rotation);
            }


        }
    }
}
