using Michsky.MUIP;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PassItem : MonoBehaviour
{
    public enum PassItemState {
            Lock  , Unlock
    }
    public string passItemID;
    public PassItemState passItemState;

    public UnityAction sliderAniComplete;
    
    public GameObject chessItemPrefab;
    public Transform chessItemsTF;
    public List<ChessItem> chessItems = new List<ChessItem>();
    public Slider slider;
    public TextMeshProUGUI iconIndex;
    public LockBarAni barAni;
         
   

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
    private void OnEnable()
    {
        sliderAniComplete += ChangeStateUnlock;
    }
    private void OnDisable()
    {
        sliderAniComplete -= ChangeStateUnlock;
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
    private void ChangeStateUnlock()
    {
        passItemState = PassItemState. Unlock;
    }

    public void PlaySliderAni() {
        StartCoroutine(PlayAni());
    
    }
    IEnumerator PlayAni() {
        for (int i = 0; i < 101; i++)
        {
            if (slider.value < 0.99f)
            {
                slider.value += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
                slider.value = 1f;
                break;
            }
        }
        
        sliderAniComplete?.Invoke();


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
        if (GUILayout.Button("开始slider动画"))
        {
            PassItem.PlaySliderAni();
        }
        if (GUILayout.Button("开锁"))
        {
            PassItem.barAni.PlayAniOpen();
        }
        if (GUILayout.Button("关锁"))
        {
            PassItem.barAni.PlayAniClose();
        }
    }

}
