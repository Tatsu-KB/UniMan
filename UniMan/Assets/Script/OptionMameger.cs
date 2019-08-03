using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMameger : MonoBehaviour
{
    public TextMeshProUGUI Music,Sound,Exit,MusicText,SoundText;
    public Slider Slider_Music, Slider_Sound;
    public AudioSource[] audios;
    public int ButtonNum;
    const int Max = 2;
    public AudioClip BackMusic, BackSound;
    bool AxisReset = false,ExitFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        audios = GameObject.Find("SoundManeger").GetComponents<AudioSource>();
        ButtonNum = Max;
        SoundManeger.instance.Music(BackMusic,1.0f);
        Slider_Music.value = Mathf.FloorToInt(audios[0].volume * 100);
        Slider_Sound.value = Mathf.FloorToInt(audios[1].volume * 100);
    }

    // Update is called once per frame
    void Update()
    {
        if(ExitFlag)
        {
            StartCoroutine(ButtonSelect());

            if (Input.GetAxis("Vertical") != 0.0f && !AxisReset)
            {

                if (Input.GetAxis("Vertical") <= 0.0f && ButtonNum > 0)
                {
                    ButtonNum--;
                }
                if (Input.GetAxis("Vertical") >= 0.0f && ButtonNum < Max)
                {
                    ButtonNum++;
                }
                AxisReset = true;
            }
            if (Input.GetAxis("Vertical") == 0.0f && AxisReset)
            {
                AxisReset = false;
            }

            if (Input.anyKeyDown && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && ExitFlag && ButtonNum == 0)
            {
                //StartCoroutine(Select());
                SceneLoad.instance.LoadScene("MainTitle");
                SoundManeger.instance.Stop();
                ExitFlag = false;
            }
            ButtonNum = Mathf.Clamp(ButtonNum, 0, Max);
        }
        Slider_Music.value = Mathf.FloorToInt(Slider_Music.value);
        Slider_Sound.value = Mathf.FloorToInt(Slider_Sound.value);
        MusicText.text = Slider_Music.value.ToString();
        SoundText.text = Slider_Sound.value.ToString();

        if(ButtonNum != 0 && Input.GetAxisRaw("Horizontal") != 0)
        {
            
        }
    }

    IEnumerator ButtonSelect()
    {
        yield return new WaitForSeconds(0.05f);
        switch (ButtonNum)
        {
            case 2:
                Music.color = new Color(0, 1, 1, 1);
                Sound.color = new Color(1, 1, 1, 1);
                Exit.color  = new Color(1, 1, 1, 1);
                StartCoroutine("SliderValue");
                break;
            case 1:
                Music.color = new Color(1, 1, 1, 1);
                Sound.color = new Color(0, 1, 1, 1);
                Exit.color = new Color(1, 1, 1, 1);
                StartCoroutine("SliderValue");
                break;
            case 0:
                Music.color = new Color(1, 1, 1, 1);
                Sound.color = new Color(1, 1, 1, 1);
                Exit.color  = new Color(0, 1, 1, 1);
                break;
        }
    }

    IEnumerator SliderValue()
    {
        yield return new WaitForSeconds(0.5f);

        switch (ButtonNum)
        {
            case 2:
                Slider_Music.value += Input.GetAxisRaw("Horizontal");
                audios[0].volume = Slider_Music.value / 100;
                break;
            case 1:
                Slider_Sound.value += Input.GetAxisRaw("Horizontal");
                audios[1].volume = Slider_Sound.value / 100;
                break;
        }
    }
}
