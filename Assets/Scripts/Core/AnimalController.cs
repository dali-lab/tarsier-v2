using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.PlayerInteraction;
using Anivision.Vision;
using UnityEngine;

namespace Anivision.Core
{
    /// <summary>
    /// This script should be added to all animal prefabs. It saves the vision and movement information for the animal
    /// so that the Animal Manager can retrieve it and send it as a parameter in its events
    /// </summary>
    [RequireComponent(typeof(AnimalVision))]
    public class AnimalController : MonoBehaviour
    {
        public Animal animal;
        public VisionParameters VisionParameters { get; private set; }

        // Start is called before the first frame update
        private void Awake()
        {
            VisionParameters = GetComponent<AnimalVision>().ConstructVisionParametersObject(animal);
        }

    }
}

