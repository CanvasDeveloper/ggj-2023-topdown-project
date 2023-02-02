using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCenourita : MonoBehaviour
{
    [SerializeField] private float lifeTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IE_LifeTime());
    }

    private IEnumerator IE_LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
