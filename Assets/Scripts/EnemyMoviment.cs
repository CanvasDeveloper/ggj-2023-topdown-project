using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private float distanceToTree;

    //Colocado para fazer movimentação de teste
    [SerializeField] Transform treeMother;
    [SerializeField] private bool isLookingLeft;

    [SerializeField] private float damage = 1;
    [SerializeField] private float timeToAttack = 0.5f;

    private Animator anim;
   

    private bool canHit = true;

    private void OnEnable()
    {
        treeMother = TreeController.Instance.transform;
        anim = GetComponent<Animator>();
      
    }
    private void Update()
    {
        Flip();

        if (Vector3.Distance(transform.position, treeMother.position) < distanceToTree)
        {
            //perto da arvore
            if (canHit)
            {
                anim.SetTrigger("attack");
                TreeController.Instance.TakeDamage(damage);
                canHit = false;
                StartCoroutine(WaitForHit());
            }

            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, treeMother.position, speed * Time.deltaTime);
    }

    private IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(timeToAttack);
        canHit = true;
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
