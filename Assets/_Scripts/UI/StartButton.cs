using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Fields

    [SerializeField] private Sprite _normal;
    [SerializeField] private Sprite _clicked;
    [SerializeField] private Sprite _disbled;

    private Button _btn;
    private Image _btnImage;

    #endregion

    #region Unity callbacks

    void Start()
    {
        _btn = GetComponent<Button>();
        _btnImage = _btn.GetComponent<Image>();
        _btn.onClick.AddListener(StartPressed);

        GameManager.Instance.OnGameFinished += EnableButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _btnImage.sprite = _clicked;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _btnImage.sprite = _disbled;
    }


    #endregion

    #region Methods

    private void StartPressed()
    {
        AudioManager.Instance.PlayButtonSound();
        GameManager.Instance.StartGame();
        _btn.interactable = false;
    }

    private void EnableButton()
    {
        _btn.interactable = true;
        _btnImage.sprite = _normal;
    }

    #endregion
}
