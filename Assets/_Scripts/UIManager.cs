using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    Animator _animator;

    public TMP_Text prizeText;
    public TMP_Text scoreText;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PrizeManager.Instance.OnShowPrize += PlayPrizeAnimation;
    }

    private void PlayPrizeAnimation(int prize, float duration)
    {
        UpdateScore(prize);
        prizeText.text = prize.ToString();
        _animator.SetFloat("vel", duration);
        _animator.SetTrigger("play");
    }

    private void UpdateScore(int prize)
    {
        scoreText.text = (int.Parse(scoreText.text) + prize).ToString();
    }

}
