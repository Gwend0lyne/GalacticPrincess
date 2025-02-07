using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI text;


    public void UpdateLapText(string message)
    {
        text.text = message;
    }

}
