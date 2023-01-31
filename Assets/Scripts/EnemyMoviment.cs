using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    [SerializeField] float speed;

    //Colocado para fazer movimentação de teste
    [SerializeField] Transform treeMother;

    private void OnEnable()
    {
        treeMother = GameObject.FindGameObjectWithTag("Tree").transform;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, treeMother.position, speed * Time.deltaTime);
    }
}
