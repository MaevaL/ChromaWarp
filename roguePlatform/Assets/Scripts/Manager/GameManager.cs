using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using RoguePlateformer;

/// <summary>
///  Game Manager
/// </summary>
public class GameManager : MonoBehaviour {
    
    //All Variable needed for SaveGame
    //Upgrade Values
    [SerializeField]
    private float facteurUpgrade = 1.5f; 
    [SerializeField]
    private int costUpgrade = 10; 
    //PlayerValue
    [SerializeField]
    private int energyMax = 150;
    [SerializeField]
    private int lifeMax = 10;
    [SerializeField]
    private float projectileRate = 0.6f;
    [SerializeField]
    private float cacRate=0.3f;
    [SerializeField]
    private int energyCurrent = 0;
    [SerializeField]
    private int projectileDmg = 1;
    [SerializeField]
    private int cacDmg =1;
    [SerializeField]
    private float runSpeed = 10f;
    [SerializeField]
    private float jumpSpeed = 750f;
    [SerializeField]
    private float dashSpeed = 20f;

	private GameObject player;

    //Controller 
    private GoldController goldController;
    private LifeController lifeController;
    private FireController fireController;
    private MeleeController meleeController;
    private PlayerController playerController;
    private PlayerMove playerMove;
    private UpgradeController upgradeController;
    
	public float timerSecond = 0f;
	public float timerMinute = 0f;
	public Text timerText;
	public static GameManager instance = null;

    void Awake() {
        //Check if instance already exists
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    void Start() {
            GameObject.Find("HUD").SetActive(true);
            GameObject.Find("Heart").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("HealthText").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("Coin").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("CoinText").GetComponent<Mask>().showMaskGraphic = false;
            GameObject.Find("TimerText").GetComponent<Mask>().showMaskGraphic = false;

    }
		
    void InitGame() {
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timerText.text = "0:0";
        InvokeRepeating("IncrTimer", 0.0f, 1.0f);
    }

    void IncrTimer() {
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Shop") {
            return;
        }
        else {
            //if scene load is Menu or Shop
            //Don't increm
            timerSecond++;

            if (timerSecond == 60) {
                timerSecond = 0;
                timerMinute++;
            }
            timerText.text = timerMinute + ":" + timerSecond;
        }
    }
		
    //GameOver is called when the player reaches 0HP
    public void GameOver() {
        //Save PlayerData 
        instance.SaveLevelInfos();
        enabled = false;
    }

    //When the level need to be restart, conserv the important data 
    public void SaveLevelInfos() {
        //Energy
        InitController(); 
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
        
        //Upgrade
        costUpgrade = upgradeController.GetCost();
        facteurUpgrade = upgradeController.GetFacteur(); 
    }
		
    public void InitialisationPlayer() {
        InitController(); 
        //Initialize Gold
        goldController.SetEnergy(energyCurrent);
        goldController.SetGoldMax(energyMax);

        //Initialize MaxLife 
        lifeController.SetLife(lifeMax); 
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

        //UpgradeController
        upgradeController.SetCost(costUpgrade); 
    }

    public void InitController() {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            goldController = player.GetComponent<GoldController>();
            lifeController = player.GetComponent<LifeController>();
            fireController = player.GetComponent<FireController>();
            meleeController = player.GetComponent<MeleeController>();
            playerController = player.GetComponent<PlayerController>();
            playerMove = player.GetComponent<PlayerMove>();
            upgradeController = player.GetComponent<UpgradeController>(); 
        }
        else
            Debug.LogError("Player not Initialized");
    }

    public GameObject GetPlayer()
    {
        return player; 
    }

}

