using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Missions : MonoBehaviour {

	public Text task1,task2,task3,coinsCountText;
	public GameObject mainMenuParent,MissionParent;
	public GameObject task1_Tickmark,task2_Tickmark,task3_Tickmark;
	public static Missions Static;


	DateTime time,updatedtime;

	void Start () {
		Static = this;

		//		PlayerPrefs.SetInt ("MissionCoinsCount",10);
//		PlayerPrefs.SetInt ("MissionswipeLeftCount", 10);
//		PlayerPrefs.SetInt ("MissionswipeRightCount", 10);
//
//		PlayerPrefs.SetInt ("MissionMagnetPowerCount",10);
//		PlayerPrefs.SetInt ("MissionFlyPowerCount",10);
//		PlayerPrefs.SetInt ("Mission2XPowerCount",10);
//		PlayerPrefs.SetInt ("MissionJumpPowerCount",10);
//		PlayerPrefs.SetInt ("MissionRoll/SlideCount",10);
//		PlayerPrefs.SetInt ("MissionDestroyBarrelCount",10);
//		PlayerPrefs.SetInt ("MissionDestroyPotsCount",10);

		if (PlayerPrefs.GetInt ("startDailes", 0) <= 0) {
			PlayerPrefs.SetInt ("MissionCoinsCount", 3000);
			PlayerPrefs.SetInt ("MissionMagnetPowerCount", 20);
			PlayerPrefs.SetInt ("MissionJumpCount",100);
			PlayerPrefs.SetInt ("Mission",PlayerPrefs.GetInt ("Mission", 0)+ 1);
			PlayerPrefs.SetInt("startDailes", PlayerPrefs.GetInt ("startDailes", 0)+1);
			}

		task1_Tickmark.SetActive (false);
		updatedtime = DateTime.Now;

		if (PlayerPrefs.GetInt ("Mission", 0) == 1) {
			task1.text = "Collect 3000 coins ,Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
			task2.text =  "Collect  20 Magnet, Left " + PlayerPrefs.GetInt ("MissionMagnetPowerCount");
			task3.text =  "Jump 200 times, Left "+ PlayerPrefs.GetInt ("MissionJumpCount");
		} 
		if (PlayerPrefs.GetInt ("Mission", 0) == 2) {
			task1.text =  "Collect 20 Jet Pack , Left "+ PlayerPrefs.GetInt ("MissionFlyPowerCount");
			task2.text =  "Collect 30 Magnet " +PlayerPrefs.GetInt ("MissionMagnetPowerCount",30);
			task3.text = "Collect 5000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount"); 
		} 
		if (PlayerPrefs.GetInt ("Mission", 0) == 3) {
			task1.text = "Collect 7000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
			task2.text =  "Collect 30 Jet Pack Powers , Left " + PlayerPrefs.GetInt ("MissionFlyPowerCount");
			task3.text =  "Jump 300 times, Left "+ PlayerPrefs.GetInt ("MissionJumpCount", 0);
		} 
		if (PlayerPrefs.GetInt ("Mission", 0) == 4) {
			task1.text =  "Collect 20 Magnet , Left "+ PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0);
			task2.text =  "Destroy 20 Barrels, Left " + PlayerPrefs.GetInt ("MissionDestroyBarrelCount",0);
			task3.text = "Collect 9000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
		} 
		if (PlayerPrefs.GetInt ("Mission", 0) == 5) {
			task1.text = "Collect 40 Magnet Powers, Left " + PlayerPrefs.GetInt ("MissionMagnetPowerCount");
			task2.text =  "Collect 20 Jump Powers, Left "+ PlayerPrefs.GetInt ("MissionJumpPowerCount", 0);
			task3.text =  "Collect 60 2X Powers, Left " + PlayerPrefs.GetInt ("Mission2XPowerCount",0);
		} 
		if (PlayerPrefs.GetInt ("Mission", 0) == 6) {
			task1.text = "Collect 12000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
			task2.text =  "Roll/Slide 20 times, Left "+ PlayerPrefs.GetInt ("MissionRoll/SlideCount", 0);
			task3.text =  "Collect 40 Jump Powers,Left " + PlayerPrefs.GetInt ("MissionJumpPowerCount",0);
		} 
		if (PlayerPrefs.GetInt ("Mission", 0) == 7) {
			task2.text =  "Collect  20 Magnet, Left " + PlayerPrefs.GetInt ("MissionMagnetPowerCount");
			task2.text =  "Collect 60 Jet Pack , Left "+ PlayerPrefs.GetInt ("MissionFlyPowerCount", 0);
			task3.text =  "Collect 80 2X Powers,Left " + PlayerPrefs.GetInt ("Mission2XPowerCount",0);
		} 

		if (PlayerPrefs.GetInt ("Mission", 0) == 8) {
			task1.text = "Roll/Slide 60 times, Left " + PlayerPrefs.GetInt ("MissionRoll/SlideCount");
			task2.text =  "Destroy 80 Barrels, Left "+ PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0);
			task3.text =  "Destroy 100 Pots,Left " + PlayerPrefs.GetInt ("MissionDestroyPotsCount",0);
		} 

		if (PlayerPrefs.GetInt ("Mission", 0) == 9) {
			task1.text =  "Collect 20 Jet Pack, Left "+ PlayerPrefs.GetInt ("MissionFlyPowerCount");
			task2.text =  "Collect 30 Magnet " +PlayerPrefs.GetInt ("MissionMagnetPowerCount",30);
			task3.text =  "Destroy 200 Pots, Left " + PlayerPrefs.GetInt ("MissionDestroyPotsCount",0);
		} 
		if (PlayerPrefs.GetInt ("Mission", 0) == 10) {
			task1.text = "Collect 20000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
			task2.text =  "Destroy  120 Barrels, Left "+ PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0);
			task3.text =  "Destroy 400 Pots, Left " + PlayerPrefs.GetInt ("MissionDestroyPotsCount",0);
		} 

		// mission Two : coins Swipe left , Swipe Right
	
		// mission three : coins swipe right ,  jump

		// mission four : Destroy barrels , MagnetPower , Coins

		// mission Five : Jump power , MagnetPower , 2Xpower

		// mission six : roll/Slide ,Coins , Jump power

		// misssion Seven : Swipe Left , Fly power , 2XPower 

		// misssion Seven : roll/Slide , Barrels , pots 
	
		// misssion Seven :  pots , Swipe left ,swipe Right 

		// misssion Seven : roll/Slide , Coins , pots 

	
	}

	public void OnButtonClick (string ButtonName)
	{
		switch (ButtonName) {
		case "Close":
			SoundController.Static.playSoundFromName("Click");
			MissionParent.SetActive(false);
			mainMenuParent.SetActive(true);

			break;
		}
	}

	public bool left;
	void Update () {


//		if(Input.GetKeyDown(KeyCode.B))
//		{
//			PlayerPrefs.SetInt ("MissionCoinsCount",PlayerPrefs.GetInt ("MissionCoinsCount")-3000);
//			PlayerPrefs.SetInt ("MissionswipeLeftCount", PlayerPrefs.GetInt ("MissionswipeLeftCount")-2);
//			PlayerPrefs.SetInt ("MissionJumpCount",PlayerPrefs.GetInt ("MissionJumpCount")-100);
//		}
//		if(Input.GetKeyDown(KeyCode.C))
//		{
//			PlayerPrefs.SetInt ("MissionswipeLeftCount", PlayerPrefs.GetInt ("MissionswipeLeftCount")-200);
//		}



		// mission one : coins ,Swipe left , jump ...............................

		if (PlayerPrefs.GetInt ("Mission", 0) == 1) {

			//Debug.Log(PlayerPrefs.GetInt ("MissionCoinsCount"));
			if( PlayerPrefs.GetInt ("MissionCoinsCount", 0)>0)
			{
				task1.text = "Collect 3000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
				PlayerPrefs.SetInt("CollectCoins",1);

			}else{

				task1_Tickmark.SetActive(true);
				task1.text = "Collect 3000 coins"; 
			}
		
			if(PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)>0)
			{
				task2.text =  "Collect  20 Magnet, Left " + PlayerPrefs.GetInt ("MissionMagnetPowerCount");
				PlayerPrefs.SetInt("LeftSwipe",1);
			}
			else{

				Debug.Log("Swipe Left at missions");
				task2.text= "Collect 20 Magnet";
				task2_Tickmark.SetActive(true);

			}

			if(PlayerPrefs.GetInt ("MissionJumpCount", 0)>0){
				task3.text =  "Jump 200 times, Left "+ PlayerPrefs.GetInt ("MissionJumpCount", 0);
				PlayerPrefs.SetInt("JumpCount",1);
			}
			else{
				task3.text ="Jump 200 times";
				task3_Tickmark.SetActive(true);
			}

			if(PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)<=0 && PlayerPrefs.GetInt ("MissionCoinsCount", 0)<=0 && PlayerPrefs.GetInt ("MissionJumpCount", 0)<=0 )
			{
				PlayerPrefs.SetInt ("MissionCoinsCount",5000);
				PlayerPrefs.SetInt ("MissionMagnetPowerCount",30);
				PlayerPrefs.SetInt ("MissionFlyPowerCount",20);
				task1.text = "swipe right 200 times ";
				task2.text = "Collect  20 Magnet ";
				task3.text = "Collect 5000 coins";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
				PlayerPrefs.SetInt("AddMissionCoins",100);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
			}

		}
		//.....................................................

		// Mission Two : coins, Swipe left , Swipe Right .................

		if (PlayerPrefs.GetInt ("Mission", 0) == 2) {


			if(PlayerPrefs.GetInt ("MissionFlyPowerCount", 0)>0){

				task1.text =  "Collect 20 Jet Pack, Left "+ PlayerPrefs.GetInt ("MissionFlyPowerCount");
				PlayerPrefs.SetInt("RightSwipe",1);
			}
			else{
				task1.text ="Collect 20 Jet Pack";
				task1_Tickmark.SetActive(true);
			}

			if(PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)>0)
			{
				task2.text =  "Collect 30 Magnet " +PlayerPrefs.GetInt ("MissionMagnetPowerCount",30);
				PlayerPrefs.SetInt("LeftSwipe",1);
			}
			else{
				task2.text= "Collect 30 Magnet";
				task2_Tickmark.SetActive(true);
			}

			if( PlayerPrefs.GetInt ("MissionCoinsCount", 0)>0)
			{
				task3.text = "Collect 5000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount"); 
				PlayerPrefs.SetInt("CollectCoins",1);
			}else{
				task3_Tickmark.SetActive(true);
				task3.text = "Collect 5000 coins"; 
			}

			
			if(PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)<0 && PlayerPrefs.GetInt ("MissionCoinsCount", 0)<0 && PlayerPrefs.GetInt ("MissionFlyPowerCount", 0)<0 )
			{
				PlayerPrefs.SetInt("AddMissionCoins",500);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
				PlayerPrefs.SetInt ("MissionCoinsCount",7000);
				PlayerPrefs.SetInt ("MissionJumpCount", 300);
				PlayerPrefs.SetInt ("MissionFlyPowerCount", 30);
				task1.text = "Collect 7000 coins ";
				task2.text = "Swipe Right 400 times ";
				task3.text = "Jump 300 times";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
				
			}
		}
		//..............................................................

		// mission three : coins, swipe right ,  jump

		if (PlayerPrefs.GetInt ("Mission", 0) == 3) {
			

			if( PlayerPrefs.GetInt ("MissionCoinsCount", 0)>0)
			{
				task1.text = "Collect 7000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
				PlayerPrefs.SetInt("CollectCoins",1);
				
			}else{
				task1_Tickmark.SetActive(true);
				task1.text = "Collect 7000 coins"; 
			}
			
			if(PlayerPrefs.GetInt ("MissionFlyPowerCount", 0)>0)
			{
				task2.text =  "Collect 30 Jet Pack, Left " + PlayerPrefs.GetInt ("MissionFlyPowerCount");
				PlayerPrefs.SetInt("LeftSwipe",1);
			}
			else{
				task2.text= "Collect 30 Jet Pack ";
				task2_Tickmark.SetActive(true);
				
			}
			
			if(PlayerPrefs.GetInt ("MissionJumpCount", 0)>0){
				task3.text =  "Jump 300 times, Left "+ PlayerPrefs.GetInt ("MissionJumpCount", 0);
				PlayerPrefs.SetInt("JumpCount",1);
			}
			else{
				task3.text ="Jump 300 times";
				task3_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionFlyPowerCount", 0)<=0 && PlayerPrefs.GetInt ("MissionCoinsCount", 0)<=0 && PlayerPrefs.GetInt ("MissionJumpCount", 0)<=0 )
			{
				PlayerPrefs.SetInt("AddMissionCoins",1000);
				PlayerPrefs.SetInt ("MissionCoinsCount",9000);
				PlayerPrefs.SetInt ("MissionMagnetPowerCount",20);
				PlayerPrefs.SetInt ("MissionDestroyBarrelCount",20);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
				task1.text = "Collect MagnetPower 20 ";
				task2.text = "Destroy 20 Barrels";
				task3.text = "Collect Coins 9000";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
			}
			
		}
		//...............................................................
	
	
		// mission four : Destroy barrels , MagnetPower , Coins
		if (PlayerPrefs.GetInt ("Mission", 0) == 4) {
			

			if(PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)>0){
				task1.text =  "Collect 20 Magnet , Left "+ PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0);
				PlayerPrefs.SetInt("MagnetPower",1);
			}
			else{
				task1.text ="Collect 20 Magnet";
				task1_Tickmark.SetActive(true);
			}

			
			if(PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0)>0)
			{
				task2.text =  "Destroy 20 Barrels, Left " + PlayerPrefs.GetInt ("MissionDestroyBarrelCount",0);
				PlayerPrefs.SetInt("Barrel",1);
			}
			else{
				task2.text= "Destroy 20 Barrels";
				task2_Tickmark.SetActive(true);
				
			}
		

			if( PlayerPrefs.GetInt ("MissionCoinsCount", 0)>0)
			{
				task3.text = "Collect 9000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
				PlayerPrefs.SetInt("CollectCoins",1);
				
			}else{
				task3_Tickmark.SetActive(true);
				task3.text = "Collect 9000 coins"; 
			}
			
			if(PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0)<=0 && PlayerPrefs.GetInt ("MissionCoinsCount", 0)<=0 && PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)<=0 )
			{
				PlayerPrefs.SetInt("AddMissionCoins",1500);
				PlayerPrefs.SetInt ("MissionMagnetPowerCount",40);
				PlayerPrefs.SetInt ("Mission2XPowerCount",60);
				PlayerPrefs.SetInt ("MissionJumpPowerCount",20);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
				task1.text = "Collect MagnetPower 40 ";
				task2.text = "Collect Jump Power 20 ";
				task3.text = "Collect 2X Power  60";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
			}
			
		}

		// mission Five : Jump power , MagnetPower , 2Xpower

		if (PlayerPrefs.GetInt ("Mission", 0) == 5) {

		
			if( PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)>0)
			{
				task1.text = "Collect 40 Magnet, Left " + PlayerPrefs.GetInt ("MissionMagnetPowerCount");
				PlayerPrefs.SetInt("MagnetPower",1);
				
			}else{
				task1_Tickmark.SetActive(true);
				task1.text = "Collect 40 Magnet "; 
			}

			if(PlayerPrefs.GetInt ("MissionJumpPowerCount", 0)>0){
				task2.text =  "Collect 20 Jump Powers, Left "+ PlayerPrefs.GetInt ("MissionJumpPowerCount", 0);
				PlayerPrefs.SetInt("JumpPower",1);
			}
			else{
				task2.text ="Collect 20 Jump Powers";
				task2_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("Mission2XPowerCount", 0)>0)
			{
				task3.text =  "Collect 60 2X Powers, Left " + PlayerPrefs.GetInt ("Mission2XPowerCount",0);
				PlayerPrefs.SetInt("2XPower",1);
			}
			else{
				task3.text= "Collect 60 2X Powers";
				task3_Tickmark.SetActive(true);
			}

			if(PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)<=0 && PlayerPrefs.GetInt ("Mission2XPowerCount", 0)<=0 && PlayerPrefs.GetInt ("MissionJumpPowerCount", 0)<=0 )
			{
				PlayerPrefs.SetInt("AddMissionCoins",2000);
				PlayerPrefs.SetInt ("MissionCoinsCount",12000);
				PlayerPrefs.SetInt ("MissionJumpPowerCount",40);
				PlayerPrefs.SetInt ("MissionRoll/SlideCount",20);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
				task1.text = "Collect 12000 coins ";
				task2.text = "Roll/Slide 20 times ";
				task3.text = "Collect Jump Power 40";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
			}
			
		}

		// mission Five : coins , roll/slide, jump Power

		if (PlayerPrefs.GetInt ("Mission", 0) == 6) {
			

			if( PlayerPrefs.GetInt ("MissionCoinsCount", 0)>0)
			{
				task1.text = "Collect 12000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
				PlayerPrefs.SetInt("CollectCoins",1);
				
			}else{
				task1_Tickmark.SetActive(true);
				task1.text = "Collect 12000 coins"; 
			}
			
			if(PlayerPrefs.GetInt ("MissionRoll/SlideCount", 0)>0){
				task2.text =  "Roll/Slide 20 times, Left "+ PlayerPrefs.GetInt ("MissionRoll/SlideCount", 0);
				PlayerPrefs.SetInt("Roll/Slide",1);
			}
			else{
				task2.text ="Roll/Slide 20 times";
				task2_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionJumpPowerCount", 0)>0)
			{
				task3.text =  "Collect 40 Jump,Left " + PlayerPrefs.GetInt ("MissionJumpPowerCount",0);
				PlayerPrefs.SetInt("JumpPower",1);
			}
			else{
				task3.text= "Collect 40 Jump ";
				task3_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionCoinsCount", 0)<=0 && PlayerPrefs.GetInt ("MissionJumpPowerCount", 0)<=0 && PlayerPrefs.GetInt ("MissionRoll/SlideCount", 0)<=0 )
			{	
				PlayerPrefs.SetInt("AddMissionCoins",2500);
				PlayerPrefs.SetInt ("MissionMagnetPowerCount",40);
				PlayerPrefs.SetInt ("MissionFlyPowerCount",60);
				PlayerPrefs.SetInt ("Mission2XPowerCount",80);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
				task1.text = "Collect Fly power 60 ";
				task2.text = "Swipe left 6000 times ";
				task3.text = "Collect 2X Power  80";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
			}
			
		}

		// mission Five : swipe left, fly power,2Xpower
		
		if (PlayerPrefs.GetInt ("Mission", 0) == 7) {
			

			if( PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)>0)
			{
				task2.text =  "Collect  20 Magnet, Left " + PlayerPrefs.GetInt ("MissionMagnetPowerCount");
				PlayerPrefs.SetInt("LeftSwipe",1);
				
			}else{
				task1_Tickmark.SetActive(true);
				task2.text = "Collect  20 Magnet"; 
			}
			
			if(PlayerPrefs.GetInt ("MissionFlyPowerCount", 0)>0){
				task2.text =  "Collect 60 Jet Pack, Left "+ PlayerPrefs.GetInt ("MissionFlyPowerCount", 0);
				PlayerPrefs.SetInt("FlyPower",1);
			}
			else{
				task2.text ="Collect 60 Jet Pack";
				task2_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("Mission2XPowerCount", 0)>0)
			{
				task3.text =  "Collect 80 2X Powers,Left " + PlayerPrefs.GetInt ("Mission2XPowerCount",0);
				PlayerPrefs.SetInt("2XPower",1);

			}
			else{
				task3.text= "Collect 80 2X Powers";
				task3_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)<=0 && PlayerPrefs.GetInt ("Mission2XPowerCount", 0)<=0 && PlayerPrefs.GetInt ("MissionFlyPowerCount", 0)<=0 )
			{
				PlayerPrefs.SetInt("AddMissionCoins",3000);
				PlayerPrefs.SetInt ("MissionRoll/SlideCount",60);
				PlayerPrefs.SetInt ("MissionDestroyBarrelCount",80);
				PlayerPrefs.SetInt ("MissionDestroyPotsCount",100);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
				task1.text = "Roll/Slide 60 times  ";
				task2.text = "Destroy 80 Barrels";
				task3.text = "Destroy 100 Pots";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
			}
			
		}
		// mission Five : roll/slide,barrel,pots
		
		if (PlayerPrefs.GetInt ("Mission", 0) == 8) {
			

			if( PlayerPrefs.GetInt ("MissionRoll/SlideCount", 0)>0)
			{
				task1.text = "Roll/Slide 60 times, Left " + PlayerPrefs.GetInt ("MissionRoll/SlideCount");
				PlayerPrefs.SetInt("Roll/Slide",1);
				
			}else{
				task1_Tickmark.SetActive(true);
				task1.text = "Roll/Slide 60 times"; 
			}
			
			if(PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0)>0){
				task2.text =  "Destroy 80 Barrels, Left "+ PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0);
				PlayerPrefs.SetInt("Barrel",1);
			}
			else{
				task2.text ="Destroy 80 Barrels";
				task2_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionDestroyPotsCount", 0)>0)
			{
				task3.text =  "Destroy 80 Pots, Left " + PlayerPrefs.GetInt ("MissionDestroyPotsCount",0);
				PlayerPrefs.SetInt("Pots",1);
			}
			else{
				task3.text= "Destroy 80 Pots";
				task3_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionRoll/SlideCount", 0)<=0 && PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0)<=0 && PlayerPrefs.GetInt ("MissionDestroyPotsCount", 0)<=0 )
			{	
				PlayerPrefs.SetInt("AddMissionCoins",3500);
				PlayerPrefs.SetInt ("MissionFlyPowerCount",40);
				PlayerPrefs.SetInt ("MissionMagnetPowerCount",50);
				PlayerPrefs.SetInt ("MissionDestroyPotsCount",200);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
				task1.text = "swipe right 1000 times ";
				task2.text = "Swipe left 8000 times ";
				task3.text = "Destroy 200 Pots";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
			}
			
		}

		// mission Five : pots, swipe right, swipe left
		
		if (PlayerPrefs.GetInt ("Mission", 0) == 9) {
			

			if( PlayerPrefs.GetInt ("MissionFlyPowerCount", 0)>0)
			{
				task1.text =  "Collect 20 Jet Pack Powers, Left "+ PlayerPrefs.GetInt ("MissionFlyPowerCount");

				PlayerPrefs.SetInt("LeftSwipe",1);
				
			}else{
				task1_Tickmark.SetActive(true);
				task1.text = "Collect 20 Jet Pack Powers"; 
			}
			
			if(PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)>0){
				task2.text =  "Collect 30 MagnetPowers " +PlayerPrefs.GetInt ("MissionMagnetPowerCount",30);
				PlayerPrefs.SetInt("RightSwipe",1);
			}
			else{
				task2.text ="Collect 30 MagnetPowers";
				task1_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionDestroyPotsCount", 0)>0)
			{
				task3.text =  "Destroy 200 Pots, Left " + PlayerPrefs.GetInt ("MissionDestroyPotsCount",0);
				PlayerPrefs.SetInt("Pots",1);
			}
			else{
				task3.text= "Destroy 200 Pots";
				task3_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionFlyPowerCount", 0)<=0 && PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0)<=0 && PlayerPrefs.GetInt ("MissionDestroyPotsCount", 0)<=0 )
			{
				PlayerPrefs.SetInt("AddMissionCoins",4000);
				PlayerPrefs.SetInt ("MissionCoinsCount", 20000);
				PlayerPrefs.SetInt ("MissionDestroyBarrelCount", 120);
				PlayerPrefs.SetInt ("MissionDestroyPotsCount",400);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
				task1.text = "Collect 20000 coins ";
				task2.text = "Destroy 120 Barrels";
				task3.text = "Destroy 400 Pots";
				task1_Tickmark.SetActive(false);
				task2_Tickmark.SetActive(false);
				task3_Tickmark.SetActive(false);
			}
			
		}
		// mission Five : coins,barrel,pots
		
		if (PlayerPrefs.GetInt ("Mission", 0) == 10) {
			
		
			if( PlayerPrefs.GetInt ("MissionCoinsCount", 0)>0)
			{
				task1.text = "Collect 20000 coins, Left " + PlayerPrefs.GetInt ("MissionCoinsCount");
				PlayerPrefs.SetInt("CollectCoins",1);
				
			}else{
				task1_Tickmark.SetActive(true);
				task1.text = "Collect 20000 coins"; 
			}
			
			if(PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0)>0){
				task2.text =  "Destroy  120 Barrels, Left "+ PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0);
				PlayerPrefs.SetInt("Barrel",1);
			}
			else{
				task2.text ="Destroy  120 Barrels";
				task2_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionDestroyPotsCount", 0)>0)
			{
				task3.text =  "Destroy 400 Pots, Left " + PlayerPrefs.GetInt ("MissionDestroyPotsCount",0);
				PlayerPrefs.SetInt("Pots",1);
			}
			else{
				task3.text= "Destroy 400 Pots";
				task3_Tickmark.SetActive(true);
			}
			
			if(PlayerPrefs.GetInt ("MissionCoinsCount", 0)<=0 && PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0)<=0 && PlayerPrefs.GetInt ("MissionDestroyPotsCount", 0)<=0 )
			{	
				PlayerPrefs.SetInt("AddMissionCoins",5000);
				PlayerPrefs.SetInt ("Mission", PlayerPrefs.GetInt("Mission")+1);
			}
			
		}

	}

}
