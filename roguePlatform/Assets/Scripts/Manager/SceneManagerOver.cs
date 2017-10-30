using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///  Scene Manager 
///  Manage differents scenes of the game
/// </summary>
public class SceneManagerOver : MonoBehaviour {

    private GameManager gameManager;
    private GameObject player; 
    private PlayerController playerController;
    private Scene activeScene; 
    private string previousScene = null;

	public static SceneManagerOver Instance { set; get; }

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


    void Update() {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            playerController = player.GetComponent<PlayerController>();
        }
        activeScene = SceneManager.GetActiveScene();
     
        if (player != null) {
            if (playerController.GetEndLevel()) {
				//Load (SceneManager.GetActiveScene ().index);
                Load("Menu");
            }
            if (playerController.GetIsDead()) {
                previousScene = SceneManager.GetActiveScene().name;
                gameManager.SaveLevelInfos();
                Load("Shop");
            }
        }


        if (activeScene.name == "Shop" && playerController.GetShopDisabled()) {
            if (previousScene != null) {
                Load(previousScene);   
            }
            else {
                Load("Menu");
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape)) {
            gameManager.SaveLevelInfos();
            Load("Menu");
        }
    }

	//Load scene by his index number
    public void Load(int sceneIndex) {
        string nameScene = SceneManager.GetSceneByBuildIndex(sceneIndex).name;
        if (!(nameScene == "Menu" || nameScene == "Shop")) {
            GameObject.Find("HUD").SetActive(true);
            GameObject.Find("Heart").GetComponent<Mask>().showMaskGraphic = true;
            GameObject.Find("HealthText").GetComponent<Mask>().showMaskGraphic = true;
            GameObject.Find("Coin").GetComponent<Mask>().showMaskGraphic = true;
            GameObject.Find("CoinText").GetComponent<Mask>().showMaskGraphic = true;
            GameObject.Find("TimerText").GetComponent<Mask>().showMaskGraphic = true;
        }
        else {
			//Disable the HUD when or shop menu is loaded
            GameObject.Find("HUD").SetActive(true);
            GameObject.Find("Heart").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("HealthText").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("Coin").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("CoinText").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("TimerText").GetComponent<Mask>().showMaskGraphic = false;
        }

        if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded) {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }

	//Load scene by his name
    public void Load(string sceneName) {
        gameManager.SaveLevelInfos();
        if (!(sceneName == "Menu" || sceneName == "Shop")) {
            GameObject.Find("HUD").SetActive(true);
            GameObject.Find("Heart").GetComponent<Mask>().showMaskGraphic = true;
            GameObject.Find("HealthText").GetComponent<Mask>().showMaskGraphic = true;
            GameObject.Find("Coin").GetComponent<Mask>().showMaskGraphic = true;
            GameObject.Find("CoinText").GetComponent<Mask>().showMaskGraphic = true;
            GameObject.Find("TimerText").GetComponent<Mask>().showMaskGraphic = true;
        }
        else {
			//Disable the HUD when menu is loaded
            GameObject.Find("HUD").SetActive(true);
            GameObject.Find("Heart").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("HealthText").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("Coin").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("CoinText").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("TimerText").GetComponent<Mask>().showMaskGraphic = false;
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
