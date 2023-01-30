using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>
/// Simula um botao, para evitar que seja trigado mais de uma vez no update
/// </summary>
public class InputButton
{
    public bool IsPressed;

 

}

/// <summary>
/// Usado pelo player
/// </summary>
public class InputReference : MonoBehaviour, PlayerInputMap.IGameplayActions
{
    //quero pegar o valor, mas nao deixo ninguem de fora atualizar
    public Vector2 Movement { get; private set; } = Vector2.zero;
    public InputButton PauseButton { get; private set; } = new InputButton();

    private PlayerInputMap playerInputs;

    //Para fins de teste
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoisiton;
    private bool isCanShoot = true;
    private Camera main;

    private void Start()
    {
        playerInputs = new PlayerInputMap();

        playerInputs.Gameplay.SetCallbacks(this);
        playerInputs.Enable();
        main = Camera.main;
    }

    private void Update()
    {
        //Rotaciona o player
        Vector2 mouseScreenPosition = playerInputs.Gameplay.MousePosition.ReadValue<Vector2>();
        Vector3  mouseWorldPosition = main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

    

        //Old input
        //UpdateMovementValue();
        //UpdatePauseValue();
    }

    #region OLD INPUT
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
    #endregion


    private IEnumerator IE_CanShoot()
    {
        isCanShoot = false;
        yield return new WaitForSeconds(0.5f);
        isCanShoot = true;
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        Vector2 mousePoision = context.ReadValue<Vector2>();
        mousePoision = Camera.main.ScreenToWorldPoint(mousePoision);
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isCanShoot)
                return;

            GameObject temp = Instantiate(bulletPrefab, bulletPoisiton.position, bulletPoisiton.rotation);
            Debug.Log("apertou mouse");
            StartCoroutine(IE_CanShoot());
        }       
            
        
       
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();

        Movement = new Vector2(input.x, input.y).normalized;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        PauseButton.IsPressed = context.ReadValueAsButton();
        StartCoroutine(ResetButton(PauseButton));
    }

    private IEnumerator ResetButton(InputButton button)
    {
        yield return new WaitForEndOfFrame();

        if (button.IsPressed)
            button.IsPressed = false;
    }
}
