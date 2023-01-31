using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHitSpriteShader : MonoBehaviour
{
    private IDamageable damageable;
    [SerializeField] private SpriteRenderer sRenderer;

    [SerializeField] private float speed = 4;
    [SerializeField] private float glow = 2;

    private void Awake()
    {
        damageable = GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        damageable.OnTakeDamage += HitEffect;
    }

    private void OnDisable()
    {
        damageable.OnTakeDamage -= HitEffect;
    }

    [ContextMenu("Test Take Hit Effect")]
    private void HitEffect()
    {
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        sRenderer.material.EnableKeyword("HITEFFECT_ON");

        sRenderer.material.SetFloat("_HitEffectGlow", glow);
        sRenderer.material.SetFloat("_HitEffectBlend", 0);

        while(sRenderer.material.GetFloat("_HitEffectBlend") < 0.9f)
        {
            var amount = Mathf.Lerp(sRenderer.material.GetFloat("_HitEffectBlend"), 1, speed * Time.deltaTime);
            sRenderer.material.SetFloat("_HitEffectBlend", amount);

            yield return new WaitForEndOfFrame();
        }

        sRenderer.material.SetFloat("_HitEffectBlend", 1);

        yield return new WaitForEndOfFrame();

        while (sRenderer.material.GetFloat("_HitEffectBlend") > 0.1f)
        {
            var amount = Mathf.Lerp(sRenderer.material.GetFloat("_HitEffectBlend"), 0, speed * Time.deltaTime);
            sRenderer.material.SetFloat("_HitEffectBlend", amount);

            yield return new WaitForEndOfFrame();
        }

        sRenderer.material.SetFloat("_HitEffectBlend", 0);
    }
}
