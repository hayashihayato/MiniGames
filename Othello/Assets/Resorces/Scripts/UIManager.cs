using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text playertxt;
    [SerializeField]
    Text cputxt;
    private Othello_Controll othello_Controll;
    private int[] diskcounter;

    void Start()
    {
        othello_Controll = this.GetComponent<Othello_Controll>();
        diskcounter = othello_Controll.Getdiskcount();
    }

    void Update()
    {
        playertxt.text = diskcounter[0].ToString();
        cputxt.text = diskcounter[1].ToString();
    }
}
