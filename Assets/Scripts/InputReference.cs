using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReference : MonoBehaviour
{
    //quero pegar o valor, mas n�o deixo ningu�m de fora atualizar
    public Vector2 Movement { get; private set; } = Vector2.zero;

    private void Update()
    {
        UpdateMovementValue();
    }

    private void UpdateMovementValue()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var inputY = Input.GetAxisRaw("Vertical");

        Movement = new Vector2(inputX, inputY).normalized;
    }
}
