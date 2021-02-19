using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetUpPlayerPrefs : MonoBehaviour
{
    bool setUp;
    public GameObject sensSlider;
    public GameObject volumeSlider;
    public GameObject musicVolumeSlider;
    public GameObject fovSlider;

    private void Awake()
    {
        setUp = false;
    }

    private void Update()
    {
        if (sensSlider.GetComponent<Slider>().value != PlayerPrefs.GetFloat("setSensitivityPref") && setUp == false || volumeSlider.GetComponent<Slider>().value != PlayerPrefs.GetFloat("setVolumePref") && setUp == false
        || musicVolumeSlider.GetComponent<Slider>().value != PlayerPrefs.GetFloat("setMusicVolumePref") && setUp == false || fovSlider.GetComponent<Slider>().value != PlayerPrefs.GetFloat("setFOVPref") && setUp == false)
        {
            SetUpThePrefs();
        }

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            UpdateSettings();
        }
    }

    void SetUpThePrefs()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            GameObject.Find("Player").GetComponent<PlayerLook>().mouseSensitivity = PlayerPrefs.GetFloat("setSensitivityPref");
        }
        sensSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("setSensitivityPref");


        volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("setVolumePref");

        musicVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("setMusicVolumePref");


        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            GameObject.Find("CharacterCamera").GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("setFOVPref");
        }
        fovSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("setFOVPref");

        setUp = true;
    }
    
    void UpdateSettings()
    {
            GameObject.Find("Player").GetComponent<PlayerLook>().mouseSensitivity = PlayerPrefs.GetFloat("setSensitivityPref");
       
            GameObject.Find("CharacterCamera").GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("setFOVPref");
    }
}
