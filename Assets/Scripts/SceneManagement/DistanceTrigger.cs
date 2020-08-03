using UnityEngine;

namespace Anivision.SceneManagement
{
    public class DistanceTrigger : SceneSwitch
    {
        public Transform headsetTransform;
        public float requiredDistance = 1;

        void Update()
        {
            if (Vector3.Distance(gameObject.transform.position, headsetTransform.position) <= requiredDistance)
            {
                StartTransition();
            }
        }
    }
}
