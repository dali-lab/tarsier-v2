using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anivision.Vision
{
    public class UV : MonoBehaviour
    {
        [Range(0, 255)] public int uvAmount;
        [Tooltip("The property name in the shader that refers to the main color that should be changed")]
        public string materialBaseColorName = "_BaseColor";

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

