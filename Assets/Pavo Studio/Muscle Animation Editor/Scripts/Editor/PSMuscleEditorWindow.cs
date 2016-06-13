//
//  PSMuscleEditorWindow.cs
//  PSMuscle Editor
//
//  Copyright (c) 2014-2015 Pavo Studio.
//
using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

class PSMuscleEditorWindow : EditorWindow
{

	public enum TAB
	{
		MUSCLE = 0,
		UTILITY = 1
	}

	//public TAB curTab;
	public PSBaseReflection reflection;
	public AnimationClip clip;
	public float time;
	public int frame;
	public GameObject target;
	public PSTabMuscle tabMuscle;
	public int cleanCount = 0;
	public bool onCleaning = false;
	public List<EditorCurveBinding> unusedBindings;
	public Vector2 scrollPos;

	public bool onFocus;
	
	[MenuItem ("Window/Muscle Animation Editor")]
	static void InitWindow ()
	{
		((PSMuscleEditorWindow)EditorWindow.GetWindow (typeof(PSMuscleEditorWindow))).Initialize ();
	}

	public void Initialize ()
	{
		tabMuscle = new PSTabMuscle ();
	}

	void OnInspectorUpdate ()
	{
		this.Repaint ();
		UpdateValue ();
	}

	private void UpdateValue ()
	{
		if (reflection == null)
			return;

		GameObject newTarget = reflection.GetRootObject ();

		if (!IsValidTarget (newTarget)) {
			target = null;
			return;
		}
		
		if (newTarget != target) {
			target = newTarget;
			tabMuscle.SetTarget (target);
		}
		
		clip = reflection.GetClip ();
		time = reflection.GetTime ();
		frame = reflection.GetFrame ();
		
		tabMuscle.SetClip (clip);
		
		tabMuscle.SetTimeFrame (time, frame);


		if (AnimationMode.InAnimationMode ()&&!onFocus) {
			tabMuscle.OnUpdateValue ();
			//tabMuscle.ResampleAnimation();
		}
		
	}

	void OnFocus(){
		//Debug.Log("OnFocus");
		onFocus = true;
	}

	void OnLostFocus(){
		//Debug.Log("OnLostFocus");
		onFocus = false;
	}

	/***********************************************
	 *  GUI METHODS
	 ***********************************************/
	
	void OnGUI ()
	{
		//Debug.Log ("OnGUI:"+Event.current.type);
		
		if (reflection == null) {
			reflection = PSReflectionFactory.GetInstance ().GetReflection ();	
		}
		
		if (target == null) {
			EditorGUILayout.HelpBox ("No humanoid target found, please select humanoid gameobject from the hierarchy.", MessageType.None);
			if (GUILayout.Button ("Refresh", EditorStyles.miniButton, GUILayout.Width (100))) {
				reflection.Init();
			}
			return;
		}
		
		this.InfoGUI ();
		EditorGUILayout.Space ();
		this.TabGUI ();

	}

	protected void TabGUI ()
	{

		scrollPos = EditorGUILayout.BeginScrollView (scrollPos);

		EditorGUI.BeginDisabledGroup (!AnimationMode.InAnimationMode ());
		
		tabMuscle.OnTabGUI ();
		
		EditorGUI.EndDisabledGroup ();

		EditorGUILayout.EndScrollView ();

	}
	
	protected void InfoGUI ()
	{
		EditorGUILayout.BeginHorizontal (EditorStyles.toolbar);
		EditorGUILayout.LabelField ("Information");
		if (GUILayout.Button ("Refresh", EditorStyles.toolbarButton, GUILayout.Width (80))) {
			reflection.Init();
		}
		EditorGUILayout.EndHorizontal ();

		Color tmpColor = GUI.color;
		GUI.color = Color.yellow;
		EditorGUILayout.HelpBox ("You should back up your clip files before using this tool!", MessageType.Warning);
		GUI.color = tmpColor;

		EditorGUILayout.HelpBox ("Target: " + target + "\nClip: " + clip + "\nTime: " + time + "s\nFrame: " + frame, MessageType.None);

		if (GUILayout.Button ("Remove useless properties", EditorStyles.miniButton, GUILayout.Width (180))) {

			clip = reflection.GetClip ();

			if (!onCleaning && clip != null && target != null) {
				EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings (clip);
				onCleaning = true;

				unusedBindings = new List<EditorCurveBinding> ();

				for (int i=0; i<bindings.Length; i++) {
					if (!string.IsNullOrEmpty (bindings [i].path) 
						&& target.transform.Find (bindings [i].path) == null) {
						unusedBindings.Add (bindings [i]);
					}
				}
			}

		}

		if (onCleaning) {

			if (cleanCount < unusedBindings.Count) {
				float progress = (float)cleanCount / (float)unusedBindings.Count;
				string info = "Removing: " + PSMuscleEditorUtil.PathTruncate (unusedBindings [cleanCount].path) + " : " + unusedBindings [cleanCount].propertyName;
				EditorUtility.DisplayProgressBar ("Remove useless properties", info, progress);
				AnimationUtility.SetEditorCurve (clip, unusedBindings [cleanCount], null);
				cleanCount++;
			} else {
				cleanCount = 0;
				onCleaning = false;
				EditorUtility.ClearProgressBar ();
			}
		}
	}

	/***********************************************
	 * 
	 ***********************************************/
	
	private bool IsValidTarget (GameObject obj)
	{
		if (obj != null) {
			Animator animator = obj.GetComponent (typeof(Animator)) as Animator;
			if (animator != null) {
				Avatar avatar = animator.avatar;
				if (avatar != null && avatar.isValid && avatar.isHuman) {
					return true;
				}
			}
		}

		return false;
	}
	
}


