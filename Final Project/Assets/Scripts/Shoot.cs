using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody fireRb;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        fireRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        fireRb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
