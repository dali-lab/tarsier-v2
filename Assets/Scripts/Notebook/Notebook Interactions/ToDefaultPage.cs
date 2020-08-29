using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the button that will flip to the default page of this chapter
/// </summary>
namespace Anivision.NotebookSystem
{
    public class ToDefaultPage : MonoBehaviour
    {
        private Notebook _notebook;
        private Button _toDefaultButton;

        private void OnEnable()
        {
            _notebook = Notebook.Instance;
            if (_notebook == null) UnityEngine.Debug.LogError("Trying to access the notebook in this scene, but there is none.");

            _toDefaultButton = gameObject.GetComponent<Button>();
            if (_toDefaultButton == null) throw new System.Exception("Must have a button script on this object");

            _toDefaultButton.onClick.AddListener(FlipToDefault);
        }
        private void FlipToDefault()
        {
            _notebook.CurrentChapter.PresentPage(_notebook.CurrentChapter.defaultPage);   // display the default page of the chapter
        }
    }
}
