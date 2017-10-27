using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;      
using UnityEngine.UI;
using RoguePlateformer;

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

    [SerializeField]
    private GameObject player;
    //All Variable needed for SaveGame //
    //PlayerValue
    private int energyMax;
    private int lifeMax;
    private float projectileRate;
    private float cacRate;
    private int energyCurrent;
    private int projectileDmg;
    private int cacDmg;
    private float runSpeed;
    private float jumpSpeed;
    private float dashSpeed; 
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
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
       // instance.level++;
        //instance.InitGame();
    }
    void Start() {
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timerText.text = "0:0";
        InvokeRepeating("IncrTimer", 0.0f, 1.0f);
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

    //When the level need to be restart, conserv the important data 
    public void SaveLevelInfos()
    {
        //A réimplémenter pour éviter une duplication du code 
        player = GameObject.FindGameObjectWithTag("Player");
        GoldController goldController = player.GetComponent<GoldController>();
        LifeController lifeController = player.GetComponent<LifeController>();
        FireController fireController = player.GetComponent<FireController>();
        MeleeController meleeController = player.GetComponent<MeleeController>();
        PlayerController playerController = player.GetComponent<PlayerController>();
        PlayerMove playerMove = player.GetComponent<PlayerMove>(); 

        //Energy
        energyCurrent = goldController.GetEnergy();
        energyMax = goldController.GetEnergyMax();
        //Life 
        lifeMax = lifeController.GetLifeMax();
        //Fire 
        projectileRate = fireController.GetFireRate();
        projectileDmg = playerController.GetDamageProjectile(); 
        //Cac
        cacRate = meleeController.GetAttackCooldown();
        cacDmg = playerController.GetDamageMelee();
        //Move 
        runSpeed = playerMove.GetRunSpeed();
        dashSpeed = playerMove.GetDashSpeed();
        jumpSpeed = playerMove.GetJumpSpeed(); 
    }

    public void InitialisationPlayer()
    {
        //A réimplémenter pour éviter une duplication du code
        player = GameObject.FindGameObjectWithTag("Player");
        GoldController goldController = player.GetComponent<GoldController>();
        LifeController lifeController = player.GetComponent<LifeController>();
        FireController fireController = player.GetComponent<FireController>();
        PlayerController playerController = player.GetComponent<PlayerController>();
        MeleeController meleeController = player.GetComponent<MeleeController>();
        PlayerMove playerMove = player.GetComponent<PlayerMove>(); 
        //Initialize Gold
        goldController.SetEnergy(energyCurrent);
        goldController.SetGoldMax(energyMax);
        //Initialize MaxLife 
        lifeController.SetLifeMax(lifeMax);
        //Initialize FireProperties
        fireController.SetFireRate(projectileRate);
        playerController.SetDamageProjectile(projectileDmg);
        //Initialize CAC Properties
        meleeController.SetAttackCoolDown(cacRate);
        playerController.SetDamageMelee(cacDmg);
        //Initiamlize PlayerMove
        playerMove.SetDashSpeed(dashSpeed);
        playerMove.SetJumpSpeed(jumpSpeed);
        playerMove.SetRunSpeed(runSpeed); 

    }   


    


}

