using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Anivision.NotebookSystem
{
    /// <summary>
    /// 3rd level of the notebook. Controls individual UI elements
    /// Automatically saves a reference to all of the Button, Text Mesh Pro, and Image components that are attached to children of the game object
    /// </summary>
    public class Page : MonoBehaviour
    {
        private Button[] _buttons;
        private TextMeshPro[] _textMeshPros;
        private SpriteRenderer[] _images;
        private Dictionary<TextMeshPro, string> _originalTextInfo; //saves original text of text mesh pros
        private Dictionary<SpriteRenderer, Sprite> _originalSprites; // saves original sprites of images
        
        private void Awake()
        {
            BuildElementsDictionary();
        }
        
        /// <summary>
        /// Resets the entire page and its elements, sets all elements to active
        /// </summary>
        public virtual void Setup()
        {
            BuildElementsDictionary();
            ResetButtons();
            ResetText();
            ResetImages();
            SetButtonsActive(true);
            SetTextActive(true);
            SetImagesActive(true);
            if (!gameObject.activeSelf) gameObject.SetActive(true);

        }
        
        /// <summary>
        /// Sets all elements to active without resetting
        /// </summary>
        public virtual void Show()
        {
            BuildElementsDictionary();
            SetButtonsActive(true);
            SetTextActive(true);
            SetImagesActive(true);
            if (!gameObject.activeSelf) gameObject.SetActive(true);

        }

        /// <summary>
        /// Resets the entire page and its elements, sets all elements to inactive
        /// </summary>
        public virtual void Cleanup()
        {
            BuildElementsDictionary();
            ResetButtons();
            ResetText();
            ResetImages();
            SetButtonsActive(false);
            SetTextActive(false);
            SetImagesActive(false);
            
            if (!gameObject.activeSelf) gameObject.SetActive(false);
        }
        
        public virtual void Hide()
        {
            BuildElementsDictionary();
            SetButtonsActive(false);
            SetTextActive(false);
            SetImagesActive(false);
            if (!gameObject.activeSelf) gameObject.SetActive(false);
        }

        public virtual void ChangeText(TextMeshPro tmp, String s)
        {
            TextMeshPro result = Array.Find(_textMeshPros, ele => tmp.Equals(ele));
            result.text = s;
        }
        
        public virtual void ChangeButton(Button button, String s)
        {
            Button result = Array.Find(_buttons, ele => button.Equals(ele));
            result.ChangeText(s);
        }
        
        public virtual void ChangeImage(SpriteRenderer image, Sprite sprite)
        {
            SpriteRenderer result = Array.Find(_images, ele => image.Equals(ele));
            result.sprite = sprite;
        }
        
        protected virtual void BuildElementsDictionary()
        {
            if (_buttons == null)
            {
                _buttons = GetComponentsInChildren<Button>();
            }

            if (_textMeshPros == null)
            {
                _textMeshPros = GetComponentsInChildren<TextMeshPro>();
                _originalTextInfo = new Dictionary<TextMeshPro, string>();
                foreach (TextMeshPro tmp in _textMeshPros)
                {
                    if (!_originalTextInfo.ContainsKey(tmp))
                    {
                        _originalTextInfo.Add(tmp, tmp.text);
                    }
                }
            }

            if (_images == null)
            {
                List<SpriteRenderer> allImages = GetComponentsInChildren<SpriteRenderer>().ToList();
                //UnityEngine.Debug.Log("count: " + allImages.Count);
                for (int i = allImages.Count-1; i >= 0; i--)
                {
                    //UnityEngine.Debug.Log(allImages[i].gameObject);
                    if (allImages[i].gameObject.tag == "text effect")
                    {
                        //UnityEngine.Debug.Log("removed: " + allImages[i].gameObject);
                        allImages[i].gameObject.SetActive(false);
                        allImages.Remove(allImages[i]);
                    }
                }
                _images = allImages.ToArray();

                _originalSprites = new Dictionary<SpriteRenderer, Sprite>();
                foreach (SpriteRenderer image in _images)
                {
                    if (!_originalSprites.ContainsKey(image))
                    {
                        _originalSprites.Add(image, image.sprite);
                    }
                }
            }
        }

        protected virtual void ResetButtons()
        {
            foreach (Button b in _buttons)
            {
                b.ChangeText(b.buttonText);
            }
        }
        
        protected virtual void ResetText()
        {
            foreach (TextMeshPro tmp in _textMeshPros)
            {
                tmp.text = _originalTextInfo[tmp];
            }
        }
        
        protected virtual void ResetImages()
        {
            foreach (SpriteRenderer image in _images)
            {
                image.sprite = _originalSprites[image];
            }
        }

        private void SetTextActive(bool setActive)
        {
            foreach (TextMeshPro tmp in _textMeshPros)
            {
                tmp.enabled = setActive;
            }
        }

        private void SetButtonsActive(bool setActive)
        {
            foreach (Button b in _buttons)
            {
                b.gameObject.SetActive(setActive);
            }
        }

        private void SetImagesActive(bool setActive)
        {
            foreach (SpriteRenderer image in _images)
            {
                image.gameObject.SetActive(setActive);
            }
        }
    }
}