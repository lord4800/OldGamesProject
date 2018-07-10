using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


public class SceneEditor : EditorWindow {

	[MenuItem ("Loader/ScenesWindow")]
	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(SceneEditor));
	}

	
	public List<string> scenes = new List<string> ();
	private static string[] FillLevels()
	{
		return (from scene in EditorBuildSettings.scenes  select scene.path).ToArray();
	}
	void Awake()
	{
		Load ();
	}
	public Vector2 scrollPosition = Vector2.zero;
	void OnGUI()
	{
		EditorGUILayout.BeginVertical ( );
		scrollPosition = GUI.BeginScrollView(new Rect(10, 10, this.position.width-15 , this.position.height-50), scrollPosition, new Rect(0, 0, 100, scenes.Count * 25));
	 
		for (int i = 0; i<scenes.Count; i++) {
			
			string name = scenes [i].Split(new char[1]{'/'})[scenes [i].Split(new char[1]{'/'}).Length-1];
			if (GUI.Button (new Rect (0, i * 25, 250, 20), name)) {
				EditorApplication.OpenScene (scenes [i]);
			}
			if (GUI.Button (new Rect (255, i * 25, 40, 20), "ADD")) {
				if (SceneManager.GetSceneByPath (scenes [i]).isLoaded)
				{
					EditorSceneManager.CloseScene (SceneManager.GetSceneByPath (scenes [i]),true);
				}else{
					EditorSceneManager.OpenScene (scenes [i], OpenSceneMode.Additive);
				}
			}
			if (GUI.Button (new Rect (300, i * 25, 30, 20), "U")) {
				if(i!=0)
				{
					string std = scenes[i];
					scenes[i] = scenes[i-1];
					scenes[i-1] = std;
				}
			}
			if (GUI.Button (new Rect (335, i * 25, 30, 20), "D")) {
				if(i!=scenes.Count-1)
				{
					string std = scenes[i ];
					scenes[i] = scenes[i+1];
					scenes[i+1] = std;
				}
			}



			if (GUI.Button (new Rect (370, i * 25, 90, 20), "Delete")) {
				scenes.Remove(scenes[i]);
			}

		}

		GUI.EndScrollView();
		if (GUI.Button (new Rect (10,this.position.height -30, 100, 25), "AddScene")) {
			if (Selection.activeObject == null) {
					scenes.Add (EditorSceneManager.GetActiveScene().path);
			} else {

				var obj = Selection.activeObject;
				if (obj != null) {
					string path = AssetDatabase.GetAssetPath (obj.GetInstanceID ());
					if (path.Contains (".unity"))
						scenes.Add (path);
				
				}
			}
		}
		if (GUI.Button (new Rect (125,this.position.height -30, 100, 25), "Save")) {
			Save();
		}
		if (GUI.Button (new Rect (240,this.position.height -30, 100, 25), "Load")) {
			Load();
		}
		EditorGUILayout.EndVertical ();
	}
	void Save()
	{ 

		string path =  @"Assets/Scenes/" +"scenesSave";
		//		Debug.Log("Save to :"+path);
		FileStream fss = new FileStream(path, FileMode.Create);
		BinaryFormatter bff = new BinaryFormatter();
		bff.Serialize(fss,scenes);
		fss.Close();
	}
	void Load()
	{
		if (!File.Exists( @"Assets/Scenes/"   +"scenesSave"))
		{
			return  ;
		}
		
		FileStream fsss = new FileStream(  @"Assets/Scenes/"  +"scenesSave", FileMode.Open);
		if (fsss==null)
			return  ;
		
		BinaryFormatter bf;
		object loadedItem;
		
		try
		{
			bf = new BinaryFormatter();
			loadedItem = bf.Deserialize(fsss);
		}
		catch(Exception e)
		{
			fsss.Close();
			Debug.LogError(e.ToString());
			return  ;
		}
		
		fsss.Close();
		if (loadedItem is List<string>)
		{
			scenes = (List<string>)loadedItem;
		}
		else
		{
			return  ;
		}
		return  ;
	}
}
