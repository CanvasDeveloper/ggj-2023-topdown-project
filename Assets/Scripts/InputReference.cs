using System.Collections;
using UnityEngine;

public class InputButton
{
    public bool IsPressed;
}

public class InputReference : MonoBehaviour
{
    //quero pegar o valor, mas nao deixo ninguem de fora atualizar
    public Vector2 Movement { get; private set; } = Vector2.zero;
    public InputButton PauseButton { get; private set; } = new InputButton();

    private void Update()
    {
        UpdateMovementValue();
        UpdatePauseValue();
    }

    private void UpdateMovementValue()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var inputY = Input.GetAxisRaw("Vertical");

        Movement = new Vector2(inputX, inputY).normalized;
    }

    private void UpdatePauseValue()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseButton.IsPressed = true;
            StartCoroutine(ResetButton(PauseButton));
        }

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
            PauseButton.IsPressed = false;
    }

    private IEnumerator ResetButton(InputButton button)
    {
        yield return new WaitForEndOfFrame();
        button.IsPressed = false;
    }
}
