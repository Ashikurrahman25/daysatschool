using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Clean : MonoBehaviour {

	public ToolType InteractWithTool;

	public ToolType InteractWithTool2 = ToolType.none;
	 

	int imagesCount;
	bool bChangeImage = false;
	public bool bDestroyOnEnd = true;
	public bool bFallDown = false;
	int state = 0;
	string tagName;
	int ToolNO;
	//PROMENLJIVE KOJIMA SE ODREDJUJe KOLIKO FAZA IMA PRILIKOM CISCENJA 
	//I DA LI SE OBJEKAT SKALIRA ILI MU SE MENJA PROVIDNOST 
	public int CleaningTransitionsAlpha = 0;
	public int CleaningTransitionsScale = 0;
	float alphaStep = 0;
	float scaleStep = 0;
	Vector3 trLocalScale;
	public static int CleanAndFixItemsCount = 0;
 
	ParticleSystem psFinisCleaning = null;

	void Start () {
		//yield return new WaitForSeconds(0.3f);
		if(transform.Find("ParticlesFinishCleaning")!=null)
		{
			psFinisCleaning = transform.Find("ParticlesFinishCleaning").GetComponent<ParticleSystem>();
			psFinisCleaning.transform.parent = transform.parent;
		}


		if(CleaningTransitionsAlpha > 0) alphaStep = 1/(float)CleaningTransitionsAlpha;
		if(CleaningTransitionsScale > 0) scaleStep =   1/(float)(CleaningTransitionsScale);
		trLocalScale = transform.localScale;

		ToolType[] cleaningTools = MenuCleaningTools.ReturnRoomTools(Gameplay.roomNo);
		for(int i = 1; i<=cleaningTools.Length; i++)
		{
			if(InteractWithTool == cleaningTools[i-1]) 
			{
				tagName= "T"+i.ToString();
				transform.tag = tagName;
				ToolNO = i;
				gameObject.layer =       LayerMask.NameToLayer("Tool"+ToolNO.ToString()+"Interact");
			}

			if(InteractWithTool2 == cleaningTools[i-1]) 
			{
				StartCoroutine(IncreaseCTItems((i-1)));
				CleanAndFixItemsCount++;
			}
		}
		if(transform.childCount > 0)
		{
			bChangeImage = true;
			for(int i = 1; i<=transform.childCount ;i++)
			{
				if(transform.Find("Image"+i.ToString())!=null ) transform.Find("Image"+i.ToString()).GetComponent<Image>().enabled = (i==1);
			}
		}

		CleanAndFixItemsCount++;
	}


	
	IEnumerator IncreaseCTItems(int _tool)
	{
		yield return new WaitForSeconds(0.5f);
		//Debug.Log("Povecao za 1");
		MenuCleaningTools.cleaningToolsItems[(_tool)] ++;

	}

	public void Cleaned()
	{ 
		
		if(bChangeImage)
		{
			state++;
			if(InteractWithTool2 != ToolType.none)
			{
				if(state == 1) //zamena alata na prvom 
				{
					Gameplay.Instance.ObjectCleaned(ToolNO);

					//zamena alata
					ToolType[] cleaningTools = MenuCleaningTools.ReturnRoomTools(Gameplay.roomNo);
					for(int i = 1; i<=cleaningTools.Length; i++)
					{

						if(InteractWithTool2 == cleaningTools[i-1]) 
						{
							tagName= "T"+i.ToString();
							transform.tag = tagName;
							ToolNO = i;
							gameObject.layer =       LayerMask.NameToLayer("Tool"+ToolNO.ToString()+"Interact");

						}
					}
				}
                TutorialCleanClassRoom.Instance.Phase2bEndPositions = new Transform[] {transform};
                TutorialCleanClassRoom.Instance.Phase2EndPositions = new Transform[] {transform};

				//Debug.Log("State  " + state);
				if(alphaStep>0)
				{
					if(state ==1)
					{
						transform.Find("Image1").GetComponent<Image>().enabled =false;
						transform.Find("Image2").GetComponent<Image>().enabled =true;
						if(CleaningTool.OneToolEnabledNo > -1) CleaningTool.OneToolEnabledNo = ToolNO;
					}
					//Debug.Log("PROVIDNOST2");
					 
					if(state<=(CleaningTransitionsAlpha))
						transform.Find("Image2" ).GetComponent<Image>().color = new Color(1,1,1,1-((state-1)*alphaStep) );

					else 
					{

						if(!bDestroyOnEnd   )
						{
							//Debug.Log("PROVIDNOST2 iskljuceno");
                            TutorialCleanClassRoom.Instance.phase2BTool  = -1;
							Gameplay.Instance.ObjectCleaned(ToolNO);
							if(psFinisCleaning !=null) psFinisCleaning.Play();
							gameObject.GetComponent<PolygonCollider2D>().enabled = false;
							this.enabled = false;
						}
						else  
						{
							//Debug.Log("PROVIDNOST2 unisteno");
                            TutorialCleanClassRoom.Instance.phase2BTool = -1;
							Gameplay.Instance.ObjectCleaned(ToolNO);
							if(psFinisCleaning !=null) psFinisCleaning.Play();
							GameObject.Destroy(gameObject);

						}

					}
				}
				else //zamena slika
				{
					
					if(state < transform.childCount  && state >0)
					{
						transform.Find("Image"+(state ).ToString()).GetComponent<Image>().enabled =false;
						transform.Find("Image"+(state+1).ToString()).GetComponent<Image>().enabled =true;
						if(CleaningTool.OneToolEnabledNo > -1) CleaningTool.OneToolEnabledNo = ToolNO;	 
					}

					if(!bDestroyOnEnd && state == (transform.childCount) )
					{
						Gameplay.Instance.ObjectCleaned(ToolNO);
						if(psFinisCleaning !=null) psFinisCleaning.Play();
						gameObject.GetComponent<PolygonCollider2D>().enabled = false;
						this.enabled = false;
					}
					else if(state == (transform.childCount) )
					{
						Gameplay.Instance.ObjectCleaned(ToolNO);
						if(psFinisCleaning !=null) psFinisCleaning.Play();
						GameObject.Destroy(gameObject);
					}
				}
			}
			else
			{
				if(state < transform.childCount-1  && state >0)
				{
					transform.Find("Image"+(state ).ToString()).GetComponent<Image>().enabled =false;
					transform.Find("Image"+(state+1).ToString()).GetComponent<Image>().enabled =true;
					if(!bDestroyOnEnd && state == (transform.childCount-1) ){
						Gameplay.Instance.ObjectCleaned(ToolNO);
						if(psFinisCleaning !=null) psFinisCleaning.Play();
						gameObject.GetComponent<PolygonCollider2D>().enabled = false;
						this.enabled = false;
					}
				}
				else if(state >= transform.childCount-1 && bDestroyOnEnd)
				{
	 
					//Camera.main.SendMessage("ObjectCleaned",ToolNO);
					Gameplay.Instance.ObjectCleaned(ToolNO);
					if(psFinisCleaning !=null) psFinisCleaning.Play();
				    GameObject.Destroy(gameObject);
				}
			}
		}

		else
		{
			if(alphaStep> 0)
			{
				//Debug.Log("PROVIDNOST");
				state++;
				if(state<CleaningTransitionsAlpha)
					transform.GetComponent<Image>().color = new Color(1,1,1,1-(state*alphaStep) );
			 
				else 
				{
					//Debug.Log("PROVIDNOST unisteno");
					Gameplay.Instance.ObjectCleaned(ToolNO);
					if(psFinisCleaning !=null) psFinisCleaning.Play();
					GameObject.Destroy(gameObject);
				}
			}
			else if(scaleStep> 0)
			{
				//Debug.Log("SKALIRANJE");
				state++;
				if(state<CleaningTransitionsScale)
					transform.localScale= trLocalScale * (1 - .5f* state*scaleStep) ;

				else 
				{
					Gameplay.Instance.ObjectCleaned(ToolNO);
					if(psFinisCleaning !=null) psFinisCleaning.Play();
					GameObject.Destroy(gameObject);
				}
			}
			else if(bFallDown)
			{
				//Debug.Log("PADANJE");
				Gameplay.Instance.ObjectCleaned(ToolNO);
				transform.GetComponent<PolygonCollider2D>().enabled = false;
				if(psFinisCleaning !=null) psFinisCleaning.Play();
				StartCoroutine("FallDown");
			}
			else 
			{
				//Debug.Log("UNISTENO ODMAH");
				Gameplay.Instance.ObjectCleaned(ToolNO);
				if(psFinisCleaning !=null) psFinisCleaning.Play();
				GameObject.Destroy(gameObject);
			}
		}

	}


	IEnumerator FallDown()
	{
		float speedX = Random.Range(-6f,6f);
		float speedY = 5f;
		float gravity = -.5f;
		while(transform.position.y>-7)
		{
			transform.position += new Vector3(speedX,speedY,0)*Time.fixedDeltaTime ;
			speedY+=gravity;
			yield return new WaitForFixedUpdate();
		}
		yield return new WaitForEndOfFrame();
		//Debug.Log("UNISTENO PADANJE");
		GameObject.Destroy(gameObject);
	}
}
