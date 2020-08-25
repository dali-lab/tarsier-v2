using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Anivision.Notebook
{
    /// <summary>
    /// 3rd level of the notebook. Controls individual UI elements
    /// Automatically saves a reference to all of the Button, Text Mesh Pro, and Image components that are attached to children of the game object
    /// </summary>
    public class Page : MonoBehaviour
    {
        private Button[] _buttons;
        private TextMeshPro[] _textMeshPros;
        private Image[] _images;
        private Dictionary<TextMeshPro, string> _originalTextInfo; //saves original text of text mesh pros
        private Dictionary<Image, Sprite> _originalSprites; // saves original sprites of images
        
        private void Awake()
        {
            _buttons = GetComponentsInChildren<Button>();
            _textMeshPros = GetComponentsInChildren<TextMeshPro>();
            _images = GetComponentsInChildren<Image>();
            _originalTextInfo = new Dictionary<TextMeshPro, string>();
            _originalSprites = new Dictionary<Image, Sprite>();
            
            foreach (TextMeshPro tmp in _textMeshPros)
            {
                if (!_originalTextInfo.ContainsKey(tmp))
                {
                    _originalTextInfo.Add(tmp, tmp.text);
                }
            }
            
            foreach (Image image in _images)
            {
                if (!_originalSprites.ContainsKey(image))
                {
                    _originalSprites.Add(image, image.sprite);
                }
            }
        }
        
        /// <summary>
        /// Resets the entire page and its elements, sets all elements to active
        /// </summary>
        public virtual void Setup()
        {
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
            SetButtonsActive(true);
            SetTextActive(true);
            SetImagesActive(true);
        }

        /// <summary>
        /// Resets the entire page and its elements, sets all elements to inactive
        /// </summary>
        public virtual void Cleanup()
        {
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
            SetButtonsActive(false);
            SetTextActive(false);
            SetImagesActive(false);
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
        
        public virtual void ChangeImage(Image image, Sprite sprite)
        {
            Image result = Array.Find(_images, ele => image.Equals(ele));
            result.sprite = sprite;
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
            foreach (Image image in _images)
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
            foreach (Image image in _images)
            {
                image.gameObject.SetActive(setActive);
            }
        }
    }
}