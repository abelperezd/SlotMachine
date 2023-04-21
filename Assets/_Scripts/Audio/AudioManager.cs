using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Fields
    [Header("Sources")]
    [SerializeField] private AudioSource _prizeSource;
    [SerializeField] private AudioSource _buttonSource;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> _prizeSounds;
    [SerializeField] private AudioClip _buttonSound;

    public static AudioManager Instance => _instance;
    private static AudioManager _instance;

    #endregion

    #region Unity callbacks

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _buttonSource.clip = _buttonSound;
    }

    #endregion

    #region Methods

    internal void PlayPrizeSound()
    {
        int randomIndex = Random.Range(0, _prizeSounds.Count);
        _prizeSource.clip = _prizeSounds[randomIndex];
        _prizeSource.Play();
    }

    internal void PlayButtonSound()
    {
        _buttonSource.Play();
    }

    #endregion
}
