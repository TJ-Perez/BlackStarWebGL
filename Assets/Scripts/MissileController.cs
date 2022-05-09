using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float moveSpeed = 400f;
    public float lifeTime = 5;
    private Rigidbody rb;
    public float addObstaclePoint;
    public GameObject gameManagerObject;
    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
        gameManagerObject = GameObject.Find("SceneGameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0f, 0f, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("UFO"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);

        }
        addObstaclePoint = col.gameObject.tag == "Asteroid"
        ? 100
        : 200;
        Debug.Log("Added additional points " + addObstaclePoint);
        gameManager.AddPoints(addObstaclePoint);

    }


}
