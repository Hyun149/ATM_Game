using UnityEngine;

/// <summary>
/// 씬 집입 시 배경음악을 자동으로 재생합니다.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var audio = GetComponent<AudioSource>();
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }
}
