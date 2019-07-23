using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMameger : MonoBehaviour
{
    public TextMeshProUGUI Music,Sound;
    public Slider Slider_Music, Slider_Sound;
    public AudioSource[] audios;
    // Start is called before the first frame update
    void Start()
    {
        audios = GameObject.Find("SoundManeger").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
