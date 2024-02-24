using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMove : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed = 5.0f;
    public float xRangeR = 16.4f;
    public float xRangeL = 0;
    public float yRangeU = 8.4f;
    public float yRangeD = 0;
    public bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);
        Vector3 diagonalMovement = new Vector3(horizontalInput, verticalInput, 0.0f);
        transform.Translate(diagonalMovement * speed * Time.deltaTime);
        if (transform.position.y > yRangeU)
            transform.position = new Vector3(transform.position.x, yRangeU, transform.position.z);
        else if (transform.position.y < yRangeD)
            transform.position = new Vector3(transform.position.x, yRangeD, transform.position.z);
        if (transform.position.x > xRangeR)
            transform.position = new Vector3(xRangeR, transform.position.y, transform.position.z);
        else if (transform.position.x < xRangeL)
            transform.position = new Vector3(xRangeL, transform.position.y, transform.position.z);
    }
}
