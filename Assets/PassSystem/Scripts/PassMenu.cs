using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PassMenu : MenuBase
{

    public PassOB passOB;
    public GameObject passItemBeginPrefab, passItemMiddlePrefab, passItemEndPrefab;
    public Transform passItemContentTF;
    public List<PassItem> passItems;
    public Transform passItemsContentTestTF;
  

    #region action
    public UnityAction passItemFullComplete;


    #endregion
    #region passdata
    public class PassData
    {

        public int playerKey;//玩家的钥匙
        public int passProgress;//玩家在第几层

        public PassData() { }
        public PassData(int playerKey, int passProgress)
        {
            this.playerKey = playerKey;
            this.passProgress = passProgress;

        }
    }
    public PassData passData;
    #endregion
    public override void InitItem()
    {
        base.InitItem();
        if (passItemsContentTestTF!=null)
        {
            passItemsContentTestTF.gameObject.SetActive(false);
        }

        passData= ES3.Load("PassData",new PassData(0,0));
        Debug.Log("玩家在"+passData.passProgress+"玩家的钥匙数"+ passData.playerKey);
        //初始化
        for (int i = 0; i < passItems.Count; i++)
        {
           // passItems[i].PassItemInit();
            passItems[i].sliderAniComplete += TestA;
            passItems[i].barAni.lockBarOpen += BarOpenComplete;
            passItems[i].barAni.lockBarClose += BarCloseComplete;
        }
      
    }
    public override void OpenMenu()
    {
        base.OpenMenu();
        for (int i = 0; i < passItems.Count; i++)
        {
            if (passData.passProgress>= i)
            {
                passItems[i].passItemState = PassItem.PassItemState.Unlock;
            }
            else
            {
                passItems[i].passItemState = PassItem.PassItemState.Lock;
            }
        }
        for (int i = 0; i < passItems.Count; i++)
        {
            passItems[i].UpdataSlider();
        }
        //上锁
        if (passData.passProgress + 1<=passOB.passItemOBs.Length)
        {
            passItems[passData.passProgress + 1].barAni.DirectClose();
        }
      
    }
    public void BarOpenComplete() {
        Debug.Log("BarOpenComplete");
    }
    public void BarCloseComplete()
    {

        Debug.Log("BarCloseComplete");
    }
    public void CreatePassItem()
    {
        CleanOld();
        CreateNew();
        
    }
    /// <summary>
    /// 
    /// </summary>
    public void InitPassItem()
    {
        for (int i = 0; i < passItems.Count; i++)
        {
            passItems[i].PassItemInit(passOB,i );
           
        }
    }
    /// <summary>
    ///  创建passitem
    /// </summary>
    private void CreateNew()
    {
        for (int i = 0; i < passOB.passItemOBs.Length; i++)
        {
            if (i == 0)
            {
                var passItemBeginGB = Instantiate(passItemBeginPrefab, passItemContentTF);
                var passItemBegin = passItemBeginGB.GetComponent<PassItem>();
                passItems.Add(passItemBegin);
                continue;
            }
            if (i == passOB.passItemOBs.Length - 1)
            {
                var passItemEndGB = Instantiate(passItemEndPrefab, passItemContentTF);
                var passItemEnd = passItemEndGB.GetComponent<PassItem>();
                passItems.Add(passItemEnd);
                continue;
            }
            var passItemMiddleGB = Instantiate(passItemMiddlePrefab, passItemContentTF);
            var passItemMiddle = passItemMiddleGB.GetComponent<PassItem>();
            passItems.Add(passItemMiddle);
        }
    }

    public void CleanOld()
    {
        for (int i = passItemContentTF.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(passItemContentTF.GetChild(i).gameObject);
        }
        passItems.Clear();
    }

    public void PlayPassItemAni() {

        for (int i = 0; i < passItems.Count; i++)
        {
            if (passItems[i].CanUnLock())
            {
                passItems[i].PlaySliderAni();
                Debug.Log(i);
                break;
            }
        }
    }
    public void TestA() {

        for (int i = 0; i < passItems.Count; i++)
        {
            if (passItems[i].CanUnLock())
            {
                if (passData.passProgress>= i)
                {
                    PlayPassItemAni();
                } 
                break;
            }
        }
        Debug.Log("TestA");
     
     
        // PlayPassItemAni();
    }
    public void AddKey()
    {

        passData.playerKey+=5;
        while (passData.playerKey >= passOB.passItemOBs[passData.passProgress].playerNeedKey && passOB.passItemOBs.Length > passData.passProgress + 1)
        {
            passData.playerKey -= passOB.passItemOBs[passData.passProgress].playerNeedKey;
            passData.passProgress++;
        }
        Debug.Log("passData.playerKey"+passData.playerKey+ "passData.passProgress"+ passData.passProgress);
    }
    public void UpdateSlider() {
      
      
     
       
        Debug.Log(" passData.playerKey++");
        PlayPassItemAni();

    }
    
}
[CustomEditor(typeof(PassMenu))]
public class PassMenuEditor:Editor {
   
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       
        var PassMenu = (PassMenu)target;
        
        if (GUILayout.Button("开始slider动画"))
        {
            PassMenu.PlayPassItemAni();
        }
        if (GUILayout.Button("创建PassItem"))
        {
            PassMenu.CreatePassItem();
        }
        if (GUILayout.Button("配置PassItem"))
        {
            PassMenu.InitPassItem();
        }
        if (GUILayout.Button("删除全部PassItem"))
        {
            PassMenu.CleanOld();
        }
        if (GUILayout.Button("添加Key" +
            "10个"))
        {
            PassMenu.AddKey();
        }
        if (GUILayout.Button("更新"))
        {
            PassMenu.UpdateSlider();
        }
    }
}
