using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public GameObject DifficultyToggles;
    //public Slider volumeSlider;

    public Slider musicSlider, soundSlider;

    public void Start()
    {
        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;
        audioMixer.GetFloat("Sound", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;

        resolutions = Screen.resolutions.Select(Resolution => new Resolution { width = Resolution.width, height = Resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Screen.fullScreen = true;

        DifficultyToggles.transform.GetChild((int)GameValues.difficulty).GetComponent<Toggle>().isOn = true;
    }

    #region Difficulty
    public void setEasyDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.difficulty = GameValues.Difficulties.Easy;
    }

    public void setMediumDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.difficulty = GameValues.Difficulties.Medium;
    }

    public void setHardDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.difficulty = GameValues.Difficulties.Hard;
    }
    #endregion

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("Sound", volume);
    }

    public void SetDialogueVolume(float volume)
    {
        audioMixer.SetFloat("Dialogue", volume);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
