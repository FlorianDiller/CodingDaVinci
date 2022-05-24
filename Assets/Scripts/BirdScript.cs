using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField]
    public float speed = 0.1f;
    public float minSpeed = 0.05f;
    public float maxSpeed = 5f;
    public float rotSpeed = 0.6f;
    public float liftSpeed;
    public float tiltAngle = 0.3f;
    public float minHeight = 50f;
    public float maxHeight = 400f;
    public float boundary = 500;
    //public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(Random.Range(100, 900), Random.Range(50, 300), Random.Range(100, 900));
        gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + 0.1f * speed * gameObject.transform.forward * Time.deltaTime * 150;
        //Turning //Boundary check, turns if not viable
        if (Mathf.Abs(gameObject.transform.position.x - 500) > boundary || Mathf.Abs(gameObject.transform.position.z - 500) > boundary)
        {
            gameObject.transform.Rotate(0.0f, 150 * rotSpeed * Time.deltaTime, 0.0f);
        }
    }
}
