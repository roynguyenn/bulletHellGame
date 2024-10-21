using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    public Renderer map;
    public CircleCollider2D mapCollider;
    // Start is called before the first frame update
    public float movespeed = 3f;
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
        Debug.Log(movespeed);
        boundPosition(mapCollider);
    }

    public void boundPosition(CircleCollider2D collider){
        Vector3 center = (Vector2) collider.transform.position +(collider.offset * collider.transform.localScale.x);
        Debug.Log(center);
        float offset = 0f;
        Vector3 direction = gameObject.transform.position - center;
        float distance = direction.magnitude;

        float radius = collider.radius * collider.transform.localScale.x;
        if (distance >= radius + offset){
            Vector2 boundedPos = center + direction.normalized * (radius + offset);
            transform.position = boundedPos;
        }
    }
}
