using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintVertices : MonoBehaviour
{
    public MeshRenderer MeshRenderer;

    public MeshFilter MeshFilter;
	// Use this for initialization
	void Start ()
	{
	    var mesh = MeshFilter.mesh;
	    var vertices = mesh.vertices;
	    var colors = new Color[vertices.Length];

	    colors[0] = Color.black;
	    colors[3] = Color.blue;
	    colors[4] = Color.blue;
	    colors[5] = Color.blue;
	    colors[6] = Color.blue;
	    colors[9] = Color.red;
	    colors[20] = Color.green;

        mesh.colors = colors;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
