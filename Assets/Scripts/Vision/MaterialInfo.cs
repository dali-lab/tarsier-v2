using System;
using UnityEngine;

namespace Anivision.Vision
{
    // class to save all of the relevant material information
    public class MaterialInfo
    {
        public Texture2D texture;
        public Texture2D changedTexture;
        public Color? color = null;
        public String shaderTextureProperty = null;
        public String shaderColorProperty = null;
        public Renderer renderer;
    }
}