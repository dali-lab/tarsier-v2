using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Anivision.NotebookSystem
{
    /// <summary>
    /// Second level of the notebook. Automatically saves a reference to all of the page scripts that are attached to children of the game object
    /// </summary>
    public class Chapter : MonoBehaviour
    {
        // enum of all chapter titles that distinguishes different types of chapters
        public enum ChapterTitle{ Tutorial, Home, VisionSwitch }
        
        [Tooltip("The chapter title of this chapter. Should be unique from titles of other chapters")]
        public ChapterTitle chapterTitle;
        [Tooltip("Default page to be presented at setup of this chapter")]
        public Page defaultPage;
        [Tooltip("Buttons that belong to this chapter and should not change from page to page within the chapter")]
        public List<Button> buttons;
        [Tooltip("Text Mesh Pros that belong to this chapter and should not change from page to page within the chapter")]
        public List<TextMeshPro> texts;

        private List<Page> _pages; // all pages that are children of this game object
        private Page _currentPage;
        private Dictionary<TextMeshPro, string> _originalTextInfo; //saves original text of text mesh pros
        
        //save text mesh pro information, get pages from all children
        private void Awake()
        {
            _originalTextInfo = new Dictionary<TextMeshPro, string>();
            foreach (TextMeshPro tmp in texts)
            {
                if (!_originalTextInfo.ContainsKey(tmp)) _originalTextInfo.Add(tmp, tmp.text);
            }

            GetPages();
        }
        
        /// <summary>
        /// Resets all pages, buttons, and text in this chapter and presents the default page. Turns all elements active
        /// </summary>
        public virtual void Setup()
        {
            GetPages();
            foreach (Page p in _pages)
            {
                p.Setup();
            }
            ResetCurrentPage();
            ResetButtons();
            ResetTexts();
            if (_currentPage != null) _currentPage.Show();
            SetButtonsActive(true);
            SetTextsActive(true);
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Shows the current page, buttons, and text without resetting anything, turns all elements active
        /// </summary>
        public virtual void Show()
        {
            GetPages();
            if (_currentPage == null)
            {
                ResetCurrentPage();
            }
            
            if (_currentPage != null) _currentPage.Show();
            SetButtonsActive(true);
            SetTextsActive(true);
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Cleans up all pages, resets page to default, resets buttons and texts, and turns all elements inactive
        /// </summary>
        public virtual void Cleanup()
        {
            GetPages();
            foreach (Page p in _pages)
            {
                p.Cleanup();
            }
            ResetCurrentPage();
            ResetButtons();
            ResetTexts();
            SetButtonsActive(false);
            SetTextsActive(false);
            if (gameObject.activeSelf) gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Hides current page, buttons, and text without resetting anything; Turns all elements inactive
        /// </summary>
        public virtual void Hide()
        {
            GetPages();
            if (_currentPage != null)
            {
                _currentPage.Hide();
            }
            
            SetButtonsActive(false);
            SetTextsActive(false);
            if (!gameObject.activeSelf) gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Presents the page that is passed in.
        /// Optional booleans to see if current and new pages should be reset before being hidden/presented.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="resetCurrentPage"></param>
        /// <param name="resetPageToBePresented"></param>
        public virtual void PresentPage(Page page, bool resetCurrentPage = false, bool resetNewPage = false)
        {
            if (_currentPage != null)
            {
                if (resetCurrentPage)
                {
                    _currentPage.Cleanup();
                }
                else
                {
                    _currentPage.Hide();
                }
            }

            _currentPage = page;

            if (resetNewPage)
            {
                _currentPage.Setup();
            }
            else
            {
                _currentPage.Show();
            }
            
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }

        protected virtual void GetPages()
        {
            if (_pages == null)
            {
                _pages = GetComponentsInChildren<Page>().ToList();
            }
        }
        
        // resets _currentPage variable to default
        private void ResetCurrentPage()
        {
            if (defaultPage != null && _pages.Contains(defaultPage))
            {
                _currentPage = defaultPage;
            }
            else if (_pages.Count > 0)
            {
                _currentPage = _pages[0];
            }
            else
            {
                _currentPage = null;
            }
        }
        
        // sets all buttons active/inactive
        protected void SetButtonsActive(bool setActive)
        {
            foreach (Button b in buttons)
            {
                b.gameObject.SetActive(setActive);
            }
        }
        
        // sets all texts active/inactive
        protected void SetTextsActive(bool setActive)
        {
            foreach (TextMeshPro tmp in texts)
            {
                tmp.enabled = setActive;
            }
        }
        
        // resets all chapter button text
        protected virtual void ResetButtons()
        {
            foreach (Button b in buttons)
            {
                b.ChangeText(b.buttonText);
            }
        }
        
        // resets all chapter text mesh pro text
        protected virtual void ResetTexts()
        {
            foreach (TextMeshPro tmp in texts)
            {
                tmp.text = _originalTextInfo[tmp];
            }
            
        }
    }
}