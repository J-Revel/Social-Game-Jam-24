using System.Collections;
using UnityEngine;

public enum MusicType
{
    Menu,
    Shop,
}

[System.Serializable]
public struct MusicSource
{
    public MusicType music;
    public AudioSource source;
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public MusicSource[] sources;
    public float transition_duration;
    private MusicType active_music;

    void Start()
    {
    }
    private void Awake()
    {
        instance = this;
    }

    public void PlayMusic(MusicType type)
    {
        StopAllCoroutines();
        StartCoroutine(PlayMusicCoroutine(type));
    }

    public IEnumerator PlayMusicCoroutine(MusicType music)
    {
        AudioSource previous_source = null;
        AudioSource target_source = null;
        for(int i=0; i< sources.Length; i++)
        {
            if (sources[i].music == music)
                target_source = sources[i].source;
            if (sources[i].music == active_music)
                previous_source = sources[i].source;
        }
        active_music = music;
        if(previous_source != null)
        {
            float previous_source_start_volume = previous_source.volume;
            for(float time=0; time < transition_duration; time+= Time.deltaTime)
            {
                target_source.volume = time / transition_duration;
                previous_source.volume = (1 - time / transition_duration) * previous_source_start_volume;
                yield return null;
            }
        }
        else
        {
            for(float time=0; time < transition_duration; time+= Time.deltaTime)
            {
                target_source.volume = time / transition_duration;
                yield return null;
            }
        }

        
    }

    void Update()
    {
        
    }
}
