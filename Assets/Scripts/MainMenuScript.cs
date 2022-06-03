using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    public GameObject ottoCharacter;
    public GameObject ameliaCharacter;
    public GameObject ottoPlane;
    public GameObject ameliaPlane;
    public GameObject ottoPlaneIdle;
    public GameObject ameliaPlaneIdle;
    public AudioSource MotorRun;
    public GameObject instructionPanel;

    public void GoFly ()
    {
        MotorRun.GetComponent<AudioSource>().enabled = true;
        MotorRun.GetComponent<AudioSource>().Play();
        if (ameliaPlaneIdle.activeSelf)
        {
            ameliaPlaneIdle.SetActive(false);
            ameliaPlane.SetActive(true);
        }
        if (ottoPlaneIdle.activeSelf)
        {
            ottoPlaneIdle.SetActive(false);
            ottoPlane.SetActive(true);
        }
    }

    public void ChooseAmelia ()
    {
        ottoCharacter.SetActive(false);
        ottoPlaneIdle.SetActive(false);
        ameliaCharacter.SetActive(true);
        ameliaPlaneIdle.SetActive(true);
    }

    public void ChooseOtto ()
    {
        ottoCharacter.SetActive(true);
        ottoPlaneIdle.SetActive(true);
        ameliaCharacter.SetActive(false);
        ameliaPlaneIdle.SetActive(false);
    }
    public void ToggleInstructions ()
    {
        instructionPanel.SetActive(!instructionPanel.activeSelf);
    }
    public void Update()
    {
        if (MotorRun.GetComponent<AudioSource>().enabled && !MotorRun.GetComponent<AudioSource>().isPlaying)
        {
            if (ameliaCharacter.activeSelf)
            {
                SceneManager.LoadScene(1);
            }
            if (ottoCharacter.activeSelf)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
