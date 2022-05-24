using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public float speed = 0.1f;
    public float minSpeed = 0.05f;
    public float maxSpeed = 5f;
    public float rotSpeed;
    public float liftSpeed;
    public float tiltAngle = 0.3f;
    public float minHeight = 50f;
    public float maxHeight = 400f;
    public float boundary = 500;
    //public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        speed = Mathf.Clamp(speed + v * 0.01f,minSpeed,maxSpeed);
        gameObject.transform.position = gameObject.transform.position + 0.1f * speed * gameObject.transform.forward * Time.deltaTime * 150;

        //Turning //Boundary check, turns if not viable
        if (Mathf.Abs(gameObject.transform.position.x - 500) < boundary && Mathf.Abs(gameObject.transform.position.z - 500) < boundary)
        {
            gameObject.transform.Rotate(0.0f, 150 * rotSpeed * h * Time.deltaTime, 0.0f);
        }
        else
        {
            gameObject.transform.Rotate(0.0f, 150 * rotSpeed * Time.deltaTime, 0.0f);
            if (Mathf.Abs(gameObject.transform.GetChild(0).transform.localRotation.z) < tiltAngle)
            {
                gameObject.transform.GetChild(0).transform.Rotate(0, 0, -150 * Time.deltaTime);
            }
        }

        //Tilting while turning
        if (Input.GetAxisRaw("Horizontal") != 0 && Mathf.Abs(gameObject.transform.GetChild(0).transform.localRotation.z) < tiltAngle && !Input.GetKey("c") && !Input.GetKey("space"))
        {
            gameObject.transform.GetChild(0).transform.Rotate(0, 0, -75 * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        }
        else //Tilt back to normal
        {
            if (gameObject.transform.GetChild(0).transform.localRotation.z>0)
            {
                gameObject.transform.GetChild(0).transform.Rotate(0, 0, -75 * Time.deltaTime);
            }
            else
            {
                gameObject.transform.GetChild(0).transform.Rotate(0, 0, 75 * Time.deltaTime);
            }
        }

        //Ascent and Descent
        if (Input.GetKey("space") && gameObject.transform.position.y < maxHeight && Input.GetAxisRaw("Horizontal") == 0)
        {
            gameObject.transform.position = gameObject.transform.position + liftSpeed * 75 * gameObject.transform.up * Time.deltaTime;
            if (Mathf.Abs(gameObject.transform.GetChild(0).transform.localRotation.x) < tiltAngle)
            {
                gameObject.transform.GetChild(0).transform.Rotate(-75 * Time.deltaTime, 0, 0);
            }
        }
        if (Input.GetKey("c") && gameObject.transform.position.y > minHeight && Input.GetAxisRaw("Horizontal") == 0)
        {
            if (Mathf.Abs(gameObject.transform.GetChild(0).transform.localRotation.x) < tiltAngle)
            {
                gameObject.transform.GetChild(0).transform.Rotate(75 * Time.deltaTime, 0, 0);
            }
            gameObject.transform.position = gameObject.transform.position + liftSpeed * 75 * -gameObject.transform.up * Time.deltaTime;
        }
        //Tilt back to normal
        if (gameObject.transform.GetChild(0).transform.localRotation.x > 0 && !Input.GetKey("c"))
        {
            gameObject.transform.GetChild(0).transform.Rotate(-75 * Time.deltaTime,0, 0);
        }
        if (gameObject.transform.GetChild(0).transform.localRotation.x < 0 && !Input.GetKey("space"))
        {
                gameObject.transform.GetChild(0).transform.Rotate(75 * Time.deltaTime,0, 0);
        }
    }
}
