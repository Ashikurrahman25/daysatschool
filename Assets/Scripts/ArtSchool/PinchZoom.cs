using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using AdvancedMobilePaint;

/// <summary>
/// <para>Scene: Gameplay</para>
/// <para>Object: Paint</para>
/// <para>Description: Skripta za PinchZoom. Izracunava scale i poziciju PaintPanel objekta u zavisnosti od polozaja tapova. Sadrzi u sebi logiku za razlikovanje Zoom-a od iscrtavanja</para>
/// </summary>
public class PinchZoom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
{
	public float zoomSpeed;
	public float scaleMax;
	public Transform objectToScale;
	public AdvancedMobilePaint.AdvancedMobilePaint paintEngine;
	
	// Pinch Zoom promenljive
	float factor; 						// za koliko ce se povecati scale datog objekta
	float prevTouchDeltaMag;			// razlika u razdaljini dodira u prethodnom frejmu
	float touchDeltaMag;				// razlika u razdaljini dodira u trenutnom frejmu
	float deltaMagnitudeDiff;			// razlika u razdaljinama izmedju dva frejma
	float clamper;						// ogranicava pozicioniranje datog objekta
	float midX;							// X koordinata tacke izmedju dva dodira
	float midY;							// Y koordinata tacke izmedju dva dodira
	float magnitudeFactor;				// Odnos preNorm vektora i lenghtX duzi
	float lenghtX;						// duzina X ose skaliranog objekta (sirina)
	Touch t0;
	Touch t1;
	Vector2 t0PrevPos;
	Vector2 t1PrevPos;
	Vector3 offsetVector;				// razlika izmedju localPosition objekta koji se pomera i pozicije pointera (ili vektora midX,midY,0 ukoliko je pomeranje sa 2 prsta)
	Vector3 preClampedPosition;			// izracunata pozicija objekta pre ogranicavanja clamp funkcijom
	Vector3 preNorm;					// razlika izmedju ScreenSpace pozicije objekta koji se pomera i vektora midX,midY,0
	Vector3 norm;						// normalizovani preNorm vektor
	bool touchInside;					// da li je dodirnut zeljeni objekat
	bool oneFingerZoom = false;			// da li se zumiranje vrsi jednim prstom 
	bool inputUpdated = false;			// da li je input azuriran
	//	float normX;
	//	float normY;

	// pomocne promenljive za iscrtavanje FloodFill brush-a
	Vector2 pointerDownPos;
	Vector2 pointerUpPos;

//    Text logText;

    void Start()
    {
//        logText = GameObject.Find("Canvas/Text").GetComponent<Text>();
    }


	public void OnPointerDown(PointerEventData eventData)
	{
		oneFingerZoom = true;
		pointerDownPos = eventData.position;
		if(eventData.pointerId <= 0)
		{
//			Debug.Log("PointerDown");
			touchInside = true;
			offsetVector = objectToScale.localPosition - new Vector3 (eventData.pressPosition.x, eventData.pressPosition.y,0);
			if(!SubMenusController.StickerMode)
				paintEngine.drawEnabled = true;
		}

		if(eventData.pointerId == 1)
		{
//			magnitude = 0;
			midX = (Input.GetTouch(0).position.x + Input.GetTouch(1).position.x)/2;
			midY = (Input.GetTouch(0).position.y + Input.GetTouch(1).position.y)/2;
			offsetVector = objectToScale.localPosition - new Vector3 (midX,midY,0);

			preNorm = Camera.main.WorldToScreenPoint(objectToScale.position) - new Vector3(midX,midY,0);
			preNorm.z = 0;
			norm = Vector3.Normalize (preNorm);
			norm.z = 0;
			magnitudeFactor = preNorm.magnitude / (objectToScale.GetComponent<RectTransform>().sizeDelta.x*objectToScale.localScale.x);

			inputUpdated = true;
		}

		CancelInvoke("DrawIsIntended");
		if(paintEngine.drawMode != DrawMode.FloodFill)
			Invoke ("DrawIsIntended", 0.05f);	// delay od 0.08s kako bi se razlikovao zoom od iscrtavanja.
		else
			paintEngine.drawIntended = true;

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.LogError("POINTER UP START");
		pointerUpPos = eventData.position;


		CancelInvoke("DrawIsIntended");
		Debug.LogError(Vector2.Distance(pointerDownPos, pointerUpPos));
		if (paintEngine.drawMode == DrawMode.FloodFill && Vector2.Distance (pointerDownPos,pointerUpPos) <= 10)
		{
			paintEngine.DrawOnPointerUp();

			Debug.LogError("DRAWWWW");
		}

		if(eventData.pointerId <= 0)
		{
			touchInside = false;
//			paintEngine.drawEnabled = false;
			offsetVector = Vector2.zero;
		}

		inputUpdated = false;
		oneFingerZoom = false;

		Debug.LogError("POINTER UP END");
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
//		Debug.Log("Drag began");
//        logText.text = "Drag began";
		CancelInvoke("DrawIsIntended");
		if(paintEngine.drawMode != DrawMode.FloodFill && eventData.pointerId == 0)
			paintEngine.drawIntended = true;
	}

