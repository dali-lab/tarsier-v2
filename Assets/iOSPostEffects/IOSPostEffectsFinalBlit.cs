using UnityEngine;
using System.Collections;

public class IOSPostEffectsFinalBlit : MonoBehaviour {
	private IOSPostEffects iOSPostEffects;
	
	void Start () 
	{
		//_camera = gameObject.GetComponent<Camera>();
		Camera mainCamera = Camera.main;
		if (mainCamera)
		{
			iOSPostEffects = mainCamera.GetComponent<IOSPostEffects>();
		}
		else
		{
			iOSPostEffects = GameObject.Find("Main Camera").GetComponent<IOSPostEffects>();
		}
	}
	
	void OnPreRender ()
	{
		if (iOSPostEffects.UseIOSPostEffects)
		{
    		Graphics.Blit(iOSPostEffects.cameraRenderTexture, null, iOSPostEffects.cameraBlitMat, 3);
		}
    }
	
}
