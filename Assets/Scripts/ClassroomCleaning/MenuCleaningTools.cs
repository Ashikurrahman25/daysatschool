using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuCleaningTools : MonoBehaviour {
	 

	ToolType[] tools;
	public static int[] cleaningToolsItems;

	Animator animMenu ;

 
	void Start () {
//		yield return new WaitForSeconds(0.3f);
//		tools = ReturnRoomTools(Gameplay.roomNo);
//        Debug.Log(tools.Length);
        tools=new ToolType[5];

        int index=0;

		cleaningToolsItems = new int[5];
        foreach(Transform t in transform.GetChild(0))
        {
            if (t.GetComponent<CleaningTool>() != null){
                tools[index++] = t.GetComponent<CleaningTool>().toolType;
                t.GetComponent<CleaningTool>().StartRotation = transform.Find("AnimationHolder/Room" + Gameplay.roomNo.ToString() + "Positions/T" + t.GetComponent<CleaningTool>().ToolNo.ToString()).rotation;
                if(t.GetComponent<CleaningTool>().ToolNo == TutorialCleanClassRoom.Instance.phase2ATool) TutorialCleanClassRoom.Instance.Phase2StartPosition = t.transform;
                if(t.GetComponent<CleaningTool>().ToolNo ==  TutorialCleanClassRoom.Instance.phase2BTool) TutorialCleanClassRoom.Instance.Phase2bStartPosition = t.transform;
            }
        }
//		for(int toolNO = 1; toolNO<= tools.Length; toolNO++)
//		{
// 
//			RectTransform toolInst = (RectTransform) GameObject.Instantiate( Resources.Load<RectTransform>( "CleaningTools/"+ ReturnToolPrefabName(tools[toolNO-1]) ));
//			CleaningTool ct = toolInst.GetComponent<CleaningTool>();
//            Debug.Log(ct);
//			ct.toolType =tools[toolNO-1];
//			ct.ToolNo = toolNO;
// 
//			Transform trPos = transform.Find("AnimationHolder/Room"+Gameplay.roomNo.ToString()+"Positions/T"+toolNO.ToString());
// 
//			toolInst.transform.position = trPos.position;
//			ct.AnimationChild =  toolInst.GetChild(0).GetChild(0).GetComponent<Animator>();
//		 	
//			ct.StartRotation =  trPos.rotation;
////	VRATI	
//			ct.AnimationChild.transform.parent.rotation =  trPos.rotation;
//		 
//			toolInst.SetParent(transform.Find("AnimationHolder"));
//			toolInst.localScale = Vector3.one;
//			//toolInst.rotation = Quaternion.identity;
//			toolInst.name =  ReturnToolPrefabName(tools[toolNO-1] ) ;
//
            
//            if(toolNO ==  TutorialCleanClassRoom.Instance.phase2BTool) TutorialCleanClassRoom.Instance.Phase2bStartPosition = toolInst.transform;
//			 
//		}
//
		for(int toolNOb = 1; toolNOb<= 5;toolNOb++)
		{
			cleaningToolsItems[(toolNOb-1)] += GameObject.FindGameObjectsWithTag("T"+toolNOb.ToString()).Length;	 
		}
//
//
//		animMenu = transform.GetChild(0).GetComponent<Animator>();
//		animMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(-182,0);
//		gameObject.SetActive(false);
	}

	public void ObjectCleaned(int toolNO)
	{
		cleaningToolsItems[toolNO-1] --;
		if(cleaningToolsItems[toolNO-1]  == 0) 
		{
			GameObject.Find( "LeftMenuCleaning/AnimationHolder/"+ReturnToolPrefabName(tools[toolNO-1] ) ).GetComponent<CleaningTool>().AllCleaned();
			 
		}
	}


	public void ShowMenu()
	{
		if(animMenu==null) animMenu = transform.GetChild(0).GetComponent<Animator>();
		animMenu.SetBool("Show",true);
		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ShowMenu);
	}

	public void HideMenu()
	{
		if(animMenu==null) animMenu = transform.GetChild(0).GetComponent<Animator>();
		animMenu.SetBool("Show",false);
		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.MenuHide);
	}

	public bool IsCleaningFinished()
	{
		for(int i = 0; i<cleaningToolsItems.Length;i++)
		{
			if(cleaningToolsItems[i] > 0) 	return false;
		}
		return true;
	}

	public static string ReturnToolPrefabName(ToolType t)
	{
		switch(t)
		{
		case ToolType.broom: return "Broom";  
		case ToolType.mop: return "Mop";  
		case ToolType.sponge: return "Sponge"; 
		case ToolType.roller_brush: return "RollerBrush"; 
		case ToolType.duster: return "Duster"; 
		case ToolType.brush: return "Brush";
		case ToolType.fix_floor: return "FixFloor";  
		case ToolType.watering_can: return "WateringCan";  
		case ToolType.rake: return "Rake";  
		case ToolType.garden_shears: return "GardenShears";  
		case ToolType.lawn_mower: return "LawnMower";  
		case ToolType.glass_spray: return "GlassSpray";
		case ToolType.cloth: return "Cloth";  
		case ToolType.crumbs_cleaner: return "CrumbsCleaner";  
		}
		return "";
	}


	public static ToolType[] ReturnRoomTools(int room)
	{
		ToolType[] tools  = new ToolType[5];
		tools[0] = ToolType.none;
		tools[1] = ToolType.none;
		tools[2] = ToolType.none;
		tools[3] = ToolType.none;
		tools[4] = ToolType.none;


		if(room == 1)
		{
			tools  = new ToolType[5];

			tools[0] = ToolType.sponge;
			tools[1] = ToolType.roller_brush;
			tools[2] = ToolType.duster;
			tools[3] = ToolType.mop;
			tools[4] = ToolType.fix_floor;

		}

		if(room == 2)
		{
			tools  = new ToolType[4];

			tools[0] = ToolType.watering_can;
			tools[1] = ToolType.garden_shears;
			tools[2] = ToolType.rake;
			tools[3] = ToolType.lawn_mower;
		}

		if(room == 3)
		{
			tools  = new ToolType[5];
 
			tools[0] = ToolType.glass_spray;
			tools[1] = ToolType.cloth;
			tools[2] = ToolType.duster;
			tools[3] = ToolType.mop;
			tools[4] = ToolType.broom;
		}


		if(room == 4)
		{
			tools  = new ToolType[5];

			tools[0] = ToolType.sponge;
			tools[1] = ToolType.crumbs_cleaner;
			tools[2] = ToolType.glass_spray;
			tools[3] = ToolType.cloth;
			tools[4] = ToolType.mop;

		}



		if(room == 5)
		{
			tools  = new ToolType[4];

			tools[0] = ToolType.duster;
			tools[1] = ToolType.brush;
			tools[2] = ToolType.mop;
			tools[3] = ToolType.sponge;
		}

		if(room == 6)
		{
			tools  = new ToolType[4];

			tools[0] = ToolType.duster;
			tools[1] = ToolType.roller_brush;
			tools[2] = ToolType.broom;
			tools[3] = ToolType.sponge;
		}

		 
		return tools;
	}
}

public enum ToolType
{
	broom,
	mop,
	sponge,
	roller_brush,
	duster,
	cloth,
	brush,
	fix_floor,
	watering_can,
	garden_shears,
	rake,
	lawn_mower,
	glass_spray,
	crumbs_cleaner,
	none

}
