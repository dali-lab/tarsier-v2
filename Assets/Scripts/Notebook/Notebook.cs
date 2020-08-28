using System;
using System.Collections.Generic;
using UnityEngine;
namespace Anivision.NotebookSystem
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
        public static Notebook Instance
        {
            get
            {
                if (!_notebook)
                {
                    _notebook = FindObjectOfType(typeof(Notebook)) as Notebook;
                    if (!_notebook)
                    {
                        UnityEngine.Debug.LogError("There needs to be one active NotebookController script on a GameObject in your scene.");
                    }
                }
                return _notebook;
            }
        }
        [Tooltip("Default chapter title. Should be a title that belongs to a chapter that is a child of this game object.")]
        public Chapter.ChapterTitle defaultChapterTitle;
        public Chapter CurrentChapter { get; private set; }
        private Chapter[] chapters;
        private Dictionary<Chapter.ChapterTitle, Chapter> _chapterDictionary = new Dictionary<Chapter.ChapterTitle, Chapter>();

        //on enable, shows the current chapter without resetting
        private void OnEnable()
        {
            if (CurrentChapter != null) ShowChapter(CurrentChapter.chapterTitle);
        }
        // on disable, hides the current chapter without resetting
        private void OnDisable()
        {
            if (CurrentChapter != null)
            {
                CurrentChapter.Hide();
            }
        }
        /// <summary>
        /// Setup resets the notebook completely and presents the default chapter resetted
        /// </summary>
        public void Setup()
        {
            BuildChaptersDictionary();
            foreach (Chapter c in chapters)
            {
                c.Cleanup();
            }
            ResetCurrentChapter();
            if (CurrentChapter != null) CurrentChapter.Setup();
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
        /// <summary>
        /// Cleanup resets the notebook and disables game object
        /// </summary>
        public void Cleanup()
        {
            BuildChaptersDictionary();
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
            BuildChaptersDictionary();
            Chapter chapter;
            if (_chapterDictionary.TryGetValue(chapterTitle, out chapter))
            {
                if (CurrentChapter != null)
                {
                    if (resetCurrentChapter)
                    {
                        CurrentChapter.Cleanup();
                    }
                    else
                    {
                        CurrentChapter.Hide();
                    }
                }
                CurrentChapter = chapter;
                if (resetNewChapter)
                {
                    CurrentChapter.Setup();
                }
                else
                {
                    CurrentChapter.Show();
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
                CurrentChapter = _chapterDictionary[defaultChapterTitle];
            }
            else if (chapters.Length > 0)
            {
                CurrentChapter = chapters[0];
            }
            else
            {
                CurrentChapter = null;
            }

        }
        private void BuildChaptersDictionary()
        {
            if (chapters == null)
            {
                chapters = GetComponentsInChildren<Chapter>();
                foreach (Chapter c in chapters)
                {
                    if (!_chapterDictionary.ContainsKey(c.chapterTitle))
                    {
                        _chapterDictionary.Add(c.chapterTitle, c);
                    }
                }
            }
        }
    }
}