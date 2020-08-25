using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anivision.Notebook
{
    /// <summary>
    /// First level of the notebook. Controls notebook that is attached to left controller
    /// Automatically saves a reference to all of the chapter scripts that are attached to children of the game object
    /// Every chapter script that is a child should have a unique chapter title
    /// </summary>
    public class Notebook : MonoBehaviour
    {
        private static Notebook _notebook;
        //singleton instance
        public static Notebook Instance { get
        {
            if (!_notebook)
            {
                _notebook = FindObjectOfType (typeof (Notebook)) as Notebook;

                if (!_notebook)
                {
                    UnityEngine.Debug.LogError("There needs to be one active NotebookController script on a GameObject in your scene.");
                }
            }

            return _notebook;
        } }

        [Tooltip("Default chapter title. Should be a title that belongs to a chapter that is a child of this game object.")]
        public Chapter.ChapterTitle defaultChapterTitle; 
        
        private Chapter[] chapters;
        private Chapter _currentChapter;
        private Dictionary<Chapter.ChapterTitle, Chapter> _chapterDictionary;
        private void Awake()
        {
            chapters = GetComponentsInChildren<Chapter>();
            _chapterDictionary = new Dictionary<Chapter.ChapterTitle, Chapter>();
            foreach (Chapter c in chapters)
            {
                if (!_chapterDictionary.ContainsKey(c.chapterTitle))
                {
                    _chapterDictionary.Add(c.chapterTitle, c);
                }
            }

            ResetCurrentChapter();
        }
        
        //on enable, shows the current chapter without resetting
        private void OnEnable()
        {
            if (_currentChapter != null) ShowChapter(_currentChapter.chapterTitle);
        }
        
        // on disable, hides the current chapter without resetting
        private void OnDisable()
        {
            if (_currentChapter != null)
            {
                _currentChapter.Hide();
            }
        }
        
        /// <summary>
        /// Setup resets the notebook completely and presents the default chapter resetted
        /// </summary>
        public void Setup()
        {
            ResetCurrentChapter();
            if (_currentChapter != null) _currentChapter.Setup();
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Cleanup resets the notebook and disables game object
        /// </summary>
        public void Cleanup()
        {
            foreach (Chapter c in chapters)
            {
                c.Cleanup();
            }
            ResetCurrentChapter();
            if (gameObject.activeSelf) gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Shows the chapter that corresponds to the title passed in. Optional booleans to determine if the current and new chapters should be reset
        /// </summary>
        /// <param name="chapterTitle"></param>
        /// <param name="resetCurrentChapter"></param>
        /// <param name="resetNewChapter"></param>
        public void ShowChapter(Chapter.ChapterTitle chapterTitle, bool resetCurrentChapter = false, bool resetNewChapter = false)
        {
            Chapter chapter;
            if (_chapterDictionary.TryGetValue(chapterTitle, out chapter))
            {
                if (_currentChapter != null)
                {
                    if (resetCurrentChapter)
                    {
                        _currentChapter.Cleanup();
                    }
                    else
                    {
                        _currentChapter.Hide();
                    }
                }

                _currentChapter = chapter;

                if (resetNewChapter)
                {
                    _currentChapter.Setup();
                }
                else
                {
                    _currentChapter.Show();
                }
                
            }
            else
            {
                UnityEngine.Debug.LogError("Chapter type does not exist in this notebook");
            }
            
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
        
        // resets the _currentChapter variable
        private void ResetCurrentChapter()
        {
            if (_chapterDictionary.ContainsKey(defaultChapterTitle))
            {
                _currentChapter = _chapterDictionary[defaultChapterTitle];
            }
            else if (chapters.Length > 0)
            {
                _currentChapter = chapters[0];
            }
            else
            {
                _currentChapter = null;
            }
        }
        
    }
}