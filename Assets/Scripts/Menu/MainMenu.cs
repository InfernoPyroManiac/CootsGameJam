using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsstuff;
    public AudioClip replacement;
    public AudioClip credits;
    public AudioSource elloguv;

    void Awake()
    {
        creditsstuff.SetActive(false);
    }
    void Update()
    {
        if (!elloguv.isPlaying) { elloguv.clip = replacement; elloguv.loop = true; elloguv.Play(); }
    }
    public void Play()
    {
        SceneManager.LoadScene("HighStreetHighJinks", LoadSceneMode.Single);
    }

    public void Settings() //this is credits now lol
    {
        creditsstuff.SetActive(true);
        elloguv.clip = credits; elloguv.loop = true; elloguv.Play();
    }

    public void Settingsback()
    {
        creditsstuff.SetActive(false);
        elloguv.clip = replacement; elloguv.loop = true; elloguv.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }
}