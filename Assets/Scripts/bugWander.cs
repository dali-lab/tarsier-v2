using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugWander : MonoBehaviour {

    public float moveSpeed = 8f;
    public float rotSpeed = 100f;
    public GameObject floor;


    private bool executeMovement = true;
    private Collider floorCollider;
    private float timer = 0.0f;
    private float randomWaitTime = 0.0f;

    // Use this for initialization
    void Start () {
        floorCollider = floor.GetComponent<Collider>();
    }
    
    // Update is called once per frame
    void Update () {

        if (executeMovement)
        {
            int animationNumber = Random.Range(0, 100);

            if (animationNumber >= 0 && animationNumber <= 50)                   // move forward
            {
                Vector3 newPosition = transform.position - transform.forward * moveSpeed * Time.deltaTime;
                Vector3 newVector = new Vector3(newPosition.x, floor.transform.position.y, newPosition.z);

                if (floorCollider.bounds.Contains(newVector))                   // if within bounds of ground, move forward
                {         
                    transform.position -= transform.forward * moveSpeed * Time.deltaTime;

                }
                else
                {
                    Vector3 halfRotation = new Vector3(0f, 180f, 0f);
                    transform.Rotate(halfRotation);
                }
            }
            else if (animationNumber > 50 && animationNumber <= 70)             // turn
            {
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            else if (animationNumber > 70 && animationNumber <= 98)             // turn
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }
            else if (animationNumber >= 98)                                     // pause
            {
                executeMovement = false;
                randomWaitTime = Random.Range(1.0f, 3.0f);
            }
        } else
        {
            if (timer >= randomWaitTime)
            {
                executeMovement = true;
                timer = 0.0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
            
               
     
        }
   
}
