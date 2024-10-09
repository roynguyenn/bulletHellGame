using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float movespeed = 5f;
    public float bulletLife = 8f;
    public Vector3 bulletDirection = new Vector3(0,0,0);
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

        transform.position += movespeed * Time.deltaTime * bulletDirection;
    }
}
