using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float movespeed = 5f;
    public float bulletLife = 8f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bulletLife)
        {
            Destroy(gameObject);
            timer = 0f;
        }

        transform.position += transform.right * movespeed * Time.deltaTime;
    }
}
