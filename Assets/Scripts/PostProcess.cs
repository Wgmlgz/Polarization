using UnityEngine;
using System.Collections;

public class PostProcess : MonoBehaviour
{

	public Material material;

	
	void Update()
	{


	}



	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{

		Graphics.Blit(source, destination, material);

	}


}