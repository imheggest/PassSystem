using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public List<TipItem> tipItems;
    public Transform tipItemsTF;
    public GameObject tipItemPrefab;

    internal void InitTip(PassOB.TipItem[] tip)
    {
        CleanOld();
        for (int i = 0; i < tip.Length; i++)
        {
            var tipItem = Instantiate(tipItemPrefab, tipItemsTF).GetComponent<TipItem>();
            tipItem.icon.sprite = tip[i].tipIcon;
            tipItem.textMeshProUGUI.text = tip[i].tipContent;
            tipItems.Add(tipItem);
        }


        this.gameObject.SetActive(false);
        
    }
    private void CleanOld() {
        for (int i = tipItemsTF.childCount-1; i >=0 ; i--)
        {
            DestroyImmediate(tipItemsTF.GetChild(i).gameObject);

        }
        tipItems.Clear();




    }
}
