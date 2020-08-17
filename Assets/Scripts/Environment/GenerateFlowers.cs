using System.Collections.Generic;
using UnityEngine;

namespace Anivision.Environment
{
    public class GenerateFlowers : MonoBehaviour
    {
        [Header("General Settings")]
        [Tooltip("List containing the prefabs for all the flower variants")]
        public List<GameObject> flowers;
        [Tooltip("Half the width and length of the area to generate flowers in (centered on this game object's position")]
        public float range;
        [Header("Cluster Settings")]
        [Tooltip("How many clusters to generate")]
        public int clusters;
        [Tooltip("The minimum amount of flowers to generate in a cluster")]
        public int minAmount;
        [Tooltip("The maximum amount of flowers to generate in a cluster")]
        public int maxAmount;
        [Tooltip("The range for each individual cluster")]
        public float clusterRange;
        [Header("Flower Settings")]
        [Tooltip("Minimum scale for the flower")]
        public float minScale;
        [Tooltip("Maximum scale for the flower")]
        public float maxScale;

        private GameObject container; // Container game object that is the parent to all the generated flowers

        // Start is called before the first frame update
        void Start()
        {
            // Create the container
            container = new GameObject();
            container.name = "Container";

            // Iterate until the desired number of clusters have been generated
            for (int i = 0; i < clusters; i++)
            {
                // Create the new cluster
                GameObject cluster = new GameObject();
                cluster.name = "Cluster" + i.ToString();
                cluster.transform.parent = container.transform; // Make the container the parent of the cluser

                // Get a random position on the terrain to center the cluster on
                cluster.transform.position = GetValidPosition(gameObject.transform.position, range);

                // Choose a type of flower for the cluster
                GameObject flowerType = flowers[Random.Range(0, flowers.Count)];

                // Iterate until a number between the minimum and maximum number of flowers have been placed in the cluster
                for (int j = 0; j < Random.Range(minAmount, maxAmount); j++)
                { 
                    // Instantiate a new flower
                    GameObject flower = Instantiate(flowerType);
                    flower.transform.parent = cluster.transform; // Set the cluster to be the new flower's parent

                    // Randomly scale and rotate the flower
                    flower.transform.localScale = flower.transform.localScale * Random.Range(minScale, maxScale);
                    flower.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                    // Put the flower in a random position within the cluster's range
                    flower.transform.position = GetValidPosition(cluster.transform.position, clusterRange);
                }         
            }
        }

        // Get a random valid position
        private Vector3 GetValidPosition(Vector3 position, float range)
        {
            // Choose an initial random position
            Vector3 randomPosition = GetRandomPosition(position, range);

            // Raycast downward. If it doesn't hit the ground, move it to a new position and try again
            RaycastHit hit;
            while (!Physics.Raycast(randomPosition, Vector3.down, out hit, Mathf.Infinity))
            {
                // Try a different random position
                randomPosition = GetRandomPosition(position, range);
            }
        
            // Set the random position to the raycast's hit point
            randomPosition = hit.point;

            return randomPosition; // Return the position
        }

        // Get a random position
        private Vector3 GetRandomPosition(Vector3 position, float range)
        {
            return new Vector3(position.x + Random.Range(-range, range), 100f, position.z + Random.Range(-range, range));
        }
    }
}