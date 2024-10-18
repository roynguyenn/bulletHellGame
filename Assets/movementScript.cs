using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    public Renderer map;
    // Start is called before the first frame update
    float movespeed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movements();

    }
    public void Movements() {
        Vector3 movementVector = new Vector3(0,0,0);
         if (Input.GetKey(KeyCode.W)) {
            movementVector += new Vector3(0,1,0); 
            // transform.position += new Vector3(0,1,0) * movespeed * Time.deltaTime;
        }  
        if (Input.GetKey(KeyCode.S)) {
            movementVector += new Vector3(0,-1,0); 
            // transform.position += new Vector3(0,-1,0) * movespeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.D)) {
            movementVector += new Vector3(1,0,0); 
             // transform.position += new Vector3(1,0,0) * movespeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.A)) {
            movementVector += new Vector3(-1,0,0); 
            // transform.position += new Vector3(-1,0,0) * movespeed * Time.deltaTime;
        }
        movementVector.Normalize();
        transform.position += movementVector * movespeed * Time.deltaTime;
        boundPosition(map);
    }

    public void boundPosition(Renderer renderer){
        Vector3 center = renderer.transform.position;
        float offset = 1f;

        Vector3 direction = gameObject.transform.position - center;
        
        float distance = direction.magnitude;

        float radius = (renderer.bounds.max.x - renderer.bounds.min.x)/2;
        if (distance >= radius - offset){
            Vector2 boundedPos = center + direction.normalized * (radius - offset);
            transform.position = boundedPos;
        }
        
    }
}