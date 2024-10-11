using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e : MonoBehaviour
{
    public float movespeed = 3f;
    public float amplitude = 2f;
    public float frequency = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(1, -1, 0);
        Vector3 moveY = new Vector3(1,1,0) * Mathf.Sin(Time.time * frequency) * amplitude;
   
        transform.position += (moveY + direction) * Time.deltaTime;

    }
}
