using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChessItem : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public enum ChessType {Normal, Advanced }
    public ChessType chessType;
    public Button getChess;
    public Image icon, iconBackGround;
    public Transform maskTF;
    public Tip tip;
    public TextMeshProUGUI IconText;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("1111");
        tip.gameObject.SetActive(true);
        if (tip.tipItems.Count==0)
        {
          
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tip.gameObject.SetActive(false);
    }
}
