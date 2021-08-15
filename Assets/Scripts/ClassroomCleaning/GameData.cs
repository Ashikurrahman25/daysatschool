using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour {

	public static int TotalStars =50;
	public static int CollectedStars = 0;

	//public static bool bWatchVideoReady = false;
	//public static bool bWatchVideoStart = false;
 
	public static bool TestTutorial = false;
	public static string Unlocked = "dafE1A";
	public static int TutorialShown = 0;
	public static List<GameItemData> ItemsDataList = new List<GameItemData>();
	public static bool[] CheckedRooms = new bool[] { false,false,false,false,false,false};
	public static bool[] MiniGamePlayed = new bool[] { false,false,false,false,false,false}; 

	public static string sTestiranje = "";

	public static int WatchVideoCounter = 2;// 0;

	public static void UnlockRoom( int itemNo)
	{
		if(!Unlocked.Contains(  ItemsDataList[  itemNo ] .code))
	   {
			Unlocked +=ItemsDataList[  itemNo ] .code;
			PlayerPrefs.SetString("Data2",Unlocked);
		}
		SetUnloceked ();
 
 
	}


	 
	public static void TutorialOver()
	{
		TutorialShown = 1;
		PlayerPrefs.SetInt("TUTORIAL",1);
	}

	public static void Init()
	{
		//-----------------------------------------------------------------
# if UNITY_EDITOR
		if( true || PlayerPrefs.GetInt("GINKO_BILOBA",0) == 2) 
		{
			

			sTestiranje = "Test;"
			 // + "SpecialOffer;"
				+ "OverrideShopCall;"  
			//	+ "TestPopUpTransaction;"	
				+ "WatchVideo;"
			//	+ "FreeStar;";
				+ "InternetOff;"
			 ;

			Debug.Log("TESTIRANJE UKLJUCENO: " + sTestiranje);
		}
		//-----------------------------------------------------------------------
#endif
		if(ItemsDataList.Count>0) return;

		TutorialShown = PlayerPrefs.GetInt("TUTORIAL",0);
		 

		GetStarsFromPP();

		GetPurchasedItems();

		ItemsDataList.Add(new GameItemData(  "Room1", 0, "da" ));
		ItemsDataList.Add(new GameItemData(  "Room2", 0, "fE" ));
		ItemsDataList.Add(new GameItemData(  "Room3", 5, "1A" ));
		ItemsDataList.Add(new GameItemData(  "Room4", 15, "g2" ));
		ItemsDataList.Add(new GameItemData(  "Room5", 50, "tI" ));
		ItemsDataList.Add(new GameItemData(  "Room6", 100, "m0" ));
		 

		Unlocked = 	PlayerPrefs.GetString("Data2","dafE");
 
//		

		SetUnloceked();
	}

	public static void SetUnloceked()
	{

		for(int i =0; i <ItemsDataList.Count;i++)
		{
			ItemsDataList[i].unlocked = (    Unlocked.Contains( ItemsDataList[i].code ) || TotalStars>=	ItemsDataList[i].stars);
		}
	}



	public static void GetStarsFromPP()
	{
		string tmp = PlayerPrefs.GetString("Data1","7542");
		tmp= tmp.Replace("_","9");
		tmp= tmp.Replace("76q","8");
		tmp= tmp.Replace("nmFs","7");
		tmp= tmp.Replace("Tr;","6");
		tmp= tmp.Replace("^3","5");
		tmp= tmp.Replace("D","4");
		tmp= tmp.Replace("EE","3");
		tmp= tmp.Replace("g$","2");
		tmp= tmp.Replace("=0","1");
		tmp= tmp.Replace("Ase","0");
 
		int tmpStars = int.Parse(tmp);
		TotalStars = tmpStars - 7542;

		 
	}

 

	public  static void SetStarsToPP()
	{
		string tmp = (TotalStars+7542).ToString();

		tmp= tmp.Replace("0","Ase");
		tmp= tmp.Replace("1","=0");
		tmp= tmp.Replace("2","g$");
		tmp= tmp.Replace("3","EE");
		tmp= tmp.Replace("4","D");
		tmp= tmp.Replace("5","^3");
		tmp= tmp.Replace("6","Tr;");
		tmp= tmp.Replace("7","nmFs");
		tmp= tmp.Replace("8","76q");
		tmp= tmp.Replace("9","_");

		PlayerPrefs.SetString("Data1", tmp);

	 
		SetUnloceked();
	}


	public static void GetPurchasedItems()
	{
		string tmp = PlayerPrefs.GetString("Data3","22317");
		tmp= tmp.Replace("<","9");
		tmp= tmp.Replace("7>q","8");
		tmp= tmp.Replace("nmFs","7");
		tmp= tmp.Replace("Vy;","6");
		tmp= tmp.Replace("*2","5");
		tmp= tmp.Replace("H","4");
		tmp= tmp.Replace("JE","3");
		tmp= tmp.Replace("B#","2");
		tmp= tmp.Replace("+0","1");
		tmp= tmp.Replace("Kce","0");
		
		int tmpPurchased = int.Parse(tmp);
		int purchased = tmpPurchased - 22317;



	}

	public static void SetPurchasedItems()
	{

	}

 
	 
 
	public static void IncrementButtonCheckClickedCount()
	{

	}
	
	public static void IncrementTapOnRoomCount()
	{

	}

	 
	public static void IncrementButtonHomeClickedCount()
	{

	}



	public static void IncrementButtonRepeatClickedCount()
	{

	}
	 

 
}

public class GameItemData 
{
	public string name = "";
	public int stars = 0;
	public bool unlocked = false;
	public string code = "";

	public GameItemData( string name, int stars, string code)
	{
		this.name = name;
		this.stars = stars;
		this.code = code;
	}
	
} 

