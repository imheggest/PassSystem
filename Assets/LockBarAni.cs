using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LockBarAni : MonoBehaviour
{
    public Transform leftDown, rightDown;
    public Image lockIcon, lockBackGround;
    public void PlayAniOpen()
    {

        StartCoroutine(OpenLock());
    }
    IEnumerator OpenLock()
    {
        lockIcon.DOFade(0, 1f);
        yield return lockBackGround.DOFade(0, 1f).WaitForCompletion();
        Tween myTween = leftDown.DOScale(new Vector3(0, 1, 1), 1f);
        rightDown.DOScale(new Vector3(0, 1, 1), 1f);

        yield return myTween.WaitForCompletion();
      


        Debug.Log("Tween completed!");
    }
    public void PlayAniClose()
    {
        StartCoroutine(CloseLock());


    }
    IEnumerator CloseLock()
    {
        Tween myTween = leftDown.DOScale(new Vector3(1, 1, 1), 1f);
        rightDown.DOScale(new Vector3(1, 1, 1), 1f);

        yield return myTween.WaitForCompletion();
        lockIcon.DOFade(1, 0.1f);
        yield return lockBackGround.DOFade(1, 0.1f).WaitForCompletion();


        Debug.Log("Tween completed!");
    }
    public void DirectOpen() {
        Color colorLockIcon = lockIcon.color;
        lockIcon.color =new Color(colorLockIcon.r, colorLockIcon.g, colorLockIcon.b,0);
        Color colorLockBackGround = lockBackGround.color;
        lockBackGround.color = new Color(colorLockBackGround.r, colorLockBackGround.g, colorLockBackGround.b, 0);
        leftDown.localScale = new Vector3(0,1,1);
        rightDown.localScale = new Vector3(0, 1, 1);
    }
    public void DirectClose() {
        Color colorLockIcon = lockIcon.color;
        lockIcon.color = new Color(colorLockIcon.r, colorLockIcon.g, colorLockIcon.b, 100);
        Color colorLockBackGround = lockBackGround.color;
        lockBackGround.color = new Color(colorLockBackGround.r, colorLockBackGround.g, colorLockBackGround.b, 100);
        leftDown.localScale = new Vector3(1, 1, 1);
        rightDown.localScale = new Vector3(1, 1, 1);


    }
}
[CustomEditor(typeof(LockBarAni))]
public class LockBarAniEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var passItemOpenAni = (LockBarAni)target;
        if (GUILayout.Button("²¥·Åopen"))
        {
            passItemOpenAni.PlayAniOpen();
        }
        if (GUILayout.Button("²¥·Åclose"))
        {
            passItemOpenAni.PlayAniClose();
        }
        if (GUILayout.Button("open"))
        {
            passItemOpenAni.DirectOpen();
        }
        if (GUILayout.Button("close"))
        {
            passItemOpenAni.DirectClose();
        }

    }


}