using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    public UnityAction<string,bool> saveGet;
    public string chessID;//根据ID获得奖品
    public void InitChessItem() {

        getChess.onClick.AddListener(
            ()=> {
                Debug.Log("获得"+ chessID);
                getChess.gameObject.SetActive(false);
                saveGet?.Invoke(chessID,true);
            }
            );


    }
  
    /// <summary>
    /// 锁宝箱
    /// </summary>
    public void LockChessItem() {

        getChess.gameObject.SetActive(false);
    }
    /// <summary>
    /// 解锁宝箱
    /// </summary>
    public void UnlockChessItem() {
        getChess.gameObject.SetActive(true);
    }
    /// <summary>
    /// 打开宝箱
    /// </summary>
    public void OpenChess() { }
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
