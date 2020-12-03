using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{


    [HideInInspector]
    public bool gameOver = false;
    [HideInInspector]
    public bool gameWin = false;

    public Text levelFailed;
    public Text levelCompleted;

    public GameObject myCanvas;
    PlayerMovement Player;
    
    private AudioManager audioManager;


    private void Start()
    {
       
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.Log("Playcouch");
        }
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Player = FindObjectOfType<PlayerMovement>();
            
            if (Player.mygun != null && Player.mygun.inHand)
            {
                myCanvas.SetActive(true);
                myCanvas.GetComponentInChildren<Text>().text = "Magazin: " + Player.mygun.magazin.ToString() + " Name : " + Player.mygun.tag;
            }
            else
            {
                myCanvas.SetActive(false);
            }

            if (!Player.alive)
            {
                myCanvas.SetActive(true);
                myCanvas.GetComponentInChildren<Text>().text = "TO RESTART PRESS R";
                if (Input.GetKey(KeyCode.R))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Destroy(audioManager);
                audioManager.StopSound("dentistTheme");
                SceneManager.LoadScene(0);
            }
            

        }
    }
}
