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
    [System.Serializable]
    public class OnCollideEvent : UnityEvent<Collider>
    {
    }

    public class HeadsetCollide : MonoBehaviour
    {
        [HideInInspector] public OnCollideEvent onCollide;

        private void Start()
        {
            if (onCollide == null) onCollide = new OnCollideEvent();        // instantiate event
        }

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
