using UnityEngine;

namespace Anivision.Vision
{
    // class to save all of the relevant material information
    public class MaterialInfo
    {
        public Texture2D texture;
        public Texture2D changedTexture;
        public Color color;
        public string shaderTextureProperty;
        public string shaderColorProperty;
        public Renderer renderer;
    }
}