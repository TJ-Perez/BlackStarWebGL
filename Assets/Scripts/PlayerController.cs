using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float acceleration = 80;
    public float damage = 100f;
    public float particleRange = 100f;
    private Rigidbody rb;
    public Camera crosshair;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 0;
        rb.drag = 3.5f;
        rb.angularDrag = 0;
    }

    // Update is called once per frame
    void Update()
    {

        float moveLeftRight = Input.GetAxis("Horizontal");
        float moveForwardBack = Input.GetAxis("Vertical");

        Vector3 xAcceleration = new Vector3(1, 0, 0) * moveLeftRight * Time.deltaTime * acceleration;
        Vector3 yAcceleration = new Vector3(0, 1, 0) * moveForwardBack * Time.deltaTime * acceleration;

        rb.velocity += xAcceleration + yAcceleration;
        //rb.velocity += xAcceleration;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot ()
    {
        RaycastHit hit;
        if (Physics.Raycast(crosshair.transform.position, Vector3.forward, out hit, particleRange))
        {
            Debug.Log("object hit");
        }
       
    }
}
