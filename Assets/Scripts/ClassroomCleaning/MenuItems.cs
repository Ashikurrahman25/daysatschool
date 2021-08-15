using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MenuItems   {
	public static Dictionary <string,MenuItemData> mitd = new Dictionary<string, MenuItemData>();

	public static bool bAllItemsUnlocked = false;
	 
	public MenuItems()
	{
	 
		if( mitd.Count == 0)
		{
			
			//tapete soba1
			mitd.Add("M01_01",new MenuItemData {ButtonImgName = "M01_01", Atlas = "Tapete/07", Name = "13" ,  ImgeSize = new Vector2(1700,970), Locked = false });
			mitd.Add("M01_02",new MenuItemData {ButtonImgName = "M01_02", Atlas = "Tapete/08", Name = "15",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_03",new MenuItemData {ButtonImgName = "M01_03", Atlas = "Tapete/02", Name = "4",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_04",new MenuItemData {ButtonImgName = "M01_04", Atlas = "Tapete/07", Name = "14",   ImgeSize = new Vector2(1700,970), Locked = false });
			mitd.Add("M01_05",new MenuItemData {ButtonImgName = "M01_05", Atlas = "Tapete/06", Name = "11",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_06",new MenuItemData {ButtonImgName = "M01_06", Atlas = "Tapete/08", Name = "16",   ImgeSize = new Vector2(1700,970), Locked = false }); 
			mitd.Add("M01_07",new MenuItemData {ButtonImgName = "M01_07", Atlas = "Tapete/06", Name = "12" ,  ImgeSize = new Vector2(1700,970), Locked = false });
			mitd.Add("M01_08",new MenuItemData {ButtonImgName = "M01_08", Atlas = "Tapete/01", Name = "1",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_09",new MenuItemData {ButtonImgName = "M01_09", Atlas = "Tapete/02", Name = "3",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_10",new MenuItemData {ButtonImgName = "M01_10", Atlas = "Tapete/01", Name = "2",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_11",new MenuItemData {ButtonImgName = "M01_11", Atlas = "Tapete/03", Name = "5",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_12",new MenuItemData {ButtonImgName = "M01_12", Atlas = "Tapete/03", Name = "6",   ImgeSize = new Vector2(1700,970), Locked = false });
			mitd.Add("M01_13",new MenuItemData {ButtonImgName = "M01_13", Atlas = "Tapete/04", Name = "7",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_14",new MenuItemData {ButtonImgName = "M01_14", Atlas = "Tapete/04", Name = "8",   ImgeSize = new Vector2(1700,970), Locked = false });
			mitd.Add("M01_15",new MenuItemData {ButtonImgName = "M01_15", Atlas = "Tapete/05", Name = "9",   ImgeSize = new Vector2(1700,970), Locked = false });  
			mitd.Add("M01_16",new MenuItemData {ButtonImgName = "M01_16", Atlas = "Tapete/05", Name = "10",   ImgeSize = new Vector2(1700,970), Locked = false });

			//klupe
            mitd.Add("M02_01",new MenuItemData {ButtonImgName = "M02_01", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "1", ImgeSize = new Vector2(400,200), Locked = false});
            mitd.Add("M02_02",new MenuItemData {ButtonImgName = "M02_02", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "5", ImgeSize = new Vector2(400,200), Locked = false});
            mitd.Add("M02_03",new MenuItemData {ButtonImgName = "M02_03", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "9", ImgeSize = new Vector2(400,200), Locked = false});
            mitd.Add("M02_04",new MenuItemData {ButtonImgName = "M02_04", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "13", ImgeSize = new Vector2(400,200), Locked = false});
            mitd.Add("M02_05",new MenuItemData {ButtonImgName = "M02_05", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "17", ImgeSize = new Vector2(400,200), Locked = false});
            mitd.Add("M02_06",new MenuItemData {ButtonImgName = "M02_06", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "21", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_07",new MenuItemData {ButtonImgName = "M02_07", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "7", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_08",new MenuItemData {ButtonImgName = "M02_08", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "8", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_09",new MenuItemData {ButtonImgName = "M02_09", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "9", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_10",new MenuItemData {ButtonImgName = "M02_10", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "10", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_11",new MenuItemData {ButtonImgName = "M02_11", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "11", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_12",new MenuItemData {ButtonImgName = "M02_12", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "12", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_13",new MenuItemData {ButtonImgName = "M02_13", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "13", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_14",new MenuItemData {ButtonImgName = "M02_14", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "14", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_15",new MenuItemData {ButtonImgName = "M02_15", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "15", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_16",new MenuItemData {ButtonImgName = "M02_16", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "16", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_17",new MenuItemData {ButtonImgName = "M02_17", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "17", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_18",new MenuItemData {ButtonImgName = "M02_18", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "18", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_19",new MenuItemData {ButtonImgName = "M02_19", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "19", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_20",new MenuItemData {ButtonImgName = "M02_20", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "20", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_21",new MenuItemData {ButtonImgName = "M02_21", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "21", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_22",new MenuItemData {ButtonImgName = "M02_22", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "22", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_23",new MenuItemData {ButtonImgName = "M02_23", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "23", ImgeSize = new Vector2(400,200), Locked = false});
//            mitd.Add("M02_24",new MenuItemData {ButtonImgName = "M02_24", Atlas = "MenuItems/Content/SchoolBenchesTexture", Name = "24", ImgeSize = new Vector2(400,200), Locked = false});





 			//luster
			mitd.Add("M03_01",new MenuItemData {ButtonImgName = "M03_01", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "4", ImgeSize = new Vector2(240,300),  Locked = false});
			mitd.Add("M03_02",new MenuItemData {ButtonImgName = "M03_02", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "3", ImgeSize = new Vector2(260,350),  Locked = false});
			mitd.Add("M03_03",new MenuItemData {ButtonImgName = "M03_03", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "2", ImgeSize = new Vector2(160,260),  Locked = false});
			mitd.Add("M03_04",new MenuItemData {ButtonImgName = "M03_04", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "1", ImgeSize = new Vector2(160,260),  Locked = false});
			mitd.Add("M03_05",new MenuItemData {ButtonImgName = "M33_01", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_01", ImgeSize = new Vector2(142, 195),  Locked = false});
			mitd.Add("M03_06",new MenuItemData {ButtonImgName = "M33_02", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_02", ImgeSize = new Vector2(142, 195),  Locked = false});
			mitd.Add("M03_07",new MenuItemData {ButtonImgName = "M33_03", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_03", ImgeSize = new Vector2(142, 195),  Locked = false});
			mitd.Add("M03_08",new MenuItemData {ButtonImgName = "M33_04", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_04", ImgeSize = new Vector2(142, 195),  Locked = false});
			mitd.Add("M03_09",new MenuItemData {ButtonImgName = "M33_05", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_05", ImgeSize = new Vector2(142, 195),  Locked = false});
			 
			//ormar
			mitd.Add("M04_01",new MenuItemData {ButtonImgName = "M04_01", Atlas = "MenuItems/Content/KidsRoom", Name = "3", ImgeSize = new Vector2(260,400), Locked = false});
			mitd.Add("M04_02",new MenuItemData {ButtonImgName = "M04_02", Atlas = "MenuItems/Content/KidsRoom", Name = "4", ImgeSize = new Vector2(260,400), Locked = false});
			mitd.Add("M04_03",new MenuItemData {ButtonImgName = "M04_03", Atlas = "MenuItems/Content/KidsRoom", Name = "2", ImgeSize = new Vector2(260,400), Locked = false});
			mitd.Add("M04_04",new MenuItemData {ButtonImgName = "M04_04", Atlas = "MenuItems/Content/KidsRoom", Name = "1",ImgeSize = new Vector2(260,400), Locked = false});
	



 			//prozor
			mitd.Add("M05_01",new MenuItemData {ButtonImgName = "M16_01", Atlas = "MenuItems/Content/KitchenFurniture", Name = "2", ImgeSize = new Vector2(360, 300), Locked = false});
			mitd.Add("M05_02",new MenuItemData {ButtonImgName = "M16_02", Atlas = "MenuItems/Content/KitchenFurniture", Name = "1", ImgeSize = new Vector2(360, 300), Locked = false});
			mitd.Add("M05_03",new MenuItemData {ButtonImgName = "M16_03", Atlas = "MenuItems/Content/KitchenFurniture", Name = "3", ImgeSize = new Vector2(360, 300), Locked = false});
			mitd.Add("M05_04",new MenuItemData {ButtonImgName = "M16_04", Atlas = "MenuItems/Content/KidsRoom", Name = "13", ImgeSize = new Vector2(240, 300), Locked = false});
			mitd.Add("M05_05",new MenuItemData {ButtonImgName = "M16_05", Atlas = "MenuItems/Content/KitchenFurniture", Name = "4", ImgeSize = new Vector2(300, 300), Locked = false});
 
			//lampa pored kreveta
			mitd.Add("M06_01",new MenuItemData {ButtonImgName = "M06_01", Atlas = "MenuItems/Content/LivingRoom", Name = "3", ImgeSize = new Vector2(120, 380), Locked = false});
			mitd.Add("M06_02",new MenuItemData {ButtonImgName = "M06_02", Atlas = "MenuItems/Content/LivingRoom", Name = "1", ImgeSize = new Vector2(120, 380), Locked = false});
			mitd.Add("M06_03",new MenuItemData {ButtonImgName = "M06_03", Atlas = "MenuItems/Content/LivingRoom", Name = "2", ImgeSize = new Vector2(120, 380), Locked = false});
			mitd.Add("M06_04",new MenuItemData {ButtonImgName = "M06_04", Atlas = "MenuItems/Content/LivingRoom", Name = "4", ImgeSize = new Vector2(100, 380), Locked = false});
			mitd.Add("M06_05",new MenuItemData {ButtonImgName = "M06_05", Atlas = "MenuItems/Content/LivingRoom", Name = "5", ImgeSize = new Vector2(100, 380), Locked = false});
		 
			//----------------------------------------------------------------------------------------------------------------------------------------
			 
			//BAZEN
			mitd.Add("M07_01",new MenuItemData {ButtonImgName = "M07_01", Atlas = "MenuItems/Content/Garden", Name = "1", ImgeSize = new Vector2(500, 350), Locked = false});
			mitd.Add("M07_02",new MenuItemData {ButtonImgName = "M07_02", Atlas = "MenuItems/Content/Garden", Name = "2", ImgeSize = new Vector2(500, 350), Locked = false});
			mitd.Add("M07_03",new MenuItemData {ButtonImgName = "M07_03", Atlas = "MenuItems/Content/Garden", Name = "4", ImgeSize = new Vector2(500, 350), Locked = false});
			mitd.Add("M07_04",new MenuItemData {ButtonImgName = "M07_04", Atlas = "MenuItems/Content/Garden", Name = "3", ImgeSize = new Vector2(500, 350), Locked = false});
			mitd.Add("M07_05",new MenuItemData {ButtonImgName = "M07_05", Atlas = "MenuItems/Content/Garden", Name = "6", ImgeSize = new Vector2(500, 350), Locked = false});
			mitd.Add("M07_06",new MenuItemData {ButtonImgName = "M07_06", Atlas = "MenuItems/Content/Garden", Name = "5", ImgeSize = new Vector2(500, 350), Locked = false});
			mitd.Add("M07_07",new MenuItemData {ButtonImgName = "M07_07", Atlas = "MenuItems/Content/Garden", Name = "7", ImgeSize = new Vector2(500, 350), Locked = false});

			//igracke
			mitd.Add("M08_01",new MenuItemData {ButtonImgName = "M08_01", Atlas = "MenuItems/Content/Garden", Name = "P1_2", ImgeSize = new Vector2(300, 250), Locked = false});
			mitd.Add("M08_02",new MenuItemData {ButtonImgName = "M08_02", Atlas = "MenuItems/Content/Garden", Name = "P1_3", ImgeSize = new Vector2(300, 250), Locked = false});
			mitd.Add("M08_03",new MenuItemData {ButtonImgName = "M08_03", Atlas = "MenuItems/Content/Garden", Name = "P1_4", ImgeSize = new Vector2(350, 200), Locked = false});
			mitd.Add("M08_04",new MenuItemData {ButtonImgName = "M08_04", Atlas = "MenuItems/Content/Garden", Name = "P1_1", ImgeSize = new Vector2(250, 350), Locked = false});
			mitd.Add("M08_05",new MenuItemData {ButtonImgName = "M08_05", Atlas = "MenuItems/Content/Garden", Name = "P2_1", ImgeSize = new Vector2(100, 100), Locked = false});
			mitd.Add("M08_06",new MenuItemData {ButtonImgName = "M08_06", Atlas = "MenuItems/Content/Garden", Name = "P2_3", ImgeSize = new Vector2(130, 130), Locked = false});
			mitd.Add("M08_07",new MenuItemData {ButtonImgName = "M08_07", Atlas = "MenuItems/Content/Garden", Name = "P2_2", ImgeSize = new Vector2(200, 200), Locked = false});
			mitd.Add("M08_08",new MenuItemData {ButtonImgName = "M08_08", Atlas = "MenuItems/Content/Garden", Name = "P2_4", ImgeSize = new Vector2(130, 130), Locked = false});
			mitd.Add("M08_09",new MenuItemData {ButtonImgName = "M08_09", Atlas = "MenuItems/Content/Garden", Name = "P2_5", ImgeSize = new Vector2(230, 120), Locked = false});
			mitd.Add("M08_10",new MenuItemData {ButtonImgName = "M08_10", Atlas = "MenuItems/Content/Garden", Name = "P2_6", ImgeSize = new Vector2(230, 130), Locked = false});
			mitd.Add("M08_11",new MenuItemData {ButtonImgName = "M08_11", Atlas = "MenuItems/Content/Garden", Name = "P2_7", ImgeSize = new Vector2(230, 130), Locked = false});

			//cvece  
			mitd.Add("M09_01",new MenuItemData {ButtonImgName = "M09_01", Atlas = "Rooms/Room_2Bg", Name = "M09_01", ImgeSize = new Vector2(260, 150), Locked = false});
			mitd.Add("M09_02",new MenuItemData {ButtonImgName = "M09_02", Atlas = "Rooms/Room_2Bg", Name = "M09_02", ImgeSize = new Vector2(260, 150), Locked = false});
			mitd.Add("M09_03",new MenuItemData {ButtonImgName = "M09_03", Atlas = "Rooms/Room_2Bg", Name = "M09_03", ImgeSize = new Vector2(260, 150), Locked = false});
			mitd.Add("M09_04",new MenuItemData {ButtonImgName = "M09_04", Atlas = "Rooms/Room_2Bg", Name = "M09_04", ImgeSize = new Vector2(260, 150), Locked = false});
			mitd.Add("M09_05",new MenuItemData {ButtonImgName = "M09_05", Atlas = "Rooms/Room_2Bg", Name = "M09_05", ImgeSize = new Vector2(260, 150), Locked = false});

			//drvo
			mitd.Add("M10_01",new MenuItemData {ButtonImgName = "M10_01", Atlas = "MenuItems/Content/Garden", Name = "8", ImgeSize = new Vector2(570, 730), Locked = false});
			mitd.Add("M10_02",new MenuItemData {ButtonImgName = "M10_02", Atlas = "MenuItems/Content/Garden", Name = "10", ImgeSize = new Vector2(450, 550), Locked = false});
			mitd.Add("M10_03",new MenuItemData {ButtonImgName = "M10_03", Atlas = "MenuItems/Content/Garden", Name = "9", ImgeSize = new Vector2(570, 730), Locked = false});
			mitd.Add("M10_04",new MenuItemData {ButtonImgName = "M10_04", Atlas = "Rooms/Room_2Bg", Name = "tree", ImgeSize = new Vector2(570, 730), Locked = false});

			//ljubimci
			mitd.Add("M11_01",new MenuItemData {ButtonImgName = "M11_01", Atlas = "MenuItems/Content/Garden", Name = "12", ImgeSize = new Vector2(140, 200),  Locked = false});
			mitd.Add("M11_02",new MenuItemData {ButtonImgName = "M11_02", Atlas = "MenuItems/Content/Garden", Name = "11", ImgeSize = new Vector2(120, 180),  Locked = false});
			mitd.Add("M11_03",new MenuItemData {ButtonImgName = "M11_03", Atlas = "MenuItems/Content/Garden", Name = "13", ImgeSize = new Vector2(240, 200),  Locked = false});
			mitd.Add("M11_04",new MenuItemData {ButtonImgName = "M11_04", Atlas = "MenuItems/Content/Garden", Name = "14", ImgeSize = new Vector2(200, 180),  Locked = false});
			 







			// soba 3---------------------------------------------------------------------------------------------------------------------------------

			//kauc
			mitd.Add("M12_01",new MenuItemData {ButtonImgName = "M12_01", Atlas = "MenuItems/Content/LivingRoom", Name = "14", ImgeSize = new Vector2(450, 250),  Locked = false});
			mitd.Add("M12_02",new MenuItemData {ButtonImgName = "M12_02", Atlas = "MenuItems/Content/LivingRoom", Name = "13", ImgeSize = new Vector2(450, 250),  Locked = false});
			mitd.Add("M12_03",new MenuItemData {ButtonImgName = "M12_03", Atlas = "MenuItems/Content/LivingRoom", Name = "12", ImgeSize = new Vector2(450, 250),  Locked = false});
			mitd.Add("M12_04",new MenuItemData {ButtonImgName = "M12_04", Atlas = "MenuItems/Content/LivingRoom", Name = "15", ImgeSize = new Vector2(450, 250),  Locked = false});

			//TV
			mitd.Add("M13_01",new MenuItemData {ButtonImgName = "M13_01", Atlas = "MenuItems/Content/LivingRoom", Name = "6", ImgeSize = new Vector2(180, 200), Locked = false});
			mitd.Add("M13_02",new MenuItemData {ButtonImgName = "M13_02", Atlas = "MenuItems/Content/LivingRoom", Name = "7", ImgeSize = new Vector2(180, 200), Locked = false});
			mitd.Add("M13_03",new MenuItemData {ButtonImgName = "M13_03", Atlas = "MenuItems/Content/LivingRoom", Name = "8", ImgeSize = new Vector2(180, 200), Locked = false});
			mitd.Add("M13_04",new MenuItemData {ButtonImgName = "M13_04", Atlas = "MenuItems/Content/LivingRoom", Name = "9", ImgeSize = new Vector2(200, 150), Locked = false});
			mitd.Add("M13_05",new MenuItemData {ButtonImgName = "M13_05", Atlas = "MenuItems/Content/LivingRoom", Name = "10", ImgeSize = new Vector2(200, 150), Locked = false});
			mitd.Add("M13_06",new MenuItemData {ButtonImgName = "M13_06", Atlas = "MenuItems/Content/LivingRoom", Name = "11", ImgeSize = new Vector2(200, 150), Locked = false});

			//tapete soba3
			mitd.Add("M14_01",new MenuItemData {ButtonImgName = "M01_01", Atlas = "Tapete/07", Name = "13" ,  ImgeSize = new Vector2(1430,880), Locked = false });
			mitd.Add("M14_02",new MenuItemData {ButtonImgName = "M01_02", Atlas = "Tapete/08", Name = "15",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_03",new MenuItemData {ButtonImgName = "M01_03", Atlas = "Tapete/02", Name = "4",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_04",new MenuItemData {ButtonImgName = "M01_04", Atlas = "Tapete/07", Name = "14",   ImgeSize = new Vector2(1430,880), Locked = false });
			mitd.Add("M14_05",new MenuItemData {ButtonImgName = "M01_05", Atlas = "Tapete/06", Name = "11",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_06",new MenuItemData {ButtonImgName = "M01_06", Atlas = "Tapete/08", Name = "16",   ImgeSize = new Vector2(1430,880), Locked = false }); 
			mitd.Add("M14_07",new MenuItemData {ButtonImgName = "M01_07", Atlas = "Tapete/06", Name = "12" ,  ImgeSize = new Vector2(1430,880), Locked = false });
			mitd.Add("M14_08",new MenuItemData {ButtonImgName = "M01_08", Atlas = "Tapete/01", Name = "1",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_09",new MenuItemData {ButtonImgName = "M01_09", Atlas = "Tapete/02", Name = "3",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_10",new MenuItemData {ButtonImgName = "M01_10", Atlas = "Tapete/01", Name = "2",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_11",new MenuItemData {ButtonImgName = "M01_11", Atlas = "Tapete/03", Name = "5",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_12",new MenuItemData {ButtonImgName = "M01_12", Atlas = "Tapete/03", Name = "6",   ImgeSize = new Vector2(1430,880), Locked = false });
			mitd.Add("M14_13",new MenuItemData {ButtonImgName = "M01_13", Atlas = "Tapete/04", Name = "7",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_14",new MenuItemData {ButtonImgName = "M01_14", Atlas = "Tapete/04", Name = "8",   ImgeSize = new Vector2(1430,880), Locked = false });
			mitd.Add("M14_15",new MenuItemData {ButtonImgName = "M01_15", Atlas = "Tapete/05", Name = "9",   ImgeSize = new Vector2(1430,880), Locked = false });  
			mitd.Add("M14_16",new MenuItemData {ButtonImgName = "M01_16", Atlas = "Tapete/05", Name = "10",   ImgeSize = new Vector2(1430,880), Locked = false });

			//tepih
			mitd.Add("M29_01",new MenuItemData {ButtonImgName = "M29_01", Atlas = "MenuItems/Content/KidsRoom", Name = "10", ImgeSize = new Vector2(650, 130), Locked = false});
			mitd.Add("M29_02",new MenuItemData {ButtonImgName = "M29_02", Atlas = "MenuItems/Content/KidsRoom", Name = "11", ImgeSize = new Vector2(650, 130), Locked = false});
			mitd.Add("M29_03",new MenuItemData {ButtonImgName = "M29_03", Atlas = "MenuItems/Content/KidsRoom", Name = "12", ImgeSize = new Vector2(650, 130), Locked = false});
			mitd.Add("M29_04",new MenuItemData {ButtonImgName = "M29_04", Atlas = "MenuItems/Content/LivingRoom", Name = "21", ImgeSize = new Vector2(650, 130), Locked = false});
			mitd.Add("M29_05",new MenuItemData {ButtonImgName = "M29_05", Atlas = "MenuItems/Content/LivingRoom", Name = "23", ImgeSize = new Vector2(650, 130), Locked = false});
			mitd.Add("M29_06",new MenuItemData {ButtonImgName = "M29_06", Atlas = "MenuItems/Content/LivingRoom", Name = "22", ImgeSize = new Vector2(650, 130), Locked = false});
			mitd.Add("M29_07",new MenuItemData {ButtonImgName = "M29_07", Atlas = "MenuItems/Content/LivingRoom", Name = "24", ImgeSize = new Vector2(650, 130), Locked = false});

			//stocic
			mitd.Add("M30_01",new MenuItemData {ButtonImgName = "M30_01", Atlas = "MenuItems/Content/LivingRoom", Name = "18", ImgeSize = new Vector2(250, 130), Locked = false});
			mitd.Add("M30_02",new MenuItemData {ButtonImgName = "M30_02", Atlas = "MenuItems/Content/LivingRoom", Name = "17", ImgeSize = new Vector2(250, 130), Locked = false});
			mitd.Add("M30_03",new MenuItemData {ButtonImgName = "M30_03", Atlas = "MenuItems/Content/LivingRoom", Name = "19", ImgeSize = new Vector2(250, 130), Locked = false});
			mitd.Add("M30_04",new MenuItemData {ButtonImgName = "M30_04", Atlas = "MenuItems/Content/LivingRoom", Name = "20", ImgeSize = new Vector2(250, 170), Locked = false});
			mitd.Add("M30_05",new MenuItemData {ButtonImgName = "M30_05", Atlas = "MenuItems/Content/LivingRoom", Name = "16", ImgeSize = new Vector2(250, 130), Locked = false});


			//soba4---------------------------------------------------------------------------------------------------------------------------

			//prozor
			mitd.Add("M16_01",new MenuItemData {ButtonImgName = "M16_01", Atlas = "MenuItems/Content/KitchenFurniture", Name = "2", ImgeSize = new Vector2(270, 250), Locked = false});
			mitd.Add("M16_02",new MenuItemData {ButtonImgName = "M16_02", Atlas = "MenuItems/Content/KitchenFurniture", Name = "1", ImgeSize = new Vector2(270, 250), Locked = false});
			mitd.Add("M16_03",new MenuItemData {ButtonImgName = "M16_03", Atlas = "MenuItems/Content/KitchenFurniture", Name = "3", ImgeSize = new Vector2(270, 250), Locked = false});
			mitd.Add("M16_04",new MenuItemData {ButtonImgName = "M16_04", Atlas = "MenuItems/Content/KidsRoom", Name = "13", ImgeSize = new Vector2(230, 230), Locked = false});
			mitd.Add("M16_05",new MenuItemData {ButtonImgName = "M16_05", Atlas = "MenuItems/Content/KitchenFurniture", Name = "4", ImgeSize = new Vector2(270, 230), Locked = false});


			//viseci elementi
			mitd.Add("M17_01",new MenuItemData {ButtonImgName = "M17_01", Atlas = "MenuItems/Content/KitchenFurniture", Name = "12", ImgeSize = new Vector2(300, 180), Locked = false});
			mitd.Add("M17_02",new MenuItemData {ButtonImgName = "M17_02", Atlas = "MenuItems/Content/KitchenFurniture", Name = "15", ImgeSize = new Vector2(300, 180), Locked = false});
			mitd.Add("M17_03",new MenuItemData {ButtonImgName = "M17_03", Atlas = "MenuItems/Content/KitchenFurniture", Name = "14", ImgeSize = new Vector2(300, 180), Locked = false});
			mitd.Add("M17_04",new MenuItemData {ButtonImgName = "M17_04", Atlas = "MenuItems/Content/KitchenFurniture", Name = "13", ImgeSize = new Vector2(300, 180), Locked = false});

			//kuhinjski elnetni donji deo
			mitd.Add("M18_01",new MenuItemData {ButtonImgName = "M18_01", Atlas = "MenuItems/Content/KitchenFurniture", Name = "8" ,  ImgeSize = new Vector2(350,300), Locked = false });
			mitd.Add("M18_02",new MenuItemData {ButtonImgName = "M18_02", Atlas = "MenuItems/Content/KitchenFurniture", Name = "11",   ImgeSize = new Vector2(350,300), Locked = false });
			mitd.Add("M18_03",new MenuItemData {ButtonImgName = "M18_03", Atlas = "MenuItems/Content/KitchenFurniture", Name = "9",   ImgeSize = new Vector2(350,300), Locked = false });
			mitd.Add("M18_04",new MenuItemData {ButtonImgName = "M18_04", Atlas = "MenuItems/Content/KitchenFurniture", Name = "10",   ImgeSize = new Vector2(350,300), Locked = false });


			//sporet
			mitd.Add("M19_01",new MenuItemData {ButtonImgName = "M19_01", Atlas = "MenuItems/Content/KitchenFurniture", Name = "16", ImgeSize = new Vector2(190, 235),  Locked = false});
			mitd.Add("M19_02",new MenuItemData {ButtonImgName = "M19_02", Atlas = "MenuItems/Content/KitchenFurniture", Name = "18", ImgeSize = new Vector2(190, 235),  Locked = false});
			mitd.Add("M19_03",new MenuItemData {ButtonImgName = "M19_03", Atlas = "MenuItems/Content/KitchenFurniture", Name = "17", ImgeSize = new Vector2(1590, 235),  Locked = false});

			//frizider
			mitd.Add("M31_01",new MenuItemData {ButtonImgName = "M31_01", Atlas = "MenuItems/Content/KitchenFurniture", Name = "7", ImgeSize = new Vector2(195, 370),  Locked = false});
			mitd.Add("M31_02",new MenuItemData {ButtonImgName = "M31_02", Atlas = "MenuItems/Content/KitchenFurniture", Name = "5", ImgeSize = new Vector2(195, 370),  Locked = false});
			mitd.Add("M31_03",new MenuItemData {ButtonImgName = "M31_03", Atlas = "MenuItems/Content/KitchenFurniture", Name = "6", ImgeSize = new Vector2(195, 370),  Locked = false});
			 





			//soba 5---------------------------------------------------------------------------------------------------------------------------

			//ogledalo
			mitd.Add("M20_01",new MenuItemData {ButtonImgName = "M20_01", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "5", ImgeSize = new Vector2(220, 320),  Locked = false});
			mitd.Add("M20_02",new MenuItemData {ButtonImgName = "M20_02", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "9", ImgeSize = new Vector2(220, 320),  Locked = false});
			mitd.Add("M20_03",new MenuItemData {ButtonImgName = "M20_03", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "7", ImgeSize = new Vector2(220, 320),  Locked = false});
			mitd.Add("M20_04",new MenuItemData {ButtonImgName = "M20_04", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "8", ImgeSize = new Vector2(220, 320),  Locked = false});
			mitd.Add("M20_05",new MenuItemData {ButtonImgName = "M20_05", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "6", ImgeSize = new Vector2(220, 320),  Locked = false});
			mitd.Add("M20_06",new MenuItemData {ButtonImgName = "M20_06", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "10", ImgeSize = new Vector2(220, 320),  Locked = false});

			//kada
			mitd.Add("M21_01",new MenuItemData {ButtonImgName = "M21_01", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "11", ImgeSize = new Vector2(500, 350),  Locked = false});
			mitd.Add("M21_02",new MenuItemData {ButtonImgName = "M21_02", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "14", ImgeSize = new Vector2(500, 350),  Locked = false});
			mitd.Add("M21_03",new MenuItemData {ButtonImgName = "M21_03", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "13", ImgeSize = new Vector2(500, 350),  Locked = false});
			mitd.Add("M21_04",new MenuItemData {ButtonImgName = "M21_04", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "12", ImgeSize = new Vector2(500, 350),  Locked = false});
			mitd.Add("M21_05",new MenuItemData {ButtonImgName = "M21_05", Atlas = "MenuItems/Content/BathtubMirrorLamp", Name = "15", ImgeSize = new Vector2(500, 350),  Locked = false});
			 

			//lavabo i fioke
			mitd.Add("M22_01",new MenuItemData {ButtonImgName = "M22_01", Atlas = "MenuItems/Content/ToiletWashStand", Name = "9", ImgeSize = new Vector2(470, 400),  Locked = false});
			mitd.Add("M22_02",new MenuItemData {ButtonImgName = "M22_02", Atlas = "MenuItems/Content/ToiletWashStand", Name = "12", ImgeSize = new Vector2(470, 400),  Locked = false});
			mitd.Add("M22_03",new MenuItemData {ButtonImgName = "M22_03", Atlas = "MenuItems/Content/ToiletWashStand", Name = "11", ImgeSize = new Vector2(470, 400),  Locked = false});
			mitd.Add("M22_04",new MenuItemData {ButtonImgName = "M22_04", Atlas = "MenuItems/Content/ToiletWashStand", Name = "10", ImgeSize = new Vector2(470, 400),  Locked = false});
			mitd.Add("M22_05",new MenuItemData {ButtonImgName = "M22_05", Atlas = "MenuItems/Content/ToiletWashStand", Name = "8", ImgeSize = new Vector2(470, 400),  Locked = false});
			mitd.Add("M22_06",new MenuItemData {ButtonImgName = "M22_06", Atlas = "MenuItems/Content/ToiletWashStand", Name = "7", ImgeSize = new Vector2(470, 400),  Locked = false});


			//wc solja
			mitd.Add("M23_01",new MenuItemData {ButtonImgName = "M23_01", Atlas = "MenuItems/Content/ToiletWashStand", Name = "1", ImgeSize = new Vector2(350, 430),  Locked = false});
			mitd.Add("M23_02",new MenuItemData {ButtonImgName = "M23_02", Atlas = "MenuItems/Content/ToiletWashStand", Name = "3", ImgeSize = new Vector2(350, 430),  Locked = false});
			mitd.Add("M23_03",new MenuItemData {ButtonImgName = "M23_03", Atlas = "MenuItems/Content/ToiletWashStand", Name = "5", ImgeSize = new Vector2(350, 430),  Locked = false});
			mitd.Add("M23_04",new MenuItemData {ButtonImgName = "M23_04", Atlas = "MenuItems/Content/ToiletWashStand", Name = "2", ImgeSize = new Vector2(350, 430),  Locked = false});
			mitd.Add("M23_05",new MenuItemData {ButtonImgName = "M23_05", Atlas = "MenuItems/Content/ToiletWashStand", Name = "6", ImgeSize = new Vector2(350, 430),  Locked = false});
			mitd.Add("M23_06",new MenuItemData {ButtonImgName = "M23_06", Atlas = "MenuItems/Content/ToiletWashStand", Name = "4", ImgeSize = new Vector2(350, 430),  Locked = false});


			//soba 6  --------------------------------------------------------------------------------------------------------------------------------
		 

			//ves-masina
			mitd.Add("M25_01",new MenuItemData {ButtonImgName = "M25_01", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "1", ImgeSize = new Vector2(300, 300),  Locked = false});
			mitd.Add("M25_02",new MenuItemData {ButtonImgName = "M25_02", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "2", ImgeSize = new Vector2(300, 300),  Locked = false});
			mitd.Add("M25_03",new MenuItemData {ButtonImgName = "M25_03", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "3", ImgeSize = new Vector2(300, 300),  Locked = false});
			mitd.Add("M25_04",new MenuItemData {ButtonImgName = "M25_04", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "4", ImgeSize = new Vector2(300, 300),  Locked = false});

			//komoda
			mitd.Add("M27_01",new MenuItemData {ButtonImgName = "M27_01", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "11", ImgeSize = new Vector2(371, 291),  Locked = false});
			mitd.Add("M27_02",new MenuItemData {ButtonImgName = "M27_02", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "12", ImgeSize = new Vector2(371, 291),  Locked = false});
			mitd.Add("M27_03",new MenuItemData {ButtonImgName = "M27_03", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "13", ImgeSize = new Vector2(371, 291),  Locked = false});
			mitd.Add("M27_04",new MenuItemData {ButtonImgName = "M27_04", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "14", ImgeSize = new Vector2(371, 291),  Locked = false});
 
			//ogledalo
			mitd.Add("M28_01",new MenuItemData {ButtonImgName = "M28_01", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "10", ImgeSize = new Vector2(200, 200),  Locked = false});
			mitd.Add("M28_02",new MenuItemData {ButtonImgName = "M28_02", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "9", ImgeSize = new Vector2(200, 200),  Locked = false});
			mitd.Add("M28_03",new MenuItemData {ButtonImgName = "M28_03", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "8", ImgeSize = new Vector2(200, 200),  Locked = false});
			mitd.Add("M28_04",new MenuItemData {ButtonImgName = "M28_04", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "5", ImgeSize = new Vector2(200, 200),  Locked = false});
			mitd.Add("M28_05",new MenuItemData {ButtonImgName = "M28_05", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "7", ImgeSize = new Vector2(200, 200),  Locked = false});
			mitd.Add("M28_06",new MenuItemData {ButtonImgName = "M28_06", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "6", ImgeSize = new Vector2(200, 200),  Locked = false});

			//korpa za ves
			mitd.Add("M32_01",new MenuItemData {ButtonImgName = "M32_01", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M32_01", ImgeSize = new Vector2(260, 165),  Locked = false});
			mitd.Add("M32_02",new MenuItemData {ButtonImgName = "M32_02", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M32_02", ImgeSize = new Vector2(260, 165),  Locked = false});
			mitd.Add("M32_03",new MenuItemData {ButtonImgName = "M32_03", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M32_03", ImgeSize = new Vector2(260, 165),  Locked = false});
			mitd.Add("M32_04",new MenuItemData {ButtonImgName = "M32_04", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M32_04", ImgeSize = new Vector2(260, 165),  Locked = false});
			mitd.Add("M32_05",new MenuItemData {ButtonImgName = "M32_05", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M32_05", ImgeSize = new Vector2(260, 165),  Locked = false});
		 
			//plafonjera
			mitd.Add("M33_01",new MenuItemData {ButtonImgName = "M33_01", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_01", ImgeSize = new Vector2(142, 195),  Locked = false});
			mitd.Add("M33_02",new MenuItemData {ButtonImgName = "M33_02", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_02", ImgeSize = new Vector2(142, 195),  Locked = false});
			mitd.Add("M33_03",new MenuItemData {ButtonImgName = "M33_03", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_03", ImgeSize = new Vector2(142, 195),  Locked = false});
			mitd.Add("M33_04",new MenuItemData {ButtonImgName = "M33_04", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_04", ImgeSize = new Vector2(142, 195),  Locked = false});
			mitd.Add("M33_05",new MenuItemData {ButtonImgName = "M33_05", Atlas = "MenuItems/Content/WashingMachineMirrorCommode", Name = "M33_05", ImgeSize = new Vector2(142, 195),  Locked = false});
		}

		if(bAllItemsUnlocked && false)
		{
			foreach(KeyValuePair<string,MenuItemData>  kvp in mitd)
			{
				kvp.Value.Locked = false;
			}
		}
	}

	public  Dictionary <string,MenuItemData>  ReturmMenu(int menu)
	{
		Dictionary <string,MenuItemData> m = new Dictionary<string, MenuItemData>();
		string test = "M"+menu.ToString().PadLeft(2,'0')+"_";
		foreach(  KeyValuePair<string,MenuItemData>  kvp in mitd)
		{
			if( kvp.Key.StartsWith (test))
			{
				m.Add(kvp.Key,kvp.Value);
			}
		}
		return m;
	}
	/*
	public static string ReturnDollImage()
	{
		string ret = "";
		switch(DailyRewards.nagrada)
		{
		case 1:
			ret = "M01_01";
			break;
		case 2:
			ret = "M01_02";
			break;
		case 3:
			ret = "M01_03";
			break;
		case 4:
			ret = "M01_04";
			break;
		case 5:
			ret = "M01_05";
			break;
		case 6:
			ret = "M01_06";
			break;
	 

		}


		if(mitd.Count >0)
		{
			mitd["M01_02"].Locked = (DailyRewards.nagrada <2);
			mitd["M01_03"].Locked = (DailyRewards.nagrada <3);
			mitd["M01_04"].Locked = (DailyRewards.nagrada <4);
			mitd["M01_05"].Locked = (DailyRewards.nagrada <5);
			mitd["M01_06"].Locked = (DailyRewards.nagrada <6);
		 
		}
		return ret;
	}

	 */
}
public class MenuItemData
{
	public string ButtonImgName = "";
	public string  Atlas = "";
	public string Name = "";
	 
	public bool Locked = false;
	public Vector2 ImgeSize = new Vector2(20,20);
}


public enum RoomPosition
{
	Wall,
	WallNoMid,
	Floor,
	FloorNoMid,
	WallAndFloor,
	Ceiling,
	WallTile,
	FloorCarpet
}
 
