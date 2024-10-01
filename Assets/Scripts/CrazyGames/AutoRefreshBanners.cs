using CrazyGames;
using UnityEngine;

public class AutoRefreshBanners : MonoBehaviour
{
    private void Start()
    {
        if(CrazySDK.IsInitialized)
        {
            CrazySDK.Banner.RefreshBanners();
        }
    }
}
