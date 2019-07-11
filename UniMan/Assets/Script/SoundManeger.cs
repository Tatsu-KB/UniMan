using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManeger : MonoBehaviour
{
    public AudioClip BackMusic,SE;
    public AudioSource[] audioSource;
    static public SoundManeger instance;
    // Start is called before the first frame update
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

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Music(AudioClip Music, float Pitch)
    {
        audioSource[0].clip = Music;
        audioSource[0].pitch = Pitch;
        audioSource[0].Play();
    }
    public void Stop()
    {
        audioSource[0].Stop();
    }

    public void Sound(AudioClip Sound)
    {
        SE = Sound;
        audioSource[1].PlayOneShot(SE);
    }
}
