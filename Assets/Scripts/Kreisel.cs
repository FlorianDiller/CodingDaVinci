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
        gameObject.transform.GetChild(0).localRotation = Quaternion.Euler(-90f, gameObject.transform.parent.eulerAngles.y - 90f, -90f);
    }
}
