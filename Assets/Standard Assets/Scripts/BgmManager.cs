using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManager : MonoBehaviour {

    // Use this for initialization

    public static BgmManager Instance;
    public AudioClip MenuSound;
    public AudioClip VictorySound;
    public AudioClip FailedSound;
    private AudioSource m_sound_source;

    void Awake () {
        if (Instance != null)
            throw new UnityException("BgmManager.Instance!=null");
        Instance = this;
        DontDestroyOnLoad(this);

        m_sound_source = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += (s,mode) =>
          {
              if(s.name.Contains("menu")||s.name.Contains("room"))
              {
                  PlayMenuBgm();
              }
              else
              {
                  StopSound();
              }
          };
    }

    public void PlayMenuBgm()
    {
        if (m_sound_source.isPlaying) return;
        m_sound_source.clip = MenuSound;
        m_sound_source.Play();
    }

    public void PlayVictoryBgm()
    {
        if (m_sound_source.isPlaying) return;
        m_sound_source.clip = VictorySound;
        m_sound_source.Play();
    }

    public void PlayFailedBgm()
    {
        if (m_sound_source.isPlaying) return;
        m_sound_source.clip = FailedSound;
        m_sound_source.Play();
    }
    
    public void StopSound()
    {
        m_sound_source.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        

    }
}
