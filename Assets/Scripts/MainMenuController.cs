using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [Header("Outros")]    
    public AudioController audioController;

    // Start is called before the first frame update
    void Start()
    {
        audioController.playMusic(audioController.mainMenuMusic);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }



}
