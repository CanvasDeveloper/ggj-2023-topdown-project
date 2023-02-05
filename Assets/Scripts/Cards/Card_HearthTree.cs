using UnityEngine;

public class Card_HearthTree : MonoBehaviour
{
    public void SetHearthTree(int valor)
    {
        TreeController.Instance.Heal(valor);
        GameManager.Instance.Wave();

    }

    public void SetMaxLifeTree(int valor)
    {
        TreeController.Instance.AddMaxHP(valor);
        GameManager.Instance.Wave();

    }

    public void SetSpeedPlayer(int valor)
    {
        PowerUpController.Instance.m_player.moveSpeed += valor;
        GameManager.Instance.Wave();

    }
}
