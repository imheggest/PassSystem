using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIManager : MonoBehaviour
{
    public PassMenu passMenu;
    public static TestUIManager Instance;
    private void Awake()
    {
        Instance = this;
        passMenu.InitItem();
    }
}
