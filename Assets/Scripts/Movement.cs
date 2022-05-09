using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float rotSpeed;
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
        speed = speed + v;
        gameObject.transform.position = gameObject.transform.position + speed * gameObject.transform.forward;
        gameObject.transform.GetChild(0).Rotate(0.0f,0.0f,rotSpeed * h);
    }
}
