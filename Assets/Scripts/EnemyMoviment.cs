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

    private void OnEnable()
    {
        treeMother = GameObject.FindGameObjectWithTag("Tree").transform;
    }
    private void Update()
    {
        Flip();

        if (Vector3.Distance(transform.position, treeMother.position) < distanceToTree)
        {
            //perto da arvore
            


            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, treeMother.position, speed * Time.deltaTime);
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
