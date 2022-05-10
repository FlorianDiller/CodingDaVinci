using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float rotSpeed;
    public float liftSpeed;
    public float tiltAngle = 0.3f;
    public float minHeight = 85f;
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
        speed = speed + v * 0.01f;
        gameObject.transform.position = gameObject.transform.position + speed * gameObject.transform.forward;

        //Rotation and tilting while doing so
        gameObject.transform.Rotate(0.0f, rotSpeed * h, 0.0f);
        if (Input.GetAxisRaw("Horizontal") != 0 && Mathf.Abs(gameObject.transform.GetChild(0).transform.localRotation.z) < tiltAngle)
        {
            gameObject.transform.GetChild(0).transform.Rotate(0, 0, -0.2f* Input.GetAxisRaw("Horizontal"));
        }
        else //Tilt back to normal
        {
            if (gameObject.transform.GetChild(0).transform.localRotation.z>0)
            {
                gameObject.transform.GetChild(0).transform.Rotate(0, 0, -0.2f);
            }
            else
            {
                gameObject.transform.GetChild(0).transform.Rotate(0, 0, 0.2f);
            }
        }

        //Ascent and Descent
        if (Input.GetKey("space"))
        {
            gameObject.transform.position = gameObject.transform.position + liftSpeed * gameObject.transform.up;
            if (Mathf.Abs(gameObject.transform.GetChild(0).transform.localRotation.x) < tiltAngle)
            {
                gameObject.transform.GetChild(0).transform.Rotate(-0.2f, 0, 0);
            }
        }
        if (Input.GetKey("c") && gameObject.transform.position.y > minHeight)
        {
            if (Mathf.Abs(gameObject.transform.GetChild(0).transform.localRotation.x) < tiltAngle)
            {
                gameObject.transform.GetChild(0).transform.Rotate(0.2f, 0, 0);
            }
            gameObject.transform.position = gameObject.transform.position + liftSpeed * -gameObject.transform.up;
        }
        //Tilt back to normal
        if (gameObject.transform.GetChild(0).transform.localRotation.x > 0 && !Input.GetKey("c"))
        {
            gameObject.transform.GetChild(0).transform.Rotate(-0.2f,0, 0);
        }
        if (gameObject.transform.GetChild(0).transform.localRotation.x < 0 && !Input.GetKey("space"))
        {
                gameObject.transform.GetChild(0).transform.Rotate(0.2f,0, 0);
        }
    }
}
