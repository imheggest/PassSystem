using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChessItem;

[CreateAssetMenu(menuName= "CreatePassOB", fileName ="PassOB")]
public class PassOB : ScriptableObject
{
    [Serializable]
    public class Chess {
        public string chessID;
        public ChessType chessType;
        public Sprite chessImage,chessBackground;
        public string chestContent;
        public TipItem[] tipItems;
       
    }
    [Serializable]
    public class PassItemOB {
        public string passItemID;
        public int playerNeedKey;//解锁需要的钥匙
        public Chess[] chesses;
    
    }
    [Serializable]
    public class TipItem
    {
        public Sprite tipIcon;
        public string tipContent;

    }
    [SerializeField]
    public PassItemOB[] passItemOBs;
}
