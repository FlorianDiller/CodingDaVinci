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

    public void GoFly ()
    {
        if(ameliaPlaneIdle.activeSelf)
        {
            ameliaPlaneIdle.SetActive(false);
            ameliaPlane.SetActive(true);
            SceneManager.LoadScene(1);
        }
        if(ottoPlaneIdle.activeSelf)
        {
            ottoPlaneIdle.SetActive(false);
            ottoPlane.SetActive(true);
            SceneManager.LoadScene(2);
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
}
