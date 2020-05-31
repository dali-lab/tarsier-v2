using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFlowers : MonoBehaviour
{
    [Tooltip("List containing the prefabs for all the flower variants")]
    public List<GameObject> flowers;
    [Tooltip("The amount of flowers to generate")]
    public int amount;
    [Tooltip("Half the width and length of the area to generate flowers in (centered on this game object's position")]
    public float range;

    private GameObject container; // Container game object that is the parent to all the generated flowers

    // Start is called before the first frame update
    void Start()
    {
        // Create the container
        container = new GameObject();
        container.name = "Container";

        // Iterate until the desired amount of flowers have been placed
        for (int i = 0; i < amount; i++)
        {
            // Instantiate a new flower, randomly choosing one of the flower variants
            GameObject flower = Instantiate(flowers[Random.Range(0, flowers.Count)]);
            flower.transform.parent = container.transform; // Set the container to be the new flower's parent

            // Randomly scale and rotate the flower
            flower.transform.localScale = Vector3.one * Random.Range(.5f, 2f);
            flower.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            // Raycast downward from the flower. If it doesn't hit the ground, move it to a new position and try again
            RaycastHit hit;
            while (!Physics.Raycast(flower.transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                // Try a different random position
                flower.transform.position = new Vector3(Random.Range(-range, range), gameObject.transform.position.y, Random.Range(-range, range));
            }

            // Once the raycast has hit the ground, set the flower's position to match the point where the raycast hit the ground
            flower.transform.position = hit.point;
        }
    }
}
