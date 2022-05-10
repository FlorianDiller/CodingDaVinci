using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float rotSpeed;
    public float tiltAngle = 0.3f;
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
        gameObject.transform.Rotate(0.0f,rotSpeed*h,0.0f);
        speed = speed + v * 0.01f;
        gameObject.transform.position = gameObject.transform.position + speed * gameObject.transform.forward;
        
        if (Input.GetAxisRaw("Horizontal") != 0 && Mathf.Abs(gameObject.transform.GetChild(0).transform.localRotation.z) < tiltAngle)
        {
            gameObject.transform.GetChild(0).transform.Rotate(0, 0, -0.2f* Input.GetAxisRaw("Horizontal"));
            Debug.Log(gameObject.transform.GetChild(0).transform.localRotation.z);
        }
        else
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
        
    }
}
