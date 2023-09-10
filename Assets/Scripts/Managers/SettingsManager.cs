using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : BaseMonoBehavior
{
    private ApplicationManager _applicationManager;

    [SerializeField]
    private Scrollbar _masterVolume;

    [SerializeField]
    private Scrollbar _musicVolume;

    [SerializeField]
    private Scrollbar _voiceVolume;

    [SerializeField]
    private Scrollbar _effectsVolume;

    private void Start()
    {
        _applicationManager = SafeFindObjectOfType<ApplicationManager>();
        SafeAssigned(_masterVolume);
        SafeAssigned(_musicVolume);
        SafeAssigned(_voiceVolume); 
        SafeAssigned(_effectsVolume);

        var settings=_applicationManager.LoadSettings();
        settings ??= new Settings(){MusicVolume = 1,VoiceVolume = 1,EffectsVolume = 1, MasterVolume = 1};
        SetSettingsControls(settings);
    }

    private void SetSettingsControls( Settings settings)
    {
        _masterVolume.value=settings.MasterVolume;
        _musicVolume.value=settings.MusicVolume;
        _voiceVolume.value=settings.VoiceVolume;
        _effectsVolume.value=settings.EffectsVolume;


    }
    private Settings ReadSettingsControls()
    {
        var settings = new Settings
        {
            MasterVolume = _masterVolume.value,
            MusicVolume = _musicVolume.value,
            VoiceVolume = _voiceVolume.value,
            EffectsVolume = _effectsVolume.value
        };
        return settings;
    }

    public void OnMasterVolumeChange(float value)//these have to be here, or the event won't work
    {
        _applicationManager.SetMixer(ApplicationManager.SoundSources.Master, _masterVolume.value);
    }
    public void OnMusicVolumeChange(float value)
    {
        _applicationManager.SetMixer(ApplicationManager.SoundSources.Music, _musicVolume.value);
    }
    public void OnEffectsVolumeChange(float value)
    {
        _applicationManager.SetMixer(ApplicationManager.SoundSources.Effects, _effectsVolume.value);
    }
    public void OnVoiceVolumeChange(float value)
    {
        _applicationManager.SetMixer(ApplicationManager.SoundSources.Voice, _voiceVolume.value);
    }

    public void OnSaveButton()
    {
        var settings = ReadSettingsControls();
        _applicationManager.SaveSettings(settings);
        _applicationManager.CloseSettings();
    }
    public void OnBackButton()
    {
        var settings= _applicationManager.LoadSettings();
        _applicationManager.SetMixer(settings);
        _applicationManager.CloseSettings();
    }
}
