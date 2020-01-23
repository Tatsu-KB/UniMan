using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    //　非同期動作で使用するAsyncOperation
    private AsyncOperation async;
    //　シーンロード中に表示するUI画面
    [SerializeField]
    private GameObject loadUI;
    //　読み込み率を表示するスライダー
    [SerializeField]
    private Slider slider;
    SceneLoad load;
    // Start is called before the first frame update
    /*
    void Start()
    {
        load = GameObject.Find("LoadManeger").GetComponent<SceneLoad>();
        loadUI.SetActive(true);

        StartCoroutine("LoadData");
    }
    
    IEnumerator LoadData()
    {
        
        Debug.Log(slider.value);
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync(load.SceneName);
       

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
        
    }
    */
}
