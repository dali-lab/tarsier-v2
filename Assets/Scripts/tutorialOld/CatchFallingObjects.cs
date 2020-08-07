using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFallingObjects : MonoBehaviour
{
    private Vector3 originalPosition;
    private int originalY;
    private int originalZ;

    public float minY;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= minY)
        {
            GameObject g = Instantiate(gameObject, originalPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
