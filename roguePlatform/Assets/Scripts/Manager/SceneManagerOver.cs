using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerOver : MonoBehaviour {

    public static SceneManagerOver Instance { set; get; }
    private GameManager gameManager;
    
    private GameObject player; 
    private PlayerController playerController;

    private Scene activeScene; 
    private string previousScene = null;
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
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        
        activeScene = SceneManager.GetActiveScene();

       
        if (player != null)
        {
            if (playerController.GetIsDead())
            {
                previousScene = SceneManager.GetActiveScene().name;
                gameManager.SaveLevelInfos();
                Debug.Log("PlayerDataSaved");
                Load("Shop");
            }
        }

        //if(activeScene.name == "Menu")
        //{
        //    GameObject.Find("HUD").SetActive(false);
        //}

       
        if(activeScene.name == "Shop" && playerController.GetShopDisabled())
        {
            if (previousScene != null)
            {
                Load(previousScene);   
            }
            else
            {
                Load("Menu");
            }
        }

        //For TESTING PASSAGE LEVEL
        //if (Input.GetKeyUp(KeyCode.Keypad1)) {
        //    gameManager.SaveLevelInfos(); 
        //    Load(1);
        //}
        //if (Input.GetKeyUp(KeyCode.Keypad2)) {
        //    gameManager.SaveLevelInfos();
        //    Load(2);
        //}

        if (Input.GetKeyUp(KeyCode.Escape)) {
            gameManager.SaveLevelInfos();
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
        gameManager.SaveLevelInfos();
        if (!(sceneName == "Menu" || sceneName == "Shop")) {
            GameObject.Find("HUD").SetActive(true);
        }

        if (!SceneManager.GetSceneByName(sceneName).isLoaded) {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        gameManager.InitialisationPlayer();

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
