//
//  PSMuscleDefine.cs
//  PSMuscle Editor
//
//  Copyright (c) 2014-2015 Pavo Studio.
//

using UnityEngine;
using System.Collections;

public class PSMuscleDefine {

	public static string[] rootProperty = {"RootT.x","RootT.y","RootT.z","RootQ.x","RootQ.y","RootQ.z","RootQ.w"};
	public static string[] rootT = {"RootT.x","RootT.y","RootT.z"};
	public static string[] rootQ = {"RootQ.x","RootQ.y","RootQ.z","RootQ.w"};
	
	// 0 body, 1 head, 2 Arm L <-> 4 R, 3 Fingers L <-> 5 R, 6 Leg L <-> 7 R 
	public static int[] mirrorMuscle = new int[]{0, 1, 4, 5, 2, 3, 7, 6};

	public static string[] muscleBodyGroup = 
	{
		"Body",
		"Head",
		"Left Arm",
		"Left Fingers",
		"Right Arm",
		"Right Fingers",
		"Left Leg",
		"Right Leg"
	};
	public static string[] muscleTypeGroup = 
	{
		"Open Close",
		"Left Right",
		"Roll Left Right",
		"In Out",
		"Roll In Out",
		"Left Finger Open Close",
		"Right Finger Open Close",
		"Left Finger In Out",
		"Right Finger In Out"
	};

	public static string[] muscleTransform = 
	{
		"Left Hand T",
		"Left Hand Q",
		"Right Hand T",
		"Right Hand Q",
		"Left Foot T",
		"Left Foot Q",
		"Right Foot T",
		"Right Foot Q"
	};


	// index of HumanTrait.MuscleName
	public static int[][] muscle = new int[][]
	{
		new int[]
		{
			0,
			1,
			2,
			3,
			4,
			5
		},
		new int[]
		{
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17
		},
		new int[]
		{
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42
		},
		new int[]
		{
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			62,
			63,
			64,
			65,
			66,
			67,
			68,
			69,
			70,
			71
		},
		new int[]
		{
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51
		},
		new int[]
		{
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			91
		},
		new int[]
		{
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25
		},
		new int[]
		{
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33
		}
	};
	
	public static int[][] masterMuscle = new int[][]
	{
		new int[]//Open Close
		{
			0,
			3,
			6,
			9,
			18,
			21,
			23,
			26,
			29,
			31,
			34,
			36,
			39,
			41,
			43,
			45,
			48,
			50
		},
		new int[]//Left Right
		{
			1,
			4,
			7,
			10
		},
		new int[]//Roll Left Right
		{
			2,
			5,
			8,
			11
		},
		new int[]//In Out
		{
			19,
			24,
			27,
			32,
			35,
			37,
			42,
			44,
			46,
			51
		},
		new int[]//Roll In Out
		{
			20,
			22,
			28,
			30,
			38,
			40,
			47,
			49
		},
		new int[]//Left Finger Open Close
		{
			52,
			54,
			55,
			56,
			58,
			59,
			60,
			62,
			63,
			64,
			66,
			67,
			68,
			70,
			71
		},
		new int[]//Right Finger Open Close
		{
			72,
			74,
			75,
			76,
			78,
			79,
			80,
			82,
			83,
			84,
			86,
			87,
			88,
			90,
			91
		},
		new int[]//Left Finger In Out
		{
			53,
			57,
			61,
			65,
			69
		},
		new int[]//Right Finger In Out
		{
			73,
			77,
			81,
			85,
			89
		}
	};

	public static string[] GetMuscleName(){
		string[] muscleName = HumanTrait.MuscleName;
		for (int j = 0; j < muscleName.Length; j++)
		{
			if (muscleName [j].StartsWith ("Right"))
			{
				muscleName [j] = muscleName [j].Substring (5).Trim ();
			}
			if (muscleName [j].StartsWith ("Left"))
			{
				muscleName [j] = muscleName [j].Substring (4).Trim ();
			}
		}

		return muscleName;
	}

	public static string[] GetPropertyMuscleName(){
		string[] propertyMuscleName = HumanTrait.MuscleName;
		for (int j = 0; j < propertyMuscleName.Length; j++)
		{
			if (propertyMuscleName [j].EndsWith("Stretched"))
			{
				string[] subString = propertyMuscleName[j].Split(' ');
				propertyMuscleName [j] = subString[0]+"Hand."+subString[1]+"."+subString[2]+" "+subString[3];
			}
			if (propertyMuscleName [j].EndsWith("Spread"))
			{
				string[] subString = propertyMuscleName[j].Split(' ');
				propertyMuscleName [j] = subString[0]+"Hand."+subString[1]+"."+subString[2];
			}
		}

		return propertyMuscleName;
	}


}
