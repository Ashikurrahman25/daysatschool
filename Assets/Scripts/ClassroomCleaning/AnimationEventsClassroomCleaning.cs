using UnityEngine;
using System.Collections;

public class AnimationEventsClassroomCleaning : MonoBehaviour {

	public void CleaningAnimationFinished()
	{
		transform.parent.parent.SendMessage("CleaningAnimationFinised",SendMessageOptions.DontRequireReceiver);
	}

	public void WateringCanAnimationFinished()
	{
		//transform.parent.parent.SendMessage("WateringCanAnimEnd",SendMessageOptions.DontRequireReceiver);
	}

	public void StartParticles()
	{
		transform.GetComponentInChildren<ParticleSystem>().Play();
	}
		
	public void OpenMoreOptionsMenuAnimationFinished()
	{
		transform.parent.SendMessage("OpenMoreOptionsMenuAnimationFinished",SendMessageOptions.DontRequireReceiver);
	}

	public void CloseMoreOptionsMenuAnimationFinished()
	{
		transform.parent.SendMessage("CloseMoreOptionsMenuAnimationFinished",SendMessageOptions.DontRequireReceiver);
	}

	public void WashingMachineAnimationFinished()
	{
		Gameplay.Instance.WashingMachineAnimationCycleFinished();
	}

	public void CarouselSelectedAnimationOver( )
	{
		
		HomeScene.Instance.CarouselSelected();
	}

	public void CarouselSelectRoomShowAnimationEnded( )
	{
		//transform.FindChild("RoomsCarousel").GetComponent<ItemsSlider>().Init();
		 
	}

	public void CarouselSelectRoomHideAnimationEnded( )
	{

		//HomeScene.Instance.CarouselSelected();
	}

    public void PlayWinSoundFromAnim()
    {
        //PAVLE SOUNDMANAGER pusti zvuk
    }

    public void PlayStarSoundFromAnim()
    {
        //PAVLE SOUNDMANAGER zvuk zvezdice
    }
}
