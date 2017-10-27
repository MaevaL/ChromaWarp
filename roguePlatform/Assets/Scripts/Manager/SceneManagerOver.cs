using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerOver : MonoBehaviour {

    public static SceneManagerOver Instance { set; get; }
    private GameManager gameManager; 

    // Use this for initialization
    void Start() {
        if (Instance != null) {
            GameObject.Destroy(gameObject);
        }
        else {
            GameObject.DontDestroyOnLoad(gameObject);
            gameManager = gameObject.GetComponent<GameManager>();
            
            Instance = this;

        }
    }


    // Update is called once per frame
    void Update() {
        //TEMPORAIRE
        //TRIGGER FIN DE NIVEAU ICI
        
        if (Input.GetKeyUp(KeyCode.Keypad1)) {
            gameManager.SaveLevelInfos(); 
            Load(1);
        }
        if (Input.GetKeyUp(KeyCode.Keypad2)) {
            gameManager.SaveLevelInfos();
            Load(0);
        }
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Load("Menu");
        }
    }


    public void Load(int sceneIndex) {

        if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded) {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
            //PlayMusic(SceneManager.GetActiveScene().GetRootGameObjects().);

        }

    }

    public void Load(string sceneName) {

        //if (!(sceneName == "Menu" || sceneName == "Shop")) {

        //    GameObject.Find("HUD").SetActive(true);
        //}

        if (!SceneManager.GetSceneByName(sceneName).isLoaded) {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

    }

    public void Unload(string sceneName) {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded) {
            SceneManager.UnloadSceneAsync(sceneName);
        }

    }

    public void Unload(int sceneIndex) {
        if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded) {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }

    }

}
