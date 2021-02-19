using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerSettingVariables : MonoBehaviour
{


    float sensitivityPref;
    float volumePref;
    float musicVolumePref;
    float fovPref;
  
    public void SaveSensitivty(float newSensitivity)
    {
        if(sensitivityPref != newSensitivity)
        {
            sensitivityPref = newSensitivity;
            PlayerPrefs.SetFloat("setSensitivityPref", sensitivityPref);
            if(SceneManager.GetActiveScene().buildIndex >= 1)
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().mouseSensitivity = PlayerPrefs.GetFloat("setSensitivityPref");
            }
            PlayerPrefs.Save();
        }
    }
    public void SaveVolume(float newVolume)
    {
        if (volumePref != newVolume)
        {
            volumePref = newVolume;
            PlayerPrefs.SetFloat("setVolumePref", volumePref);
        }
        PlayerPrefs.Save();
    }
    public void SaveMusicVolume(float newMusicVolume)
    {
        if (musicVolumePref != newMusicVolume)
        {
            musicVolumePref = newMusicVolume;
            PlayerPrefs.SetFloat("setMusicVolumePref", musicVolumePref);
        }
        PlayerPrefs.Save();
    }

    public void SaveFOV(float newFOV)
    {
      
        if (fovPref != newFOV)
        {
            fovPref = newFOV;
            PlayerPrefs.SetFloat("setFOVPref", fovPref);
            if (SceneManager.GetActiveScene().buildIndex >= 1)
            {
                GameObject.Find("CharacterCamera").GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("setFOVPref");
            }
        }
        PlayerPrefs.Save();
    }
}
