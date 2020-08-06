using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    [RequireComponent(typeof(SkyboxController))]
    // Base class for all skybox effects
    public abstract class SkyboxEffect : MonoBehaviour
    {
        public abstract VisionEffect Effect { get; }
        public abstract void ApplyEffect(VisionParameters parameters);
    }
}