	//mora da ima OnDrag da bi OnBeginDrag bio okinut
	public void OnDrag(PointerEventData eventData)
	{
		if(!oneFingerZoom)return;
//		Debug.Log("Draging u najvece...");

		if(Input.touchCount == 1 && paintEngine.drawMode == DrawMode.FloodFill)
		{
			// Pozicioniranje panela
			clamper = (objectToScale.GetComponent<RectTransform>().sizeDelta.x/2)*(objectToScale.localScale.x-1);
			preClampedPosition = new Vector3 (eventData.position.x,eventData.position.y,0) + offsetVector;
			objectToScale.localPosition = new Vector3(
				Mathf.Clamp(preClampedPosition.x, -clamper,clamper),
				Mathf.Clamp(preClampedPosition.y, -clamper,clamper),
				0);
		}
	}

	void OnApplicationPause()
	{
		touchInside = false;
	}

	void Update()
	{
		if (Input.touchCount == 2 && inputUpdated)
		{
			oneFingerZoom = false;
			t0 = Input.GetTouch(0);
			t1 = Input.GetTouch(1);
			CancelInvoke("DrawIsIntended");		// Ukoliko se registruje drugi tap, prekida se Invoke za dozvolu iscrtavanja i krece se u zumiranje.
			paintEngine.drawIntended = false;

			if(!touchInside)return;

//            logText.text = "Dva prsta";

			// Pozicija dodira u prethodnom frejmu.
			t0PrevPos = t0.position - t0.deltaPosition;
			t1PrevPos = t1.position - t1.deltaPosition;
			
			// Udaljenosti dodira u prethodnom i trenutnom frejmu.
			prevTouchDeltaMag = (t0PrevPos - t1PrevPos).magnitude;
			touchDeltaMag = (t0.position - t1.position).magnitude;
			
			// Razlika u udaljenosti izmedju dva frejma.
			deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			factor = deltaMagnitudeDiff * zoomSpeed;

			objectToScale.localScale -= new Vector3 (factor,factor,0);

			if(objectToScale.localScale.x < 1)
				objectToScale.localScale = Vector3.one;
			if(objectToScale.localScale.x > scaleMax)
				objectToScale.localScale = new Vector3(scaleMax,scaleMax,1);

			// Pozicioniranje panela
			clamper = (objectToScale.GetComponent<RectTransform>().sizeDelta.x/2)*(objectToScale.localScale.x-1);

			midX = (t0.position.x + t1.position.x)/2;
			midY = (t0.position.y + t1.position.y)/2;

//			normX = (offsetVector.x - 0)/( - 0);
//			normY = (offsetVector.y - 0)/( - 0);

//			Vector3 norm = Vector3.Normalize (preNorm);
//			norm.z = 0;
			lenghtX = objectToScale.GetComponent<RectTransform>().sizeDelta.x*objectToScale.localScale.x;

//			preClampedPosition = new Vector3 (midX,midY,0) + offsetVector + magnitude*preNorm;
			preClampedPosition = new Vector3 (midX, midY, 0) + offsetVector + norm*magnitudeFactor*lenghtX - preNorm;
			if(Mathf.Abs(factor)>0)
			{
				objectToScale.localPosition = new Vector3(
					Mathf.Clamp(preClampedPosition.x, -clamper,clamper),
					Mathf.Clamp(preClampedPosition.y, -clamper,clamper),
					0);
//                logText.text = "Zumbira";

			}
				
		}

		#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.UpArrow))
		{
			objectToScale.localScale += new Vector3 (zoomSpeed,zoomSpeed,0);

			if(objectToScale.localScale.x < 1)
				objectToScale.localScale = Vector3.one;
			if(objectToScale.localScale.x > scaleMax)
				objectToScale.localScale = new Vector3(scaleMax,scaleMax,1);
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			objectToScale.localScale -= new Vector3 (zoomSpeed,zoomSpeed,0);
			
			if(objectToScale.localScale.x < 1)
				objectToScale.localScale = Vector3.one;
			if(objectToScale.localScale.x > scaleMax)
				objectToScale.localScale = new Vector3(scaleMax,scaleMax,1);
		}
		#endif
	}

	void DrawIsIntended()
	{
		paintEngine.drawIntended = true;
	}
}










































