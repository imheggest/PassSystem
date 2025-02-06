using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassKey : MonoBehaviour
{
    public TextMeshProUGUI count;
    public ProgressBar progressBar;
   public  void SetSuffix(string suffix) {
        progressBar.suffix = "/" + suffix;


    }
}
