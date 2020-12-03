using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LoadLevel : MonoBehaviour

    
{
    AudioManager audiomanager;
    private void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
    }

    public  void newGame()
    {
            audiomanager.PlaySound("dentistTheme");
            SceneManager.LoadScene(1);
    }

    public void loadCurrent()
        {
        Debug.Log("Current");
        //current Status
    }

    public void exitGame()
        {
        Debug.Log("Ende");
            Application.Quit();
        }
        
    
}
