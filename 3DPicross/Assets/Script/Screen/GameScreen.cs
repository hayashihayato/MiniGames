using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField]
    Text[] misstxt;
    [SerializeField]
    Material redmat;
    void Start()
    {
        Debug.Log("GameUIStart");
        for (int i = 0; i < misstxt.Length; i++)
        {
            misstxt[i].material.color = Color.white;
        }
    }
    public void AddMiss(int misscnt)
    {
        Debug.Log(misstxt[misscnt].name);
        misstxt[misscnt].material.color = Color.red;
    }
}
