using UnityEngine;

public class SaveSoundVariables : MonoBehaviour
{


    float sensitivityPref;
    float volumePref;
    float musicVolumePref;
  
    public void SaveSensitivty(float newSensitivity)
    {
        if(sensitivityPref != newSensitivity)
        {
            sensitivityPref = newSensitivity;
            PlayerPrefs.SetFloat("setSensitivityPref", sensitivityPref);
        }
    }
    public void SaveVolume(float newVolume)
    {
        if (volumePref != newVolume)
        {
            volumePref = newVolume;
            PlayerPrefs.SetFloat("setVolumePref", volumePref);
        }
    }
    public void SaveMusicVolume(float newMusicVolume)
    {
        if (musicVolumePref != newMusicVolume)
        {
            musicVolumePref = newMusicVolume;
            PlayerPrefs.SetFloat("setMusicVolumePref", musicVolumePref);
        }
    }


}
