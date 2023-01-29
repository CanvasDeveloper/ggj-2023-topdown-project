using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SSIntegrationHealthSystem : MonoBehaviour
{
    private readonly int HitParam = Animator.StringToHash("Hit");

    private Material material;

    private Animator shaderEffectsAnimator;

    private void Awake()
    {
        shaderEffectsAnimator = GetComponent<Animator>();
        material = GetComponent<Renderer>().material;
    }

    [ContextMenu("HIT EFFECT")] //usa isso nos 3 pontinhos do inspector
    public void HitEffect()
    {
        shaderEffectsAnimator.SetTrigger(HitParam);
    }
}
