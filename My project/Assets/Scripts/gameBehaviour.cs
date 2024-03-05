using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CustomExtensions;

using UnityEngine.SceneManagement;

public class gameBehaviour : MonoBehaviour, IManager
{
    private string state;

    public string State
    {
        get { return state; }
        set { state = value; }
    }
    
    public string labelText = "Collect all 4 items to proceed";
    public int maxItems = 4;
    public int ammoCount = 0;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public int armourCount;

    public Stack<string> lootStack = new Stack<string>();

    private int itemsCollected = 0;

    public delegate void DebugDelegate(string newText);

    public DebugDelegate debug = Print;

    public int Items
    {
        get { return itemsCollected; }

        set
        {
            itemsCollected = value;
            Debug.LogFormat("Items: {0}", itemsCollected);

            if(itemsCollected >= maxItems)
            {
                labelText = "You've found all items";
                showWinScreen = true;

                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - itemsCollected) + " more to go";
            }
        }

    }

    private int playerHP = 10;
    public int HP
    {
        get { return playerHP; }
        set
        {
            playerHP = value;

            if(playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's got hurt.";
            }

            Debug.LogFormat("Lives: {0}", playerHP);
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + playerHP);

        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + itemsCollected);

        GUI.Box(new Rect(20, 80, 150, 25), "Ammo: " + ammoCount);

        GUI.Box(new Rect(20, 110, 150, 25), "Armour: " + armourCount);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "You can now continue."))
            {
                //SceneManager.LoadScene(0);

                //Time.timeScale = 1.0f;

                Utilities.RestartLevel(0);
            }

            if (showLossScreen)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
                {
                    try
                    {
                        Utilities.RestartLevel(-1);
                        debug("levelrestarted successfully");
                    }
                    
                    catch (System.ArgumentException e)
                    {
                        Utilities.RestartLevel(0);
                        debug("reverting to scene 0: " + e.ToString());
                    }

                    finally
                    {
                        debug("Restart handled");
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        InventoryList<string> inventoryList = new InventoryList<string>();

        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void Initialize()
    {
        state = "Manager initialized..";
        state.FancyDebug();

        debug(state);

        Debug.Log(state);

        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");

        PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();

        playerBehaviour.playerJump += HandlePlayerJump;

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boots");
        lootStack.Push("Mythril Bracers");
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("delegating the debug task");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();

        var nextItem = lootStack.Peek();

        Debug.LogFormat("You got a {0}! Ypu've got a good chance at finding a {1} next!", currentItem, nextItem);
        
        Debug.LogFormat("There are {0} random loot items waiting for you", lootStack.Count);
    }
}
