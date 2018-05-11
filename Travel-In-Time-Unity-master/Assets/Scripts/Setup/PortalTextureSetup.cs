using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour {


    public Camera camera;
    public Material cameraMat;
	// Use this for initialization
	void Start () {
		if(camera.targetTexture != null)
        {
            camera.targetTexture.Release();
        }
        camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat.mainTexture = camera.targetTexture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
