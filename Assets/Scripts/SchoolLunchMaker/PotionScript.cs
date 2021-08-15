using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour {

	public int potionIndex;

	// If potion than true else if mission indicator than false
	public bool potion;

	// If this potion is already poured in couldron this becomes true
	public bool potionUsed;

	public Vector3 startPosition;
	public bool holdingThisPotion;

	public float startSpeed;

	public bool pouring;

	void Start()
	{
		//startPosition = transform.localPosition;
		startPosition = transform.position;

		if (GetComponent<ItemDrag>() != null)
			startSpeed = GetComponent<ItemDrag>().draggingSpeed;

		pouring = false;
	}

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.name == "BlenderImage" && !MagicPotionManager.magicPotionManager.pouringSequenceStarted)
		{
			pouring = true;
			GetComponent<ItemDrag>().enabled=false;
//			GetComponent<BoxCollider2D>().enabled = false;
			GoToPouringPositionAndPour();
			OnMouseUp();
		}
	}

	public void OnMouseDown()
	{
        Debug.Log("MOUSE DOWN");
		if (potion && !MagicPotionManager.magicPotionManager.pouringSequenceStarted)
		{
			GetComponent<ItemDrag>().draggingSpeed = MagicPotionManager.magicPotionManager.potionsDraggingSpeed;
			holdingThisPotion = true;
			GetComponent<ItemDrag>().backToStartPosition = false;
//			GetComponent<BoxCollider2D>().enabled = true;
		}
		else
		{
			GetComponent<ItemDrag>().draggingSpeed = 0;
			holdingThisPotion = false;
//			GetComponent<BoxCollider2D>().enabled = false;
		}
	}
	
	public void OnMouseUp()
	{
		if (potion && !pouring)
		{
			holdingThisPotion = false;
//			GetComponent<BoxCollider2D>().enabled = false;
//			GetComponent<ItemDrag>().x = false;
			GetComponent<ItemDrag>().backToStartPosition = true;
			GetComponent<ItemDrag>().draggingSpeed = startSpeed;
			ReturnToStartPosition();
		}
	}

	public void GoToPouringPositionAndPour()
	{
		StartCoroutine(GoToPouringPositionAndPourCoroutine());
	}

	IEnumerator GoToPouringPositionAndPourCoroutine()
	{
//		MagicPotionManager.magicPotionManager.closeButton.GetComponent<Button>().interactable = false;
       
		MagicPotionManager.magicPotionManager.pouringSequenceStarted = true;

		GetComponent<ItemDrag>().draggingSpeed = 0;

		while (Vector2.Distance(transform.position, MagicPotionManager.magicPotionManager.potionPouringPosition.transform.position) >= 0.05f)
		{
            Debug.Log("PotionScript GTPPAPC");
            StopCoroutine(ReturnToStartPositionCoroutine());
            transform.GetComponent<ItemDrag>().StopAllCoroutines();
			Vector3 screenPoint = MagicPotionManager.magicPotionManager.potionPouringPosition.transform.position;
			screenPoint.z = 14f;
//			screenPoint.z = transform.position.z;
			
			transform.position = Vector3.Lerp(transform.position, screenPoint, 7f * Time.deltaTime);

			yield return new WaitForSeconds(0.01f);

			if (Vector2.Distance(transform.position, MagicPotionManager.magicPotionManager.potionPouringPosition.transform.position) <= 0.05f)
			{
				screenPoint = MagicPotionManager.magicPotionManager.potionPouringPosition.transform.position;
				screenPoint.z = 14f;

				transform.position = screenPoint;
			}
		}

		GetComponent<Animator>().Play("PotionPour", 0, 0);

		yield return new WaitForSeconds (0.34f);
        SoundManagerPotionMaker.PlaySound("FruitInBlenderSound");

        transform.GetChild (0).gameObject.GetComponent<ParticleSystem>().Play();

		MagicPotionManager.magicPotionManager.ChangeLiquidColor(transform.GetChild (0).GetComponent<ParticleSystem>().startColor);

		yield return new WaitForSeconds (0.52f);
		
		transform.GetChild (0).gameObject.GetComponent<ParticleSystem>().Stop();

		yield return new WaitForSeconds(0.32f);

		MagicPotionManager.magicPotionManager.CheckIfPotionIsMission(potionIndex);

		GetComponent<ItemDrag>().draggingSpeed = startSpeed;

		pouring = false;
		OnMouseUp();
        transform.GetComponent<ItemDrag>().BackToStartPosition();
        Timer.Schedule(this, 1f, delegate
            {
                transform.GetComponent<ItemDrag>().enabled=true;
            });
		yield return new WaitForSeconds(0.33f);
		GetComponent<Animator>().Play("PotionAppear", 0, 0);
        transform.GetComponent<ItemDrag>().enabled = false;

        //MagicPotionManager.magicPotionManager.closeButton.GetComponent<Button>().interactable = true;
        //MagicPotionManager.magicPotionManager.pouringSequenceStarted = false;
    }

    public void ReturnToStartPosition()
	{
        StopCoroutine(GoToPouringPositionAndPourCoroutine());
		StartCoroutine(ReturnToStartPositionCoroutine());
	}

	void OnEnable()
	{
		if (MagicPotionManager.magicPotionManager != null)
		{
			if (MagicPotionManager.magicPotionManager.firstTimeStarted)
			{
				transform.position = startPosition;


				if (!potion && potionUsed)
					GetComponent<Animator>().Play("MissionPotionUsed", 0, 1f);
			}
		}
	}
	
	IEnumerator ReturnToStartPositionCoroutine()
	{
		while (Vector3.Distance(transform.position, startPosition) >= 2f&&!pouring)
		{
            Debug.Log("PotionScript RTSPC");

            transform.position = Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(startPosition), MagicPotionManager.magicPotionManager.potionsReturningSpeed * Screen.dpi / 96);
			yield return new WaitForSeconds(0.01f);
			
			if (Vector3.Distance(transform.localPosition, startPosition) <= 2f)
				transform.position = startPosition;
		}
	}
}
