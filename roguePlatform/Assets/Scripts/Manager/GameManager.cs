﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;      
using UnityEngine.UI;                  

public class GameManager : MonoBehaviour
{
    //public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
    public float timerSecond = 0f;                                //Timer of the player
    public float timerMinute = 0f;
    public Text timerText;
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.


    //private Text levelText;                                 //Text to display current level number.
    //private GameObject levelImage;                          //Image to block out level as levels are being set up, background for levelText.
    //private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    private int level = 1;                                  //Current level number, expressed in game as "Day 1".
    private List<Enemy> enemies;                            //List of all Enemy units, used to issue them move commands.
    //private bool doingSetup = true;                         //Boolean to check if we're setting up board, prevent Player from moving during setup.



    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {

            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Assign enemies to a new List of Enemy objects.
        enemies = new List<Enemy>();

        //Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //this is called only once, and the paramter tell it to be called only after the scene was loaded
    //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        instance.level++;
        instance.InitGame();
    }


    //Initializes the game for each level.
    void InitGame()
    {
        //While doingSetup is true the player can't move
        //doingSetup = true;

        //Get a reference to our image LevelImage by finding it by name.
        //levelImage = GameObject.Find("LevelImage");

        ////Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
        //levelText = GameObject.Find("LevelText").GetComponent<Text>();

        ////Set the text of levelText to the string "Day" and append the current level number.
        //levelText.text = "Day " + level;

        ////Set levelImage to active blocking player's view of the game board during setup.
        //levelImage.SetActive(true);

        ////Call the HideLevelImage function with a delay in seconds of levelStartDelay.
        //Invoke("HideLevelImage", levelStartDelay);

        //Clear any Enemy objects in our List to prepare for next level.
        enemies.Clear();

        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timerText.text = "0:0";

        InvokeRepeating("IncrTimer", 0.0f, 1.0f);
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        //boardScript.SetupScene(level);

    }

    void IncrTimer()
    {
        timerSecond++;

        if (timerSecond == 60)
        {
            timerSecond = 0;
            timerMinute++;
        }
        timerText.text = timerMinute + ":" + timerSecond;
    }


    //Hides black image used between levels
    void HideLevelImage()
    {
        //Disable the levelImage gameObject.
        //levelImage.SetActive(false);

        //Set doingSetup to false allowing player to move again.
        //doingSetup = false;
    }

    //Update is called every frame.
    void Update()
    {
        

    }

    //Call this to add the passed in Enemy to the List of Enemy objects.
    public void AddEnemyToList(Enemy script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }


    //GameOver is called when the player reaches 0 food points
    public void GameOver()
    {
        //Set levelText to display number of levels passed and game over message
        //levelText.text = "After " + level + " days, you starved.";

        ////Enable black background image gameObject.
        //levelImage.SetActive(true);

        //Disable this GameManager.
        enabled = false;
    }

}
