using UnityEngine;
using System.Collections;

public class IOSPostEffects : MonoBehaviour
{
	[HideInInspector]
	public RenderTexture cameraRenderTexture; // Temp framebuffer rendertexture
	[HideInInspector]
	public RenderTexture customGrabRenderTextureA; // Used by fx shaders
	[HideInInspector]
	public RenderTexture customGrabRenderTextureB; // Used by fx shaders
	[HideInInspector]
	public RenderTexture customGrabRenderTextureC; // Used by fx shaders
	[HideInInspector]
	public RenderTexture customGrabRenderTextureD;

	public Material cameraBlitMat; // material/shader for blitting cameratexture to custom grab texture
	public Transform focusTransform;
	public float DOFApature = 1;
	public float bloomStrength = 0.45f;
	public float bloomCutoff = 0.4f;
	
	private Camera _cam;
	private Transform _transform;
	
	public bool useCustomCamerabufferSize = true; 
	public int cameraBufferWidth = 1024;
	public int cameraBufferHeight = 512;
	

	
	public int CaptureBufferWidth = 512;
	public int CaptureBufferHeight = 256;
	
	public bool SquareLowerDownSampleBuffers = true;
	
	public bool cheapPostEffects = false;
	public bool UseIOSPostEffects = true;
	
	

	// Use this for initialization
	public void Awake()
	{
		_cam = GetComponent<Camera>();
		
		if (UseIOSPostEffects)
		{
			//HiResBuffers = (Screen.height > hiresResolution || Screen.width > hiresResolution) ? true : false;
			
			if (!useCustomCamerabufferSize)
			{
				cameraBufferWidth = Screen.width;
				cameraBufferHeight = Screen.height;
			}
			
			int ajustedforSquareCaptureBufferWidth = 0;
			int ajustedforSquareCaptureBufferHeight = 0;
					
			if ( SquareLowerDownSampleBuffers)
			{
				ajustedforSquareCaptureBufferWidth = Mathf.Max (CaptureBufferWidth,CaptureBufferWidth);
				ajustedforSquareCaptureBufferHeight = ajustedforSquareCaptureBufferWidth;
			}
			else
			{
				ajustedforSquareCaptureBufferWidth = CaptureBufferWidth;
				ajustedforSquareCaptureBufferHeight = CaptureBufferHeight;
			}
			
			
	
			// Setup Camera with its own render target
			cameraRenderTexture = new RenderTexture(cameraBufferWidth, cameraBufferHeight, 16, RenderTextureFormat.ARGB32);
			cameraRenderTexture.wrapMode = TextureWrapMode.Clamp;
			cameraRenderTexture.useMipMap = false;
			if(useCustomCamerabufferSize)
			{
				cameraRenderTexture.isPowerOfTwo = true;
			}
			else
			{
				cameraRenderTexture.isPowerOfTwo = false;
			}
			
			cameraRenderTexture.filterMode = FilterMode.Bilinear;
			cameraRenderTexture.Create();
			
			// Create custom grab render texture D

			customGrabRenderTextureD = new RenderTexture(CaptureBufferWidth / 2, CaptureBufferHeight / 2, 0, RenderTextureFormat.ARGB32);

			customGrabRenderTextureD.wrapMode = TextureWrapMode.Clamp;
			customGrabRenderTextureD.useMipMap = false;	
			cameraRenderTexture.isPowerOfTwo = true;
			customGrabRenderTextureD.filterMode = FilterMode.Bilinear;
						
			customGrabRenderTextureD.Create();
			if (cheapPostEffects) Shader.SetGlobalTexture("_CaptureTex", customGrabRenderTextureD);
			
			cameraBlitMat.SetTexture("_BlurTexD", customGrabRenderTextureD);
			
		
			if (!cheapPostEffects)
			{
				
				if (!useCustomCamerabufferSize)
				{
					// Create custom grab render texture A
	
					customGrabRenderTextureA = new RenderTexture(CaptureBufferWidth, CaptureBufferHeight, 0, RenderTextureFormat.ARGB32);
	
					customGrabRenderTextureA.wrapMode = TextureWrapMode.Clamp;
					customGrabRenderTextureA.useMipMap = false;	
					cameraRenderTexture.isPowerOfTwo = true;
					customGrabRenderTextureA.filterMode = FilterMode.Bilinear;
							
					customGrabRenderTextureA.Create();
					Shader.SetGlobalTexture("_CaptureTex", customGrabRenderTextureA);
				}
				else
				{
					Shader.SetGlobalTexture("_CaptureTex", cameraRenderTexture);
				}
				
				// Create custom grab render texture B

				customGrabRenderTextureB = new RenderTexture(ajustedforSquareCaptureBufferWidth / 2, ajustedforSquareCaptureBufferHeight / 2, 0, RenderTextureFormat.ARGB32);


				customGrabRenderTextureB.wrapMode = TextureWrapMode.Clamp;
				customGrabRenderTextureB.useMipMap = false;	
				cameraRenderTexture.isPowerOfTwo = true;
				customGrabRenderTextureB.filterMode = FilterMode.Bilinear;
							
				customGrabRenderTextureB.Create();
				
				cameraBlitMat.SetTexture("_BlurTexA", customGrabRenderTextureB);				
				
				// Create custom grab render texture C

				customGrabRenderTextureC = new RenderTexture(ajustedforSquareCaptureBufferWidth / 4 , ajustedforSquareCaptureBufferHeight / 4, 0, RenderTextureFormat.ARGB32);

				customGrabRenderTextureC.wrapMode = TextureWrapMode.Clamp;
				customGrabRenderTextureC.useMipMap = false;	
				cameraRenderTexture.isPowerOfTwo = true;
				customGrabRenderTextureC.filterMode = FilterMode.Bilinear;
						
				customGrabRenderTextureC.Create();
				
				cameraBlitMat.SetTexture("_BlurTexB", customGrabRenderTextureC);
			}		

			_cam.targetTexture = cameraRenderTexture;
			_cam.depthTextureMode = DepthTextureMode.None;	

		}
		else
		{			
			_cam.targetTexture = null;
		}
		
	}
	
