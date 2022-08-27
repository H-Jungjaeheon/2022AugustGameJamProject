using UnityEngine;

    /// </summary>
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoSingle<BackgroundMusic>
{
    [SerializeField]
    private bool loop = true;

    [SerializeField]
    AudioClip bgm;

    AudioSource audio;
    override protected void Awake()
    {
        base.Awake();
        audio = GetComponent<AudioSource>();
        if (bgm != null)
            audio.clip = bgm;
        //DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("music_enabled"))
        {
            var musicEnabled = PlayerPrefs.GetInt("music_enabled");
            if (musicEnabled == 0)
                audio.mute = true;
            else
                audio.loop = loop;
        }
        audio.Play();
    }

    override protected void OnDestroy()
    {
        base.OnDestroy();
        Stop();

    }

    public void Stop()
    {
        audio.Stop();
    }
}
