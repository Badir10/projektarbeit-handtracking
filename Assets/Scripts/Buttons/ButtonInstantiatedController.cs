using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ButtonInstantiatedController : MonoBehaviour
{
    ///// Hier befinden sich alle Eventhandler die bei der Aktivierung der Buttons verwendet werden /////
    /// 
    
    
    private bool pauseBool;
    private float timePassed = 0;
    private float maxTime = 0.7f;

    private float lerpTimer = 1;
    //private SkyboxChanger skyboxChanger;
    private Renderer rend;

    //private MusicPlayer musicPlayer;

    [SerializeField]
    private AudioSource buttonSound;

    [SerializeField]
    private Sprite iconDef;
    [SerializeField]
    private Sprite iconAlt;
    [SerializeField]
    private GameObject toInfluence;
    

    void Awake()
    {
        // nimmt sich zu Beginn dieser Klasse alle fuer den Code notwendigen Komponenten waehrend das Spiel laeuft 
        
        //skyboxChanger = GameObject.Find("Skybox").GetComponent<SkyboxChanger>();
        rend = gameObject.GetComponent<Renderer>();
        //musicPlayer = GameObject.Find("Audio").GetComponent<MusicPlayer>();
    }

    private void Start()
    {
        pauseBool = false;
    }

    private void FixedUpdate()
    {
        // pause boolean der eine gewisse Zeit verstreichen laesst und dann an den bool weitergibt
        if (pauseBool)
        {
            timePassed = timePassed + Time.deltaTime;
            
            // wenn verstrichene Zeit größer oder gleich der maximalen vorgegebenen Zeit ist
            if (timePassed >= maxTime)
            {
                timePassed = 0;
                pauseBool = false;
            }
        }
    }

    // die beiden Methoden aendern die Skybox nachdem eine Zeit von pauseBool verstrichen ist
    public void SkyboxUp()
    {
        if (!pauseBool)
        {
            pauseBool = true;
            //skyboxChanger.SkyboxUp();
        }
    }
    public void SkyboxDown()
    {
        if (!pauseBool)
        {
            //skyboxChanger.SkyboxDown();
            pauseBool = true;
        }
    }
    
    // die Methode aendert die Prefabs in der Umgebung nachdem eine Zeit von pauseBool verstrichen ist
    public void UmgebungUp()
    {
        if (!pauseBool)
        {
            //skyboxChanger.UmgebungUp();
            pauseBool = true;
        }
    }

    public void UmgebungDown()
    {
        if (!pauseBool)
        {
            //skyboxChanger.UmgebungDown();
            pauseBool = true;
        }
    }

    // die drei Methoden aendern Musik und pausieren und spielen sie nachdem eine Zeit von pauseBool verstrichen ist
    public void MusicUp()
    {
        if (!pauseBool)
        {
            pauseBool = true;
            //musicPlayer.MusicUp();
        }
    }
    public void MusicDown()
    {
        if (!pauseBool)
        {
            pauseBool = true;
            //musicPlayer.MusicDown();
        }
    }
    public void MusicPlayPause()
    {
        if (!pauseBool)
        {
            pauseBool = true;
            //musicPlayer.MusicPlayPause();
        }
    }

    
    // lerpt die Farbe des Buttons
    public void ColorLerpOn()
    {
        
            rend.material.color = Color.Lerp(Color.white, Color.green, lerpTimer * 10);
        
    }

    // spielt den Button-klick ab
    public void ButtonSound()
    {
        buttonSound.Play();
    }
}
