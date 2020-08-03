using UnityEngine;
using UnityEngine.SceneManagement;
using Anivision.Core;

namespace Anivision.SceneManagement
{
    public class SceneSwitch : MonoBehaviour
    {
        public string sceneName = "LobbyScene";
        public HeadsetFade headsetFade;
        public float fadeSpeed = 2;

        private void OnEnable()
        {
            headsetFade.OnFadeEnd += SwitchScene;
        }

        private void OnDisable()
        {
            headsetFade.OnFadeEnd -= SwitchScene;
        }

        public void StartTransition()
        {
            headsetFade.StartFade(fadeSpeed);
        }

        private void SwitchScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
