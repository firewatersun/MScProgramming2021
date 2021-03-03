using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score; // static variables can be accessed from other scripts and only one can exist at any time
    public static GameObject[] pickupsInScene;
    public static int currentPickupCount;
    public TMP_Text scoreText;
    public TMP_Text pickupText;
    public GameObject player;

    public static int pickupValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickupsInScene = GameObject.FindGameObjectsWithTag("pickup"); // populating array with all GameObjects that are pickups. 
        currentPickupCount = pickupsInScene.Length;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        pickupText.text = "Pickups Left: " + currentPickupCount;
        if (player.GetComponent<PlayerController>().health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
