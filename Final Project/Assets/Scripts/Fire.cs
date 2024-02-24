using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject projectilePrefab;
    public bool isDown = false;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDown = true;
            StartCoroutine(SpawnFireProjectile());
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDown = false;
        }
    }
    IEnumerator SpawnFireProjectile()
    {
        while (isDown)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
