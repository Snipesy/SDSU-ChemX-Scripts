using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadTankSim()
    {
        SceneManager.LoadScene("TankSim");
    }

    public void LoadCyllinderSim()
    {
        SceneManager.LoadScene("CyllinderSim");

    }

    public void LoadReactionsSim()
    {
        SceneManager.LoadScene("ReactionsSim");
    }

    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsScene");

    }

    public void openPrivacyPoilicy()
    {
        Application.OpenURL("https://surrealdev.com/privacy-policy/");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
