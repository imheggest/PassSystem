using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIManager : MonoBehaviour
{
    public PassMenu passMenu;
    private void Awake()
    {
        passMenu.InitItem();
    }
}
