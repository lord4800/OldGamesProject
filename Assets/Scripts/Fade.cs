using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BombaScr{
	public class Fade : MonoBehaviour {
		public float FadeTime;
		public SpriteRenderer Sp;
		public string [] Scenes;
		public int SceneNumber;
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		public void FadeNow ()
		{
			StartCoroutine (Stay());
		}

		IEnumerator Stay ()
		{
			yield return new WaitForSeconds(0);
			for(float i = 0; i<FadeTime;i+=Time.deltaTime )
			{
				Sp.color = new Color(0,0,0,i/FadeTime);
			}
			//if (PlayerPrefs.GetInt("Level") != 0 ){
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(Scenes[SceneNumber]);
			//}
		}
	}
}
