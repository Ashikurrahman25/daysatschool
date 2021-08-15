using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class MiniGamePainting   : MonoBehaviour {
	public AdvancedMobilePaint.AdvancedMobilePaint paintEngine;
	public Texture2D paintSurface;
	public Texture2D paintMaskSurface;
	public Texture2D brush;
	public RawImage PanitingBoard;
	public Color[] PaintingColors;

	bool bShowRoom = false;
	bool bEnableChangeColor = true;
	int activeColorIndex = 9;

	static Texture2D tmpDrawingTex ;
	int bojanka = 1;

	public static int AktivniNadDugmetom = 0;
	public static int removedCount = 0;
	public static bool FirstPoint = true;

	Texture2D psfc;
	public bool bInit = false;
	void Awake()
	{
		if(Gameplay.roomNo == 1)
		{
			 StartCoroutine("SetNewDrawingTexture");
		}

		if(Gameplay.roomNo == 5)
		{
			FirstPoint = false;
			bojanka = Random.Range(1,6);
			psfc = (Texture2D) Resources.Load<Texture2D>("Bojanka/"+bojanka.ToString().PadLeft(2,'0'));
			paintEngine.SetDrawingTexture(psfc);
		}

        transform.Find("DrawingPanel/CanvasMachine").GetComponent<AdvancedMobilePaint.AdvancedMobilePaint>().drawIntended = true;
	}

	void OnDestroy()
	{
		tmpDrawingTex = null;
	}

	IEnumerator SetNewDrawingTexture()
	{
		yield return new WaitForSeconds(0.2f);
		Vector2 sizeDelta = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta;
        paintEngine.SetDrawingTexture(new Texture2D((int) sizeDelta.x, (int) sizeDelta.y));
        paintEngine.ReadClearingImage();
	}

	IEnumerator Start()
	{
		yield return new WaitForSeconds(0.1f);
		if(tmpDrawingTex !=null)  {   PanitingBoard.texture =  tmpDrawingTex;}


		 
		if(Gameplay.roomNo == 1)
		{
 
			//	Vector2 sizeDelta = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta;
			//	paintEngine.SetNewDrawingTexture((int) sizeDelta.x, (int) sizeDelta.y);
				
			paintEngine.useLockArea=false;
			paintEngine.useMaskLayerOnly=false;
			paintEngine.useThreshold=false;
	
				//paintEngine.SetBitmapBrush(brush,AdvancedMobilePaint.BrushProperties.Default,false,true, PaintingColors[activeColorIndex],false,false,null);

			paintEngine.SetVectorBrush(AdvancedMobilePaint.VectorBrush.Circle,8,8,  PaintingColors[activeColorIndex],null,false,true,false,false);

		}

		if(Gameplay.roomNo == 5)
		{
			FirstPoint = false;
			paintEngine.SetDrawingTexture(psfc);
			paintEngine.SetFloodFIllBrush(PaintingColors[activeColorIndex],true);
 
			paintEngine.useLockArea=false;
			paintEngine.useThreshold=false;
			 
			paintEngine.canDrawOnBlack=false;
		}
		paintEngine.drawEnabled=false; 
	}

	// Use this for initialization
	void OnEnable () {
 
		transform.Find("Colors/Color"+ (activeColorIndex+1).ToString().PadLeft(2,'0')).transform.localScale = 1.4f*Vector3.one;
		transform.Find("Colors/Color"+ (activeColorIndex+1).ToString().PadLeft(2,'0')).GetComponent<RectTransform>().SetAsLastSibling();

		if(Gameplay.roomNo == 1)
		{
			paintEngine.drawMode=AdvancedMobilePaint.DrawMode.CustomBrush;
			//paintEngine.SetBitmapBrush(brush,AdvancedMobilePaint.BrushProperties.Default,false,true,PaintingColors[activeColorIndex],false,false,null);
			paintEngine.SetVectorBrush(AdvancedMobilePaint.VectorBrush.Circle,8,8,  PaintingColors[activeColorIndex],null,false,true,false,false);
			//paintEngine.drawEnabled=true;
		}
		else if(Gameplay.roomNo == 5)
		{
			paintEngine.SetFloodFIllBrush(PaintingColors[activeColorIndex],true);
			paintEngine.useMaskLayerOnly=false;
			FirstPoint = false;	
			paintEngine.useLockArea=false;
			paintEngine.useThreshold=false;
			//paintEngine.drawEnabled=true;
			paintEngine.canDrawOnBlack=false;
		}

		AktivniNadDugmetom = 0;
		removedCount = 0;
		paintEngine.drawEnabled = bInit;
	}
	


	void Update()
	{
		if(paintEngine.drawEnabled)
		{
		   	removedCount = 0;
			if(paintEngine.drawEnabled && Input.GetMouseButtonDown(0))
			{
				 	FirstPoint = true;
					StartCoroutine("DelayDrawing");

					PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
					eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					List<RaycastResult> results = new List<RaycastResult>();
					EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
					foreach(RaycastResult hit in results)
					{
						if(hit.gameObject.tag == "BUTTON" ) 
						{
							AktivniNadDugmetom = 1 ;

							StopCoroutine("DelayDrawing");
							break;
						}
					}
				}

			if ( paintEngine.drawEnabled  && Input.GetMouseButtonUp(0))
			{
				StopCoroutine("DelayDrawing");
				 FirstPoint = true;
				AktivniNadDugmetom = 0;
	 
			}
	 
			if( Input.GetMouseButton(0) &&  AktivniNadDugmetom  == 0 )
			{
				SoundManagerCleaningClassroom.Instance.Play_Sound(   SoundManagerCleaningClassroom.Instance.Draw);
			}
			else
			{
				SoundManagerCleaningClassroom.Instance.Stop_Sound (   SoundManagerCleaningClassroom.Instance.Draw);
			}
			 
		}
	}


	IEnumerator DelayDrawing()
	{
		FirstPoint = false;
		//yield return new WaitForEndOfFrame();
		// yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
	 //	FirstPoint = false;
	}





	public void ShowRoom()
	{
		if(!bShowRoom)
		{
			PanitingBoard.texture =  paintEngine.tex;
			PanitingBoard.enabled = true;
			bShowRoom = true; 
		}
		else
		{
			AktivniNadDugmetom = 0;
			removedCount = 0;
		}
	}


	public void ChangeColor( int colorInd )
	{
		if(activeColorIndex !=colorInd && bEnableChangeColor) StartCoroutine(ChangeColor(colorInd , .5f));
	}

	IEnumerator ChangeColor(int colorInd, float pauseTime)
	{
		 
		bEnableChangeColor = false;
		transform.Find("Colors/Color"+ (activeColorIndex+1).ToString().PadLeft(2,'0')).transform.localScale = 1f*Vector3.one;
		activeColorIndex = colorInd;
		transform.Find("Colors/Color"+ (activeColorIndex+1).ToString().PadLeft(2,'0')).transform.localScale = 1.4f*Vector3.one;
		transform.Find("Colors/Color"+ (activeColorIndex+1).ToString().PadLeft(2,'0')).GetComponent<RectTransform>().SetAsLastSibling();
		if(Gameplay.roomNo == 5)
		{
			paintEngine.SetFloodFIllBrush(PaintingColors[activeColorIndex],true);
			 
			//yield return new WaitForEndOfFrame();
			//paintEngine.useThreshold = true;
			//paintEngine.paintThreshold = 50;
			yield return new WaitForSeconds(pauseTime);
		}
		else
		{
			//paintEngine.SetBitmapBrush(brush,AdvancedMobilePaint.BrushProperties.Default,false,true, PaintingColors[colorInd],false,false,null);
			paintEngine.SetVectorBrush(AdvancedMobilePaint.VectorBrush.Circle,8,8,  PaintingColors[activeColorIndex],null,false,true,false,false);
			yield return new WaitForSeconds(pauseTime);
		}
		bEnableChangeColor = true;

	}

	public void Eraser()
	{
		if(paintEngine.drawEnabled)
		{
			if(Gameplay.roomNo == 1)
			{
				paintEngine.ClearImage();
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(   SoundManagerCleaningClassroom.Instance.Eraser);
			}
			if(Gameplay.roomNo == 5)
			{

				Texture2D psfc = (Texture2D) Resources.Load<Texture2D>("Bojanka/"+bojanka.ToString().PadLeft(2,'0'));
				paintEngine.SetDrawingTexture(psfc);
			}
		}
	}




	public void btnClosePaintingClick()
	{
		StartCoroutine("DelayClosePainting");
		SoundManagerCleaningClassroom.Instance.Stop_Sound (   SoundManagerCleaningClassroom.Instance.Draw);
	}

	IEnumerator DelayClosePainting()
	{
		BlockClicks.Instance.SetBlockAll(true);
		paintEngine.drawEnabled=false;
		//yield return new WaitForSeconds(0.6f);
		//paintEngine.enabled = false;
		yield return new WaitForSeconds(0.4f);
		PanitingBoard.texture =  paintEngine.tex;
		PanitingBoard.enabled = true;

		Gameplay.Instance.ShowRoom();
		Gameplay.Instance.RightMenuButtons.GetComponent<MenuMoreOptions>(). CloseRightMenu();
		tmpDrawingTex = 	(Texture2D) PanitingBoard.texture;
		gameObject.SetActive(false);
		BlockClicks.Instance.SetBlockAll(false);
	}


}
