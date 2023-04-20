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

    private Button btn;
    private Image btnImage;

    #endregion

    #region Unity callbacks

    void Start()
    {
        btn = GetComponent<Button>();
        btnImage = btn.GetComponent<Image>();
        btn.onClick.AddListener(StartPressed);

        GameManager.Instance.OnGameFinished += EnableButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        btnImage.sprite = _clicked;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        btnImage.sprite = _disbled;
    }


    #endregion

    #region Methods

    private void StartPressed()
    {
        GameManager.Instance.StartGame();
        btn.interactable = false;
    }

    private void EnableButton()
    {
        btn.interactable = true;
        btnImage.sprite = _normal;
    }

    #endregion
}
