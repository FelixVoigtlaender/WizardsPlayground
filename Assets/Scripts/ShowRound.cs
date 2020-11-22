using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowRound : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public string prefix;
    public string suffix;

    public void Start()
    {
        text.text = prefix +  GameManager.rounds + suffix;
    }
}
