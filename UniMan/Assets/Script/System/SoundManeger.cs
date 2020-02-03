using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManeger : MonoBehaviour
{
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
        audioSource[0].volume = 0.8f;
        audioSource[1].volume = 0.5f;
        audioSource[2].volume = 0.5f;
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

    public void SoundPlayer(AudioClip Sound, float Pitch)
    {
        audioSource[1].pitch = Pitch;
        audioSource[1].PlayOneShot(Sound);
    }
    public void SoundEnemy(AudioClip Sound, float Pitch)
    {
        audioSource[2].pitch = Pitch;
        audioSource[2].PlayOneShot(Sound);
    }
}
