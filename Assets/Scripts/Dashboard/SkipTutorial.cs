using Anivision.NotebookSystem;
using Anivision.PlayerInteraction;
using Anivision.Tutorial;
using UnityEngine;

namespace Anivision.Dashboard
{
    /// <summary>
    /// Skips tutorial and scales the camera if it hasn't already been scaled
    /// </summary>
    public class SkipTutorial : MonoBehaviour
    {
        public TutorialController _TutorialController;
        public ScaleController scaleController; //scale camera script

        private Button _button;
        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartScaleOrSkip); //add listener to button
        }

        private void StartScaleOrSkip()
        {
            if (scaleController != null && scaleController.GetSize() != scaleController.scale)
            {
                scaleController.ScaleDone.AddListener(DoneScaling); // add function to be called after scaling is done
                scaleController.StartScaleChange(); // start scaling camera
                
            } else
            {
                _TutorialController.Skip();
            }
            
        }

        private void DoneScaling()
        {
            _TutorialController.Skip();
            if (scaleController != null) scaleController.ScaleDone.RemoveListener(DoneScaling);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(StartScaleOrSkip);
            if (scaleController != null) scaleController.ScaleDone.RemoveListener(DoneScaling);
        }
    }
}

