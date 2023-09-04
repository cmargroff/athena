using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplicationManager:MonoBehaviour
{
    public static ApplicationManager Instance;

    [SerializeField]
    private StorySO _loseGameStory;

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


    private void LoadScene(ScenesEnum scene)
    {
        LoadScene(scene, null);
    }

    private void LoadScene(ScenesEnum scene, Action onLoad)
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
