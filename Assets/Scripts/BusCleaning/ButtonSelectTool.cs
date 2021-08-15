using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelectTool : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IPointerExitHandler    
{
	public bool changeInteractable = true;
	bool bPointerIn = false;
	bool bPointerUp= true;
	Animator anim;
	Button btn;

	int stars = 0;

	void Start () {
		anim = transform.GetComponent<Animator>();
		btn = transform.GetComponent<Button>();
	}


	
	
	public void OnPointerDown( PointerEventData eventData)
	{
		if(!changeInteractable && !btn.interactable ) return;
		if(bPointerUp )
		{
			btn.interactable = true;
			bPointerIn = true;
			bPointerUp = false;
			anim.SetBool("bPointerIn",bPointerIn );
		}
	}
	
	public void OnPointerUp( PointerEventData eventData)
	{
		bPointerUp = true;
		bPointerIn = false;
		anim.SetBool("bPointerIn",bPointerIn );
	}
	
	public void OnPointerExit( PointerEventData eventData)
	{
		bPointerIn = false;
		anim.SetBool("bPointerIn",bPointerIn );
		if(changeInteractable)
		{
			btn.interactable = false;
			anim.SetTrigger("Highlighted" );
		}
	}

	public void AddStar(int _stars)
	{
		if(_stars > 5 || _stars < 1) return;
		stars = _stars;
		transform.Find("HolderStars/Star"+stars.ToString()).GetComponent<Animator>().SetBool("starAchieved",true);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Star);
		if(stars == 5) 
		{
			transform.GetComponent<Animator>().SetTrigger("ActionComplete");
			btn.interactable = false;
		}

	}

	public void ResetAllStars()
	{
		for(int i = 1;i<=5;i++)
		{
			transform.Find("HolderStars/Star"+i.ToString()).GetComponent<Animator>().SetBool("starAchieved",false); 
		}
		btn.interactable = true;
	}

	
}
