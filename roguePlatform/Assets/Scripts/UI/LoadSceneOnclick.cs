using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnclick : MonoBehaviour {

    SceneManagerOver sceneManagerOver;

    void Start() {

        sceneManagerOver = GameObject.Find("NewGameManager").GetComponent<SceneManagerOver>();
    }

	public void LoadByIndex(int sceneIndex) {
        sceneManagerOver.Load(sceneIndex);
    }
}
