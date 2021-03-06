using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using AdvancedMobilePaint;

/// <summary>
/// <para>Scene: Gameplay</para>
/// <para>Object: Sticker prefab</para>
/// <para>Description: Skripta za selektovanje i drag stikera</para>
/// </summary>
public class Sticker : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler{

//	public int stickerIndex;

	RaycastHit hit;
	UStep stickerUndoStep;

	public void OnPointerDown(PointerEventData eventData)
	{
		if(eventData.pointerId > 0)return;
		StickersController.SelectSticker(this.gameObject);
//		Debug.Log("Current Sticker: " +StickersController.currentSticker.name);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if(StickersController.currentSticker != this.gameObject)return;
//		Debug.Log("Dragging sticker");
		if(eventData.pointerId > 0)return;
		if(!Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position),out hit))return;
		transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
		transform.localPosition = new Vector3 (transform.localPosition.x,transform.localPosition.y,0);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		stickerUndoStep = new UStep();
		stickerUndoStep.type = -2;
		stickerUndoStep.sticker = gameObject;
		stickerUndoStep.stickerLocalPos = transform.localPosition;
		stickerUndoStep.stickerScale = transform.localScale;
		stickerUndoStep.stickerRotation = transform.rotation;
		transform.parent.parent.GetComponent<PaintUndoManager>().AddStep(stickerUndoStep);
	}
}













