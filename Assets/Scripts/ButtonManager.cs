using UnityEngine;
using UnityEngine.SceneManagement;
using EasyButtons;

public class ButtonManager : MonoBehaviour {
	[SerializeField]
	string loadSceneByDef;

    public bool TestScript;
    [ConditionalHide("TestScript",true)]
    public bool Stuff;
    [ConditionalHide("TestScript", true)]
    public AnimationCurve someCurve;

    [Button]
    public void SayHello()
    {
        Debug.LogError("Hello");
    }
    
    public void loadScene()
	{
		SceneManager.LoadScene(loadSceneByDef);
	}

}
