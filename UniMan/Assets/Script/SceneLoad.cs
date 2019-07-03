using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{

    private AsyncOperation async;

    [SerializeField] GameObject loadUI;

    [SerializeField] private Slider slider;

    public string SceneName;

    //static public SceneLoad instance;
    // Start is called before the first frame update
    /*
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }
    */
    void LoadScene(string Name)
    {
        loadUI.SetActive(true);
        StartCoroutine(LoadDeta(Name));
    }

    
    IEnumerator LoadDeta(string Name)
    {
        async = SceneManager.LoadSceneAsync(Name);

        while(!async.isDone)
        {
            slider.value = async.progress;
            yield return null;
        }

           
    }
}
