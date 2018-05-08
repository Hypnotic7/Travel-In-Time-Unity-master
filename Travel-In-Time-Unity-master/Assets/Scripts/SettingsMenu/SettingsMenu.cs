using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;


    //Gets all resolutions and assigns them to the resolution dropdown
    //Sets the Graphics Quality and current resolution and full screen
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        QualitySettings.SetQualityLevel(3);
        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;

        for (int i =0;i < resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        
        resolutionDropdown.AddOptions(options);

#if UNITY_STANDALONE_WIN
        resolutionDropdown.value = currentResolutionIndex;
#endif
#if UNITY_STANDALONE_OSX
        resolutionDropdown.value = currentResolutionIndex - 4;
        SetResolution(resolutionDropdown.value);
#endif
        resolutionDropdown.RefreshShownValue();
        Screen.fullScreen = true;
    }

    //Sets the volume in audio mixer
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", (volume/ 2.1f));
        Debug.Log(volume);
    }

    //Sets Quality by index provided
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Sets the screen to full screen or windowed
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //Sets the screen resolution by index provided
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
    
}
