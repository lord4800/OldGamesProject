using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	[SerializeField]
	string loadSceneByDef;

	public void loadScene()
	{
		SceneManager.LoadScene(loadSceneByDef);
	}

}
