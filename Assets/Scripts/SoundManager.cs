using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundManager : MonoBehaviour {

    new AudioSource audio;
    public AudioSource musicAudio;
    SpeechManager speechManager;

    float musicDefaultvolume;
    public float reducedMusicVolumeRatio;

    //Sound effects
    public AudioClip focusPieceClip;
    public AudioClip unfocusPieceClip;
    public AudioClip splitClip;
    public AudioClip unsplitClip;

    //voice tutorial
    public Dictionary<string, VoiceTutoClip> voiceTuto = new Dictionary<string, VoiceTutoClip>();
    public DicAudioClip[] voicetutoTempDic;


    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        speechManager = FindObjectOfType<SpeechManager>();
        musicDefaultvolume = musicAudio.volume;

        //Make the dictionary
        for (int i=0;i<voicetutoTempDic.Length;i++)
        {
            voiceTuto.Add(voicetutoTempDic[i].name, new VoiceTutoClip(voicetutoTempDic[i].audioClip));
        }

        PlayVoiceTuto("Welcome");
    }


    //_________Sound Effects functions________________________
    //  start by receiving messages from controller
    public void SplitObject()
    {
        StartCoroutine(DelayedPlayVoiceTuto("Info", 2));
        audio.PlayOneShot(splitClip);
    }

    public void UnsplitObject()
    {
        audio.Stop();
        audio.PlayOneShot(unsplitClip);
    }

    public void FocusPiece()
    {
        StartCoroutine(DelayedPlayVoiceTuto("Focus", 2));
        audio.PlayOneShot(focusPieceClip);
    }

    public void LeaveFocus()
    {
        audio.Stop();
        audio.PlayOneShot(unfocusPieceClip);
    }

    public void PlayVoiceTuto(string audioClipName)
    {
        VoiceTutoClip clip = voiceTuto[audioClipName];
        if (clip.played)
            return;
        else
        {
            audio.clip = clip.clip;
            clip.played = true;
            audio.Play();
            StartCoroutine(LowerMusicVolume());
        }

        if (audioClipName == "Welcome")
            FindObjectOfType<Cursor>().SendMessage("ShowHandDelayed");
    }

    public void DelayedPlayVoiceTutoFunction(string audioClipName,float time)
    {
        StartCoroutine(DelayedPlayVoiceTuto(audioClipName, time));
    }

    IEnumerator DelayedPlayVoiceTuto(string audioClipName, float time)
    {
        audio.Stop();
        yield return new WaitForSeconds(time);
        PlayVoiceTuto(audioClipName);
    }

    IEnumerator LowerMusicVolume()
    {
        Debug.Log("Lower music volume");
        //target volume
        float loweredVolume = musicDefaultvolume / reducedMusicVolumeRatio;

        //Check if the music's volume is not already reduced
        if (musicAudio.volume == loweredVolume)
            yield break;

        //Reduce the volume over time
        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime*2;
            musicAudio.volume = Mathfx.Hermite(musicDefaultvolume, loweredVolume, t);

            yield return 0;
        }

        StartCoroutine(CheckIfClipEnded());
    }

    //Check every frame is a clip is playing
    IEnumerator CheckIfClipEnded()
    {
        while(true)
        {
            yield return 0;
            if(!audio.isPlaying)
            {
                //If it stoppped, up the music volume
                StartCoroutine(UpMusicVolume());
                break;
            }
        }
    }

    IEnumerator UpMusicVolume()
    {
        Debug.Log("Up music volume");
        //Current volume
        float loweredVolume = musicAudio.volume;

        //Check if the volume isn't already high
        if (musicAudio.volume == musicDefaultvolume)
            yield break;

        //Up the volume over time
        float t = 1;
        while (t>0)
        {
            t -= Time.deltaTime * 2;
            musicAudio.volume = Mathfx.Hermite(musicDefaultvolume, loweredVolume, t);

            yield return 0;
        }
    }

    [Serializable]
    public struct DicAudioClip
    {
        public string name;
        public AudioClip audioClip;
    }

    public class VoiceTutoClip
    {
        public VoiceTutoClip(AudioClip audioClip)
        {
            clip = audioClip;
            played = false;
        }

        public AudioClip clip;
        public bool played;
    }
}

