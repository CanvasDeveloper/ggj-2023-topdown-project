using UnityEngine;

public class AutoSetCrazyGameState : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.SetCrazyGamePlaying(true);
    }
}
