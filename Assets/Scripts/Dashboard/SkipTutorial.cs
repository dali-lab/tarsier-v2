using Anivision.PlayerInteraction;
using Anivision.Tutorial;
using UnityEngine;

namespace Anivision.Dashboard
{
    public class SkipTutorial : MonoBehaviour
    {
        public TutorialController _TutorialController;
        public ScaleController scaleController;

        private Button _button;
        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartScaleOrSkip);
        }

        private void StartScaleOrSkip()
        {
            if (scaleController.GetSize() != scaleController.scale)
            {
                scaleController.ScaleDone.AddListener(DoneScaling);
                scaleController.StartScaleChange();
            }
            else
            {
                _TutorialController.Skip();
            }
            
        }

        private void DoneScaling()
        {
            _TutorialController.Skip();
            scaleController.ScaleDone.RemoveListener(DoneScaling);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(StartScaleOrSkip);
            scaleController.ScaleDone.RemoveListener(DoneScaling);
        }
    }
}

