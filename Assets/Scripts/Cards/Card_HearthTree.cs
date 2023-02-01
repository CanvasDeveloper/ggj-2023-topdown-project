using UnityEngine;

public class Card_HearthTree : MonoBehaviour
{
    public void SetHearthTree(int valor)
    {
        TreeController.Instance.CurrentHealth += valor;
        if(TreeController.Instance.CurrentHealth > TreeController.Instance.MaxHealth)
        {
            TreeController.Instance.CurrentHealth = TreeController.Instance.MaxHealth;
        }
        TreeController.Instance.SetlifeBar(TreeController.Instance.CurrentHealth, TreeController.Instance.MaxHealth);
        GameManager.Instance.Wave();

    }
}
