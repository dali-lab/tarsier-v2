using UnityEngine;

namespace Anivision.SceneManagement
{
    // A SceneSwitch script that triggers when this script's game object gets withen the distance threshold to the given headset transform
    public class DistanceTrigger : SceneSwitch
    {
        [Tooltip("The transform to compare to this game object's transform.")]
        public Transform compareTransform;
        [Tooltip("How close the compare transform must get to this game object before the scene transition is triggered")]
        public float distanceThreshold = 1;

        void Update()
        {
            // Compare the distance between this gameobject and the compare transform.
            // If it is less than the threshold, start the scene transition
            if (Vector3.Distance(gameObject.transform.position, compareTransform.position) <= distanceThreshold)
            {
                StartTransition();
            }
        }
    }
}
