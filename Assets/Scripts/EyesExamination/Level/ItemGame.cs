using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class ItemGame : MonoBehaviour {

	public bool partOfPhase;

	public void GameFinished()
	{
        Debug.Log("Game Finished Item Game");
		if (partOfPhase)
		{
			LevelManager.levelManager.phaseQuestsCompleted[LevelManager.levelManager.currentPhase]++;

			if (LevelManager.levelManager.CheckIfPhaseIsFinished())
			{
                // Play particle sound

                SoundManagerEyeExamination.PlaySound("BigParticle");
                LevelManager.levelManager.successParticleHolder.GetComponent<ParticleSystem>().Play();

//				// Show next phase button
//				if (LevelManager.levelManager.currentPhase < LevelManager.levelManager.phaseQuests.Count - 1)
//					LevelManager.levelManager.nextPhaseButton.SetActive(true);
//				else
//					LevelManager.levelManager.CameraButtonPhaseClicked();
////					LevelManager.levelManager.cameraPhaseButton.SetActive(true);
			}
		}
        EyesExaminationGameManager.Instance.MiniGameFinished();
        // Character idle animation playing just in case
        //		LevelManager.levelManager.characterAnimator.Play("CharacterIdle", 0, 0);

        // Play sound and particles
        SoundManagerEyeExamination.PlaySound("BigParticle");

        //		LevelManager.levelManager.successParticleHolder.GetComponent<ParticleSystem>().Play();
    }
}
