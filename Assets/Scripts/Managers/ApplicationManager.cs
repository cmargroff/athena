using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(SaveLoadBehavior))]
public class ApplicationManager:MonoBehaviour
{
    public static ApplicationManager Instance;

    [SerializeField]
    private StorySO _loseGameStory;
    [SerializeField]
    private StorySO _startStory;
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
        Game
    }
}
