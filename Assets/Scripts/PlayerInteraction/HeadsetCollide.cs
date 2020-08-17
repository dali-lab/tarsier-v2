using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Anivision.PlayerInteraction;

namespace Anivision.PlayerInteraction
{
    /// <summary>
    /// Script attached to the center eye to detect collisions with other objects.
    /// Invokes a UnityEvent with a parameter of the colliding gameobject's Collider.
    /// </summary>
    public class HeadsetCollide : MonoBehaviour
    {
        public class OnCollideEvent : UnityEvent<Collider>
        {
        }

        [HideInInspector] public OnCollideEvent onCollide = new OnCollideEvent();

        private void OnTriggerEnter(Collider other)
        {
            onCollide.Invoke(other);                                        // Invokes UnityEvent with the other gameobject's Collider as a parameter
        }

        private void OnDisable()
        {
            onCollide.RemoveAllListeners();
        }
    }
}