	void Start()
	{
		_transform = transform;
		Shader.SetGlobalFloat("_BloomCutoff", bloomCutoff);
	
		Shader.SetGlobalFloat("_BloomStrength", bloomStrength);
		Shader.SetGlobalFloat("_DOFApature", DOFApature);
	}
	
	// Update is called once per frame
	void OnPostRender()
	{
		if (UseIOSPostEffects)
		{
			float depthDist = Vector3.Distance(this._transform.position, focusTransform.position);
			Shader.SetGlobalFloat("_DepthFar", depthDist);
			
			if (!cheapPostEffects)
			{
				if (useCustomCamerabufferSize)
				{
					Graphics.Blit(cameraRenderTexture, customGrabRenderTextureB, cameraBlitMat, 1);	// First Down Sample (A) to Second Down Sample (B)
					Graphics.Blit(customGrabRenderTextureB, customGrabRenderTextureC, cameraBlitMat, 1);	// Second Down Sample (B) to Third Down Sample (C) 
					Graphics.Blit(null, customGrabRenderTextureD, cameraBlitMat, 0);	// Calculate Bloom & DOF @ Third Down Sample resolutions
				}
				else
				{
					Graphics.Blit(cameraRenderTexture, customGrabRenderTextureA); // Blit Framebuffer capture to First Down Sample (A)
					Graphics.Blit(customGrabRenderTextureA, customGrabRenderTextureB, cameraBlitMat, 1);	// First Down Sample (A) to Second Down Sample (B)
					Graphics.Blit(customGrabRenderTextureB, customGrabRenderTextureC, cameraBlitMat, 1);	// Second Down Sample (B) to Third Down Sample (C) 
					Graphics.Blit(null, customGrabRenderTextureD, cameraBlitMat, 0);	// Calculate Bloom & DOF @ Third Down Sample resolutions
				}
			} 
			else
			{
				Graphics.Blit(cameraRenderTexture, customGrabRenderTextureD, cameraBlitMat, 4);	// Everything done in one pass
			}
		}
	}
}
