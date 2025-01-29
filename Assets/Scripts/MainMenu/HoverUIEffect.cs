using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverUIEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject UISelectedEffect;
    [SerializeField] private TextMeshProUGUI UIText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UISelectedEffect.SetActive(true);
        UIText.color = new Color(1, 0.5f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UISelectedEffect.SetActive(false);
        UIText.color = Color.white;
    }
}
