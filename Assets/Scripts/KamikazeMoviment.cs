using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeMoviment : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] private float distanceToTree;

    //Colocado para fazer movimentação de teste
    [SerializeField] Transform treeMother;
    [SerializeField] private bool isLookingLeft;

    [SerializeField] private float damage = 1;

    Animator anim;

    private bool canHit = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        treeMother = TreeController.Instance.transform;

    }
    private void Update()
    {
        Flip();

        if (Vector3.Distance(transform.position, treeMother.position) < distanceToTree)
        {
            //perto da arvore
            if (canHit)
            {
                canHit = false;
                anim.SetBool("isDie", true);
              
                //Destroy(gameObject);
               
            }

            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, treeMother.position, speed * Time.deltaTime);
    }

    public void Damage()
    {
        if(!canHit)
        TreeController.Instance.TakeDamage(damage);
    }

   

    private void Flip()
    {
        if (transform.position.x > treeMother.position.x && isLookingLeft)
        {
            isLookingLeft = false;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (transform.position.x < treeMother.position.x && !isLookingLeft)
        {
            isLookingLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

  

}
