using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartButton : MonoBehaviour
{
    float Alpha;
    TextMeshProUGUI text_Pro;
    // Start is called before the first frame update
    void Start()
    {
       text_Pro = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Alpha = Mathf.Sin(Time.time * 2f) / 2 + 0.5f;
        StartCoroutine("ColorCoroutine");
    }

    IEnumerator ColorCoroutine()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();

            var color = text_Pro.color;

            color.a = Alpha;

            text_Pro.color = color;
        }
    }
}
