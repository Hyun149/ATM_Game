using UnityEngine;

/// <summary>
/// UI 및 게임 효과음을 재생하는 효과음 매니저입니다.
/// </summary>
public class SFXManager : MonoSingleton<SFXManager>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip BankButtonClip;
    [SerializeField] private AudioClip RPGButtonClip;
    [SerializeField] private AudioClip ErrorClip;

    /// <summary>
    /// 버튼 클릭 시 호출하여 효과음을 재생합니다.
    /// </summary>
    public void PlayBankButton()
    {
        sfxSource.PlayOneShot(BankButtonClip);
    }

    public void PlayRPGButton()
    {
        sfxSource.PlayOneShot(RPGButtonClip);
    }

    public void ErrorSound()
    {
        sfxSource.PlayOneShot(ErrorClip);
    }
}
