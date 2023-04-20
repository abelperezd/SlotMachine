using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class UIManager : MonoBehaviour
{
    #region Fields and properties

    internal static UIManager Instance => _instance;
    private static UIManager _instance;

    [SerializeField] private TMP_Text _prizeText;
    [SerializeField] private TMP_Text _creditText;

    Animator _animator;

    #endregion

    #region Unit callbacks

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
        GameManager.Instance.OnGameStarted += UpdateCredit;
        GameManager.Instance.OnGameFinished += UpdateCredit;
        UpdateCredit();
    }

    #endregion

    #region Methods

    private void PlayPrizeAnimation(int prize, float duration)
    {
        _prizeText.text = prize.ToString();
        _animator.SetFloat("vel", duration);
        _animator.SetTrigger("play");
    }

    private void UpdateCredit()
    {
        _creditText.text = (GameManager.Instance.Credit.ToString());
    }

    #endregion
}
