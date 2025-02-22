using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Item {
    public string itemName;
    public int itemID;
}





public class GamePlayer: MonoBehaviour {
    public float health = 100f;

}
public class GameManager : MonoBehaviour
{
    public GameObject menuPanel;
    private bool isPaused = false;
    public Player player;
    //public Inventory inventory = new Inventory();
    //public Enemy[] enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ToggleMenu();
        }
    }
    public void ToggleMenu() {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            menuPanel.SetActive(true); // show menu;

        }
        else 
        {
            Time.timeScale = 1;
            menuPanel.SetActive(false);
        }
    }
    public void SaveGame() {
        //PlayerPrefs.SetFloat("PlayerHealth", Player.health);
        PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);
        //string itemsJson = JsonUtility.ToJson(inventory);
        //PlayerPrefs.SetString("PlayerInventory", itemsJson);
      
       /* for (int i = 0; i < enemies.Length; i++)
        {
            PlayerPrefs.SetFloat($"Enemy{i}Health", enemies[i].health);
        }
        PlayerPrefs.Save();*/
        Debug.Log("Game Saved");
    }

    public void LoadGame() {
        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            float posX = PlayerPrefs.GetFloat("PlayerPositionX");
            float posY = PlayerPrefs.GetFloat("PlayerPositionY");
            float posZ = PlayerPrefs.GetFloat("PlayerPositionZ");
            player.transform.position = new Vector3(posX, posY, posZ);
            Debug.Log("Game Loaded: Player Position Updated");
        }
        else {
            Debug.Log("No saved player position found.");
        }
    }
}
