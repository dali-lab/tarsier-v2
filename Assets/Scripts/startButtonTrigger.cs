using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButtonTrigger : MonoBehaviour
{
    // an array allows the same trigger to cause multiple actions
    // "SerializeField" forces Unity to expose the private variable in editor
    [SerializeField] private GameObject[] targets;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
            Debug.Log(target.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }

}