using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameManager gm;
    [SerializeField] GameObject connectionLostPopUp;
    [SerializeField] GameObject menuButtons;
    [SerializeField] GameObject menuButtonsComingSoon;
    [SerializeField] GameObject menuButtonsComingSoonText;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMainMenu () { 
        menuButtons.SetActive(true);
        menuButtonsComingSoon.SetActive(true);
        menuButtonsComingSoonText.SetActive(true);
    }
    public void CloseMainMenu () {
        menuButtons.SetActive(false);
        menuButtonsComingSoon.SetActive(false);
        menuButtonsComingSoonText.SetActive(false);
    }
    public void OpenConnectionLost () {
        connectionLostPopUp.SetActive(true);
    }
    public void CloseConnectionLost () {
        connectionLostPopUp.SetActive(false);
    }
    public void OpenTestMyStackMenu () {

    }
    public void CloseTestMyStackMenu () {

    }
    //buttons functions
    public void ButtonClickTestMyStack () {
        gm.ButtonClickTestMyStack();
    }
}
