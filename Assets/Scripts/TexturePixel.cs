using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePixel : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    // Use this for initialization
    void Start () {
	    var texture = new Texture2D(100,100);
        texture.SetPixel(1,1,Color.blue);
        texture.SetPixel(2,2,Color.blue);
        texture.filterMode = FilterMode.Point;
        texture.Apply();
      
        MeshRenderer.material.mainTexture = texture;


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
