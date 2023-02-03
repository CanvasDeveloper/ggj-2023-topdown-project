using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGamePlay : MonoBehaviour
{

    public GameObject CameraTarget;
    public GameObject PainelStart;


    private void Awake()
    {
        Time.timeScale = 0;
    }
    private void Update()
    {


        if (InputSystem.GetDevice<Mouse>() != null)
        {
            if (InputSystem.GetDevice<Mouse>().leftButton.wasPressedThisFrame)
            {
                SetOnStart();
            }
        }
    }


    public void SetOnStart()
    {
        CameraTarget.SetActive(true);
        PainelStart.SetActive(false);
        Time.timeScale = 1;
        Destroy(this);

    }

}
