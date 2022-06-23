using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField]
    public GameObject player;
    public GameObject instructionPanel;
    public GameObject pauseMenu;
    public GameObject gameUI;
    public GameObject audioPlayer;
    public GameObject summaryPanel = null;
    public GameObject congratulationPanel;
    public GameObject dataHandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!summaryPanel.activeSelf || summaryPanel == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                if (!pauseMenu.activeSelf)
                {
                    isPaused = true;
                    audioPlayer.GetComponent<AudioSource>().Stop();
                    gameUI.SetActive(false);
                    pauseMenu.SetActive(true);
                }
                else
                {
                    isPaused = false;
                    Continue();
                }
            }
        }
        if(dataHandler.GetComponent<DataReadExplore>() != null && dataHandler.GetComponent<DataReadExplore>().sitesFound == dataHandler.GetComponent<DataReadExplore>().tableSize)
        {
            audioPlayer.GetComponent<AudioSource>().Stop();
            gameUI.SetActive(false);
            player.GetComponent<Movement>().enabled = false;
            pauseMenu.SetActive(true);
            congratulationPanel.SetActive(true);
        }
        
        if (dataHandler.GetComponent<DataReadArchive>()!= null && !summaryPanel.activeSelf && dataHandler.GetComponent<DataReadArchive>().sitesArchived == dataHandler.GetComponent<DataReadArchive>().tableSize)
        {
            audioPlayer.GetComponent<AudioSource>().Stop();
            gameUI.SetActive(false);
            player.GetComponent<Movement>().enabled = false;
            pauseMenu.SetActive(true);
            congratulationPanel.SetActive(true);
        }
    }
    public void Continue()
    {
        audioPlayer.GetComponent<AudioSource>().Play();
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        isPaused = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ToggleInstructions()
    {
        instructionPanel.SetActive(!instructionPanel.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
