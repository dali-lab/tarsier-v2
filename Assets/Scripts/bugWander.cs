using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugWander : MonoBehaviour {

    public float moveSpeed = 8f;
    public float rotSpeed = 100f;
    public GameObject floor;
    public GameObject boundingBox;

    private bool executeMovement = true;
    private Collider floorCollider;
    private Vector3 maxPoint;
    private Vector3 minPoint;
    private float timer = 0.0f;
    private float randomWaitTime = 0.0f;

    // Use this for initialization
    void Start () {
        floorCollider = floor.GetComponent<Collider>();
        maxPoint = boundingBox.GetComponent<Collider>().bounds.max;
        minPoint = boundingBox.GetComponent<Collider>().bounds.min;
    }
    
    // Update is called once per frame
    void Update () {

        if (executeMovement)
        {
            int animationNumber = Random.Range(0, 100);

            if (animationNumber >= 0 && animationNumber <= 50)                   // move forward
            {
                Vector3 newPosition = transform.position - transform.forward * moveSpeed * Time.deltaTime;

                // if within bounds of the x and z bounding box, move forward, otherwise turn and stay within bounds
                if (newPosition.x > minPoint.x && newPosition.x < maxPoint.x && newPosition.z > minPoint.z && newPosition.z < maxPoint.z)                   
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
