using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(SaveLoadBehavior))]
public class ApplicationManager:BaseMonoBehavior
{
    public static ApplicationManager Instance;

    [SerializeField]
    private StorySO _loseGameStory;
    [SerializeField]
    private StorySO _startStory;

    private SaveLoadBehavior _saveLoad;
    [SerializeField]
    private AudioMixer _mixer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _saveLoad = GetComponent<SaveLoadBehavior>();
        SafeAssigned(_mixer);

        SetMixer(_saveLoad.LoadSettings());
    }

    public void SetMixer(Settings settings)
    {
        SetMixer(SoundSources.Master, SoundHelper.ToDecibel(settings.MasterVolume));
        SetMixer(SoundSources.Music, SoundHelper.ToDecibel(settings.MusicVolume));
        SetMixer(SoundSources.Voice, SoundHelper.ToDecibel(settings.VoiceVolume));
        SetMixer(SoundSources.Effects, SoundHelper.ToDecibel(settings.EffectsVolume));
    }

    public void SetMixer(SoundSources mixer, float value)
    {
        _mixer.SetFloat($"{mixer}{SoundVariables.Volume}", SoundHelper.ToDecibel(value));
    }
    public enum SoundSources
    {
        Master,
        Music,
        Voice,
        Effects
    }
    public enum SoundVariables
    {
        Volume
    }
    public void StartApplication()
    {
        LoadScene(ScenesEnum.Story, () =>
        {
            var storyManager = FindAnyObjectByType<StoryManager>();
            storyManager.ConfigureStory(_startStory);
            storyManager.Completed.AddListener(() =>
            {
                LoadScene(ScenesEnum.MainMenu);
            });

        });
    }

    public void StartGame()
    {
        LoadScene(ScenesEnum.Game);
    }

    public void EndGameInLoss()
    {
        LoadScene(ScenesEnum.Story, () =>
        {
            var storyManager=FindAnyObjectByType<StoryManager>();
            storyManager.ConfigureStory(_loseGameStory);
            storyManager.Completed.AddListener(() =>
            {
                LoadScene(ScenesEnum.MainMenu);
            });

        });

    }

    public Settings LoadSettings()
    {
        return _saveLoad.LoadSettings();
    }
    public void SaveSettings(Settings settings)
    {
        _saveLoad.SaveSettings(settings);
    }
    private void LoadScene(ScenesEnum scene, Action onLoad = null)
    {
        var operation= SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
        if(onLoad!=null)
        {
            operation.completed += delegate (AsyncOperation asyncOperation) { onLoad(); };
        }
    }


    public enum ScenesEnum
    {
        Start,
        MainMenu,
        Story,
        Game,
        Settings
    }

    private Action _settingsCallback;
    public void OpenSettings(Action callback=null)
    {
        ApplicationManager.Instance._settingsCallback = callback;
        SceneManager.LoadSceneAsync(ScenesEnum.Settings.ToString(), LoadSceneMode.Additive);
    }
    public void CloseSettings()
    {
        SceneManager.UnloadSceneAsync(ScenesEnum.Settings.ToString()).completed += operation =>
        {
            ApplicationManager.Instance._settingsCallback?.Invoke();
        };
    }
}
