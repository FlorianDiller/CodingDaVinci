using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kreisel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localRotation = Quaternion.Euler(gameObject.transform.parent.rotation.y, 90, 90);
    }
}
