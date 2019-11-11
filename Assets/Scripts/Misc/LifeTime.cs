using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LifeTime : MonoBehaviour
{

    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        if (lifetime == 0)
        {
            lifetime = 5f;
        }

        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
        
    }
    
}
