using UnityEngine;

public class PlayMusicOnEnable : MonoBehaviour
{
    public MusicType enter_music;
    public MusicType leave_music;
    void Start()
    {
        MusicManager.instance.PlayMusic(enter_music);
    }
    private void OnDestroy()
    {
        
        MusicManager.instance.PlayMusic(leave_music);
    }

    void Update()
    {
        
    }
}