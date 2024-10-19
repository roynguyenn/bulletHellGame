using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    enum ItemTypes{Health}
    [SerializeField] private ItemTypes itemtype;
    public float lifeTime;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTime){

            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
    }
}
