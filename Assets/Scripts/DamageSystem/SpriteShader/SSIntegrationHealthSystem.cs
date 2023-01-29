using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SSIntegrationHealthSystem : MonoBehaviour
{
    //Statics
    private static string HIT_EFFECT = "HITEFFECT_ON";

    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        
    }

    public void HitEffect()
    {
        StartCoroutine(HitEffectRoutine());
    }

    private IEnumerator HitEffectRoutine()
    {
        material.EnableKeyword(HIT_EFFECT);

        yield return 0.2f;

        material.DisableKeyword(HIT_EFFECT);
    }
}
