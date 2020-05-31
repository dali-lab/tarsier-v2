using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFlowers : MonoBehaviour
{
    public List<GameObject> flowers;
    public int amount;
    public float range;

    private GameObject container;

    // Start is called before the first frame update
    void Start()
    {
        container = new GameObject();
        container.name = "Container";

        
        for (int i = 0; i < amount; i++)
        {
            GameObject flower = Instantiate(flowers[Random.Range(0, flowers.Count)]);
            flower.transform.parent = container.transform;
            flower.transform.position = new Vector3(Random.Range(-range, range), gameObject.transform.position.y, Random.Range(-range, range));

            RaycastHit hit;
            if (Physics.Raycast(flower.transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                flower.transform.position = hit.point;
            }
            else
            {
                Destroy(flower);
                i--;
            }
        }
    }
}
