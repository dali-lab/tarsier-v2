using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the button that will flip to the vision select page.
/// </summary>
namespace Anivision.NotebookSystem
{
    public class ToVisionPage : MonoBehaviour
    {
        public Page visionPage;
        private Notebook _notebook;
        private Button _toVisionButton;

        private void OnEnable()
        {
            _notebook = Notebook.Instance;
            if (_notebook == null) UnityEngine.Debug.LogError("Trying to access the notebook in this scene, but there is none.");

            _toVisionButton = gameObject.GetComponent<Button>();
            if (_toVisionButton == null) throw new System.Exception("Must have a button script on this object");
            _toVisionButton.onClick.AddListener(FlipToVision);
        }
        private void FlipToVision()
        {
            _notebook.CurrentChapter.PresentPage(visionPage, false, false);   // display the vision select page of the home chapter
        }

        //private void Update()
        //{
        //    if (Input.GetKey("up")) FlipToVision();
        //}
    }
}
