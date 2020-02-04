using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{

    private AsyncOperation async;
    public string SceneName,BackName;

    static public SceneLoad instance;

    private Texture2D blackTexture;
    private float fadeAlpha = 0;
    private bool Fading = false;
    public int DeathCount;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
            this.blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
            this.blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
            this.blackTexture.SetPixel(0, 0, Color.white);
            this.blackTexture.Apply();

        }
        else
        {
            Destroy(gameObject);
        }



    }

    private void OnPostRender()
    {
    }
    public void LoadScene(string Name)
    {
        if (Name == "GameOver")
        {
            DeathCount++;
        }
        
        BackName = SceneName;
        SceneName = Name;
        StartCoroutine(LoadDeta(Name,0.8f));
    }

    public void OnGUI()
    {
        if (Fading)
        {  
            //透明度を更新して黒テクスチャを描画
            GUI.color = new Color(0, 0, 0, this.fadeAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.blackTexture);
        }
    }


    IEnumerator LoadDeta(string Name, float interval)
    {

        this.Fading = true;
        float time = 0.0f;

        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        SceneManager.LoadScene("Loading");
        yield return new WaitForSeconds(1.0f / 60.0f);

        this.fadeAlpha = 0;

        
        async = SceneManager.LoadSceneAsync(Name);
        async.allowSceneActivation = false;
        while (async.progress < 0.9f) yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.5f);

        fadeAlpha = 1f;
        async.allowSceneActivation = true;

        time = 0.0f;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
        
        Fading = false;
    }
}
