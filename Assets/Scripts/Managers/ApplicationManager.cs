using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager:MonoBehaviour
{
    public static ApplicationManager Instance;

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

    public void LoadScene(ScenesEnum scene, Action onLoad)
    {
        var operation= SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
        operation.completed+= delegate(AsyncOperation asyncOperation) { onLoad(); }; 
    }


    public enum ScenesEnum
    {
        Start,
        Story,
        Game
    }
}
