using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostFX : MonoBehaviour {

    public Material mat;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
