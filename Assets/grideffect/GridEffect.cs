using UnityEngine;
using System.Collections;


[ExecuteInEditMode]

public class GridEffect : MonoBehaviour {

	public Material material;

	[SerializeField]
	[Range(0,1)]
	float amount=1;
	public float Amount {get {return amount;} set { amount=value; }}

	[SerializeField]
	[Range(1,2)]
	int gridtype=1;
	public int GridType {get {return gridtype;} set { gridtype=value; }}




	[SerializeField]
	Color gridcolor=Color.black;
	public Color GridColor {get {return gridcolor;} set {gridcolor=value; }}




	[SerializeField]
	[Range(1,200)]
	int xstep=2;
	public int Xstep {get {return xstep;} set { xstep=value; }}


	[SerializeField]
	[Range(1,200)]
	int ystep=2;
	public int Ystep {get {return ystep;} set { ystep=value; }}


	[SerializeField]
	[Range(0,10)]
	int xoffset=0;
	public int Xoffset {get {return xoffset;} set { xoffset=value; }}


	[SerializeField]
	[Range(0,10)]
	int yoffset=0;
	public int Yoffset {get {return yoffset;} set { yoffset=value; }}


	[SerializeField]
	[Range(0,1)]
	float xmin=0;
	public float Xmin {get {return xmin;} set { xmin=value; }}

	[SerializeField]
	[Range(0,1)]
	float xmax=1;
	public float Xmax {get {return xmax;} set { xmax=value; }}


	[SerializeField]
	[Range(0,1)]
	float ymin=0;
	public float Ymin {get {return ymin;} set { ymin=value; }}

	[SerializeField]
	[Range(0,1)]
	float ymax=1;
	public float Ymax {get {return ymax;} set { ymax=value; }}

	[SerializeField]
	[Range(0,1)]
	int flashing=0;
	public int Flasing {get {return flashing;} set { flashing=value; }}


	[SerializeField]
	[Range(0,1)]
	int flashcount=0;
	public int Flashcount {get {return flashcount;} set { flashcount=value; }}

	[SerializeField]
	[Range(0, 100)]
	int lineSizeX = 0;
	public int LineSizeX { get { return lineSizeX; } set { lineSizeX = value; } }

	[SerializeField]
	[Range(0, 100)]
	int lineSizeY = 0;
	public int LineSizeY { get { return lineSizeY; } set { lineSizeY = value; } }



	void Update()
	{


		//for flashing

		if (flashing == 1) {
			flashcount += 1;
			if(flashcount == ystep*4)
			{
				flashcount = 0;
			}
		}





	}



	void OnRenderImage(RenderTexture source,RenderTexture destination)
	{

		material.SetFloat ("_Amount", amount);
		material.SetInt ("_GridType", gridtype);

		material.SetColor ("_GridColor", gridcolor);
		material.SetInt ("_Xstep", xstep);
		material.SetInt ("_Ystep", ystep);

		material.SetInt ("_Xoffset", xoffset);
		material.SetInt ("_Yoffset", yoffset);

		material.SetFloat ("_Xmin", xmin);
		material.SetFloat ("_Xmax", xmax);
		material.SetFloat ("_Ymin", ymin);
		material.SetFloat ("_Ymax", ymax);

		material.SetInt ("_Flasing", flashing);
		material.SetInt ("_Flashcount",flashcount);

		material.SetInt("_LineSizeX", LineSizeX);
		material.SetInt("_LineSizeY", LineSizeY);


		Graphics.Blit (source, destination, material);

	}


}
