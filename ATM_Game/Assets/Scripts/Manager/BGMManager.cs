using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Canvas 전환에 따라 배경음악을 변경하는 스크립트입니다.
/// </summary>
public class BGMManager : MonoSingleton<BGMManager>
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip BankClip;
    [SerializeField] private AudioClip RPGClip;


    public void PlayBankBGM() => PlayBGM(BankClip);
    public void PlayRPGBGM() => PlayBGM(RPGClip);

    private void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }
}
