using UnityEngine;
using System.Collections;

public class UVAnimation : MonoBehaviour {

	public int uvTileY = 4; // texture sheet columns 
	public int uvTileX = 4; // texture sheet rows

	public int fps = 30;   
	private int index;

	private Vector2 size;  
	private Vector2 offset; 

	private Renderer renderer;

	void Start() 
	{ 
		renderer = GetComponent<Renderer>(); 
	}

	void Update () {

		//calculate the index
		index = (int)(Time.time * fps); 

		//repeat when when exhausting all frames
		index = index % (uvTileY * uvTileX); 

		//size of each tile  
		size = new Vector2(1.0f / uvTileY, 1.0f / uvTileX);   

		//split into horizontal and vertical indexes
		var uIndex = index % uvTileX;
		var vIndex = index / uvTileX; 

		//build the offset   
		//v coordinate is at the bottom of the image in openGL, so we invert it
		offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);

		renderer.material.SetTextureOffset ("_MainTex", offset); 
		renderer.material.SetTextureScale ("_MainTex", size); 
	}
}
