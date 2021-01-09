using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{ 
    public string firstLevel, levelSelect;

    public GameObject continueButton;

    public string[] levelNames;

   
    public bool isNewGame;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Continue"))
        {
            continueButton.SetActive(true);
        }
      /*  else
        {
           ResetProgress();
        }
     */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.DeleteAll(); //alternate way of reseting progress lol

        PlayerPrefs.SetInt("Continue", 0);

        PlayerPrefs.SetString("CurrentLevel", firstLevel);

    //    ResetProgress();
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetProgress()
    {
        for(int i = 0; i < levelNames.Length; i++)
        {
            PlayerPrefs.SetInt(levelNames[i] + "_unlocked", 0);
            PlayerPrefs.SetInt(levelNames[i] + "_coins", 0);
        }
    }

   
}
