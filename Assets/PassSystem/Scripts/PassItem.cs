using Michsky.MUIP;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class PassItem : MonoBehaviour
{
    public enum PassItemState {
            Lock  , Unlock
    }
    public string passItemID;
    public PassItemState passItemState;

   
    public GameObject chessItemPrefab;
    public Transform chessItemsTF;
    public List<ChessItem> chessItems = new List<ChessItem>();
    public Slider slider;
    public TextMeshProUGUI iconIndex;
  
  
    //Ìõ
    public Transform leftDown, rightDown;
    public Image lockIcon, lockBackGround;
  

    public void PassItemInit(PassOB passOB,int index) {
        passItemID= passOB.passItemOBs[index].passItemID;
        iconIndex.text = (index + 1).ToString();
        CleanChessItemsTF();
        var chess = passOB.passItemOBs[index].chesses;
        for (int i = 0; i < chess.Length; i++)
        {
            var gb = Instantiate(chessItemPrefab, chessItemsTF);
            var chessItem= gb.GetComponent<ChessItem>();
          
            chessItem.chessType = chess[i].chessType;
            chessItem.icon.sprite = chess[i].chessImage;
            chessItem.iconBackGround.sprite = chess[i].chessBackground;
            chessItem.IconText.text = chess[i].chestContent;
            
            chessItem.tip.InitTip(chess[i].tipItems);
            chessItems.Add(chessItem);
        }
      






    }
   
    private void CleanChessItemsTF() {

        for (int i = chessItemsTF.childCount-1; i >= 0; i--)
        {
            DestroyImmediate(chessItemsTF.GetChild(i).gameObject);
        }
        chessItems.Clear();
    }
    public bool CanUnLock() {
      
        return passItemState== PassItemState.Lock;
    }
    public void ChangeStateUnlock()
    {
        passItemState = PassItemState. Unlock;
    }
    public void ChangeStateLock()
    {
        passItemState = PassItemState.Lock;
    }

  
   
    public void DirectOpen()
    {
        Color colorLockIcon = lockIcon.color;
        lockIcon.color = new Color(colorLockIcon.r, colorLockIcon.g, colorLockIcon.b, 0);
        Color colorLockBackGround = lockBackGround.color;
        lockBackGround.color = new Color(colorLockBackGround.r, colorLockBackGround.g, colorLockBackGround.b, 0);
        leftDown.localScale = new Vector3(0, 1, 1);
        rightDown.localScale = new Vector3(0, 1, 1);
    }
    public void DirectClose()
    {
        Color colorLockIcon = lockIcon.color;
        lockIcon.color = new Color(colorLockIcon.r, colorLockIcon.g, colorLockIcon.b, 100);
        Color colorLockBackGround = lockBackGround.color;
        lockBackGround.color = new Color(colorLockBackGround.r, colorLockBackGround.g, colorLockBackGround.b, 100);
        leftDown.localScale = new Vector3(1, 1, 1);
        rightDown.localScale = new Vector3(1, 1, 1);


    }
    public void UpdataSlider() {
        if (passItemState==PassItemState.Unlock)
        {
            slider.value=1f;
        }
        else
        {
            slider.value = 0f;
        }
    
    }
}
[CustomEditor(typeof(PassItem))]
public class PassItemEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var PassItem = (PassItem)target;
       
    }

}
