using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionScreen : MonoBehaviour
{
    public AudioMixer theMixer;
    public GameObject audioSettings;
    public GameObject controlsSettings;

    public TMP_Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        audioSettings.SetActive(false);
        controlsSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenAudioSettings()
    {
        
        Debug.Log("pressed settings");
        controlsSettings.SetActive(false);
        audioSettings.SetActive(true);
    }

    public void OpenControlsSettings()
    {
        Debug.Log("pressed close settings");
        audioSettings.SetActive(false);
        controlsSettings.SetActive(true);
    }


    public void SetMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        theMixer.SetFloat("MasterVol", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void SetMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        theMixer.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVol()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
        theMixer.SetFloat("SFXVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }

}
