using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenouritaController : MonoBehaviour
{
    
    public bool isTouchPlayer; 

    [SerializeField]
    private IAVisionCircle _iaVision;
    public Collider2D[] hitInfo;

    public Transform lastPlayerPosition;

    public GameObject prefabBullet;

    public float shotSpeed;

    private bool isDelay;
    public Transform shotPosition;

    public float delayAttack = 2.3f;

    [HideInInspector]
    public int directionBullet;
    public bool isAttack;

    private Vector2 direction;
    public bool isLookLeft = true;
    public bool enemy;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isAttack = false;
        isDelay = false;
    }

    private void FixedUpdate()
    {

        if (_iaVision != null)
        {
            hitInfo = Physics2D.OverlapCircleAll(_iaVision.hitBox.position, _iaVision.visionRange, _iaVision.hitMask);

            // Teste para pegar o mais perto com Bug ainda
            if (hitInfo.Length != 0 && !isTouchPlayer)
            {
                Transform curentTarget = this.transform;
                float distanceToTarget = Mathf.Infinity;
                for (int i = 0; i < hitInfo.Length; i++)
                {
                    float newDistance = (hitInfo[i].transform.position - transform.position).magnitude;
                    if (newDistance < distanceToTarget)
                    {
                        curentTarget = hitInfo[i].transform;
                        distanceToTarget = newDistance;
                    }
                }
                lastPlayerPosition = curentTarget;
                isTouchPlayer = true;

            }

        }

    }



    // Update is called once per frame
    void Update()
    {
        if (lastPlayerPosition != null)
        {
            if (isTouchPlayer)
            {
                if (lastPlayerPosition.transform.position.x < transform.position.x && isLookLeft == false)
                {
                    Flip();
                }
                else if (lastPlayerPosition.transform.position.x > transform.position.x && isLookLeft == true)
                {
                    Flip();

                }
               

                if(!enemy)
                {
                    //Movimentação do torço 
                    if (isLookLeft)
                    {
                        transform.rotation = LookAtTarget(transform.position - lastPlayerPosition.position);

                    }
                    else
                    {
                        transform.rotation = LookAtTarget(lastPlayerPosition.position - transform.position);
                    }

                }
            }

        }
        else
        {
            isTouchPlayer = false;
        }



        if (isLookLeft)
        {
            directionBullet = -1;
        }
        else
        {
            directionBullet = 1;
        }

        if (isTouchPlayer == true && isAttack == false && lastPlayerPosition != null && !isDelay)
        {
            isAttack = true;
            StartCoroutine(DelayAttack());
            anim.SetTrigger("Attack");
           // shotAttacking();
        }


       


    }

    void shotAttacking()
    {
        // calcula a direção do míssil em relação ao inimigo
        direction = (lastPlayerPosition.position - transform.position).normalized;

        GameObject temp = Instantiate(prefabBullet);
        temp.transform.position = shotPosition.transform.position;
        // Aplica uma força na direção do inimigo
        temp.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shotSpeed);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Planta Carnivora/Carnivora Shot", GetComponent<Transform>().position);
    }

    public void Flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }


    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }


    private IEnumerator DelayAttack()
    {
        isDelay = true;
        yield return new WaitForSeconds(delayAttack);
        isDelay = false;
        isAttack = false;
        isTouchPlayer = false;

    }
}
