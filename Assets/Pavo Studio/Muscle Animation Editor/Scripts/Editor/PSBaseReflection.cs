using UnityEngine;
using System.Collections;
using UnityEditor;

public abstract class PSBaseReflection {

	public static string UNITYEDITOR_DLL {
		get {
			if (Application.platform == RuntimePlatform.OSXEditor) {
				return EditorApplication.applicationContentsPath + "/Frameworks/Managed/UnityEditor.dll";
			} else {
				return EditorApplication.applicationContentsPath + "/Managed/UnityEditor.dll";
			}
		}
	}
	
	public abstract void Init();
	public abstract float GetTime();
	public abstract int GetFrame();
	public abstract AnimationClip GetClip();
	public abstract GameObject GetRootObject();
}
