using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public float speed;
    [SerializeField]
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = player.GetComponent<Movement>().speed;
        transform.GetComponent<AudioSource>().pitch = Mathf.Lerp(0.5f,1.4f,speed/5);
    }
}
