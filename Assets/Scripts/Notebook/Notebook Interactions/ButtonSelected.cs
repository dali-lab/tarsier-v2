using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the button that will show an indicator for when it is selected (on press, a circle appears)
/// </summary>
namespace Anivision.NotebookSystem
{
    public class ButtonSelected : MonoBehaviour
    {
        public Button[] buttons;
        public Button defaultButton;

        private SpriteRenderer[] _textSelect;
        private SpriteRenderer[] _currButtonTextSelect;

        private void OnEnable()
        {

            SetIndicator(defaultButton);    // turn on all the corresponding text select indicators for the default selected button

            foreach (Button button in buttons)
            {
                button.onClick.AddListener(delegate { SetIndicator(button); });
            }
        }

        private void SetIndicator(Button currButton)
        {
            // turns off all text select indicators (circles)
            _textSelect = GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer effects in _textSelect)
            {
                if (effects.gameObject.tag == "text select")
                {
                    effects.gameObject.SetActive(false);
                }

            }
            // turns on all the text select indicators for the button that was pressed
            _currButtonTextSelect = currButton.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer effects in _currButtonTextSelect)
            {
                if (effects.gameObject.tag == "text select")
                {
                    effects.gameObject.SetActive(true);
                }
            }
        }
    }
}
