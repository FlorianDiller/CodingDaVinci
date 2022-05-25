using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    public int cloudNumber = 10;
    public GameObject cloudPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cloudNumber; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(0, 1000), Random.Range(100, 400), Random.Range(0, 1000)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
