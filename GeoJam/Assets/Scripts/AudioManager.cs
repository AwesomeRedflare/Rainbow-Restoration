using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public static GameObject song;

    private void Awake()
    {
        if(instance == null )
        {
            instance = this;
        }
        else
        {
            if(tag != "Menu")
            {
                Destroy(gameObject);
                return;
            }
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Main Menu")
        {
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = s.group;
            s.source.pitch = s.pitch;
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Main Menu")
        {
            Play("Song");
        }
    }

    public void Destroy()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main Menu" && gameObject.tag != "Menu")
        {
            Debug.Log("why");
            Destroy(gameObject);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
}
//use this to play a sound.
//FindObjectOfType<AudioManager>().Play("name");
