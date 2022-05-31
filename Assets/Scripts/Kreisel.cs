using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kreisel : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, -player.transform.eulerAngles.y);
    }
}
