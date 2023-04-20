using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button btn;
    private Image btnImage;


    public Sprite normal;
    public Sprite clicked;
    public Sprite disbled;


    public void OnPointerDown(PointerEventData eventData)
    {
        btnImage.sprite = clicked;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        btnImage.sprite = disbled;
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btnImage = btn.GetComponent<Image>();
        btn.onClick.AddListener(StartPressed);

        GameManager.Instance.OnGameFinished += EnableButton;
    }

    private void StartPressed()
    {
        GameManager.Instance.StartGame();
        btn.interactable = false;
    }

    private void EnableButton()
    {
        btn.interactable = true;
        btnImage.sprite = normal;
    }
}
