using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    public Renderer map;
    public Animator animator;
    public CircleCollider2D mapCollider;
    // Start is called before the first frame update
    public float movespeed = 4f;
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
            animator.SetBool("isWalking", true);
        }  
        if (Input.GetKey(KeyCode.S)) {
            movementVector += new Vector3(0,-1,0); 
            animator.SetBool("isWalking", true);
        } 
        if (Input.GetKey(KeyCode.D)) {
            movementVector += new Vector3(1,0,0); 
            animator.SetBool("isWalking", true);
        } 
        if (Input.GetKey(KeyCode.A)) {
            movementVector += new Vector3(-1,0,0); 
            animator.SetBool("isWalking", true);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)){
            animator.SetBool("isWalking", false);
        }
        movementVector.Normalize();
        transform.position += movementVector * movespeed * Time.deltaTime;

        boundPosition(mapCollider);
    }

    public void boundPosition(CircleCollider2D collider){
        Vector3 center = (Vector2) collider.transform.position +(collider.offset * collider.transform.localScale.x);
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
