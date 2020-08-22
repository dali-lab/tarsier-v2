using System;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Dashboard
{
    public class VisionSwitch : MonoBehaviour
    {
        public Animal animalToSwitch;
        private Button _button;
        private AnimalManager _animalManager;

        private void OnEnable()
        {
            _animalManager = AnimalManager.Instance;
            if (_animalManager == null) throw new Exception("Must have a animal manager in the scene");
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(SwitchToAnimal);
        }

        private void SwitchToAnimal()
        {
            _animalManager.SwitchAnimal(animalToSwitch);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SwitchToAnimal);
        }
    }
}

