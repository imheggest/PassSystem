using DG.Tweening;
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
    public PassKey passKey;
  
   

   
    
    #region passdata
    public class PassData
    {

        public int playerKey;//玩家的钥匙
      
        public int passNum;//通行证等级数
        public PassData() { }
        public PassData(int playerKey, int passNum)
        {
            this.playerKey = playerKey;
            this.passNum = passNum;

        }
    }
  
    public PassData passData;
    #endregion
    public override void InitItem()
    {
        base.InitItem();
        if (passItemsContentTestTF != null)
        {
            passItemsContentTestTF.gameObject.SetActive(false);
        }

        passData = ES3.Load("PassData", new PassData(0, 0));
        Debug.Log("通行证等级数：" + passData.passNum + "玩家的钥匙数：" + passData.playerKey);
        //初始化
        UpdataKeyCount();

        for (int i = 0; i < passItems.Count; i++)
        {
            if (passData.passNum >= i)
            {
                passItems[i].passItemState = PassItem.PassItemState.Unlock;
            }
            else
            {
                passItems[i].passItemState = PassItem.PassItemState.Lock;
            }
        }
        //

        for (int i = 0; i < passItems.Count; i++)
        {
            passItems[i].UpdataSlider();
        }
        //上锁
        if (passData.passNum + 1 <= passOB.passItemOBs.Length)
        {
            passItems[passData.passNum + 1].DirectClose();
        }



    }

    private void UpdataKeyCount()
    {
     
        if (passData.passNum + 1<passOB.passItemOBs.Length)
        {
            Debug.Log("当前在" + passData.passNum + "位置");
            passKey.count.text = passData.playerKey + "/" + passOB.passItemOBs[passData.passNum + 1].playerNeedKey;
        }
        else if (passData.passNum + 1 ==passOB.passItemOBs.Length)
        {
            Debug.Log("已经完全解锁");
            passKey.count.text = passData.playerKey + "/" +"***";
        }
        else
        {
            Debug.Log("已经完全解锁");
        }
       
    }
    public bool CanUseKey() {
        return passData.playerKey >= passOB.passItemOBs[passData.passNum].playerNeedKey && passOB.passItemOBs.Length > passData.passNum + 1;
       

    }
    #region Code
    /// <summary>
    /// 进度条动画完成
    /// </summary>
    void OnPassItemFullComplete(PassItem passItem) {
        Debug.Log(" 进度条动画完成");
        passItem.passItemState = PassItem.PassItemState.Unlock;
        passData.playerKey -= passOB.passItemOBs[passData.passNum + 1].playerNeedKey;

        passData.passNum++;
        UpdataKeyCount();
    }
    /// <summary>
    /// 进度条动画全部完成
    /// </summary>
    void OnAllPassItemFullComplete() {
        Debug.Log(" 进度条动画全部完成");

    }
    /// <summary>
    /// 开锁动画完成
    /// </summary>
    void OnOpenLock() {
        Debug.Log(" 开锁动画完成");
        UpdataKeyCount();
    }

    /// <summary>
    /// 关锁动画完成
    /// </summary>
    void OnCloseLock() {

        Debug.Log(" 关锁动画完成");
        UpdataKeyCount();
    }
    #endregion


    public override void OpenMenu()
    {
        base.OpenMenu();
        UpdataKeyCount();

    }

    #region Setting
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
            passItems[i].PassItemInit(passOB, i);

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
    #endregion



    public void AddKey()
    {

        passData.playerKey+=5;
        UpdataKeyCount();

        Debug.Log("passData.playerKey"+passData.playerKey+ "passData.passProgress"+ passData.passNum);
    }
    public void AddKey(int count)
    {

        passData.playerKey += count;
        UpdataKeyCount();
        Debug.Log("passData.playerKey" + passData.playerKey + "passData.passProgress" + passData.passNum);
    }
    public void JieSuo() {

        for (int i = 0; i < passItems.Count; i++)
        {
            if (passItems[i].passItemState==PassItem.PassItemState.Lock)
            {
               
                break;
            }
        }
    
    }
    public void Suo() {

        for (int i = 0; i < passItems.Count; i++)
        {
            if (passItems[i].passItemState == PassItem.PassItemState.Lock)
            {
           
                break;
            }
        }
    }
    public void Test111() {
        //AddKey(8);
        if (passData.passNum + 1>passOB.passItemOBs.Length)
        {
            Debug.Log("已经全部解锁");
            return;
        }
        int begin=passData.passNum+1;
      
        Debug.Log("Begin"+ begin);
        int end = passOB.passItemOBs.Length-1;
        int all= 0;
        for (int i = begin; i < passOB.passItemOBs.Length;)
        {
            all += passOB.passItemOBs[i].playerNeedKey;
            if (passData.playerKey>= all)
            {
                i++;
            }
            else
            {
                end = i;
                break;
            }
        }
        Debug.LogError("begin"+begin+"end"+end+ "passData.playerKey" +passData.playerKey+"all"+all);
        StartCoroutine(enumerator(begin, end));
    
    }
    IEnumerator enumerator() {
        //开
        PassItem ppp = passItems[0];
        ppp. lockIcon.DOFade(0, 1f);
        yield return ppp.lockBackGround.DOFade(0, 1f).WaitForCompletion();
        Tween myTween = ppp.leftDown.DOScale(new Vector3(0, 1, 1), 1f);
        ppp.rightDown.DOScale(new Vector3(0, 1, 1), 1f);
        yield return myTween.WaitForCompletion();
        //进度条
        PassItem qqq = passItems[0];
        yield return qqq.slider.DOValue(1, 2f).WaitForCompletion();
        //关
        PassItem eee = passItems[0];
        Tween myTween2 = eee.leftDown.DOScale(new Vector3(1, 1, 1), 1f);
        eee.rightDown.DOScale(new Vector3(1, 1, 1), 1f);

        yield return myTween2.WaitForCompletion();
        eee.lockIcon.DOFade(1, 0.1f);
        yield return eee.lockBackGround.DOFade(1, 0.1f).WaitForCompletion();
   
    }
    /// <summary>
    /// /
    /// </summary>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    IEnumerator enumerator(int begin, int end)
    {
        //开
        PassItem ppp = passItems[begin];
        ppp.lockIcon.DOFade(0, 1f);
        yield return ppp.lockBackGround.DOFade(0, 1f).WaitForCompletion();
        Tween myTween = ppp.leftDown.DOScale(new Vector3(0, 1, 1), 1f);
        ppp.rightDown.DOScale(new Vector3(0, 1, 1), 1f);
        yield return myTween.WaitForCompletion();
        OnOpenLock();
        //进度条
        for (int i = begin; i < end; i++)
        {
          
            PassItem qqq = passItems[i];
            yield return qqq.slider.DOValue(1, 2f).WaitForCompletion();
            
            OnPassItemFullComplete(qqq);
        }
        OnAllPassItemFullComplete();
        //关
        if (end<passItems.Count-1)
        {
            PassItem eee = passItems[end];
            Tween myTween2 = eee.leftDown.DOScale(new Vector3(1, 1, 1), 1f);
            eee.rightDown.DOScale(new Vector3(1, 1, 1), 1f);

            yield return myTween2.WaitForCompletion();
            eee.lockIcon.DOFade(1, 0.1f);
            yield return eee.lockBackGround.DOFade(1, 0.1f).WaitForCompletion();
            OnCloseLock();
        }
        else
        {
            PassItem qqq = passItems[end];
            yield return qqq.slider.DOValue(1, 2f).WaitForCompletion();
            OnPassItemFullComplete(qqq);
        }
      
     

    }
   
    
}
[CustomEditor(typeof(PassMenu))]
public class PassMenuEditor:Editor {
   
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       
        var PassMenu = (PassMenu)target;
        
     
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
            PassMenu.Test111();
        }
        if (GUILayout.Button("解锁"))
        {
            PassMenu.JieSuo();
        }
    }
}
