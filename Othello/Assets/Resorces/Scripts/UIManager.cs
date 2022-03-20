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
    [SerializeField]
    GameObject endprefab;
    [SerializeField]
    GameObject canvas;
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

    public void CreateGameEndUI()
    {
        GameObject createobj;
        createobj = Instantiate(endprefab);
        createobj.transform.parent = canvas.transform;
        createobj.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        createobj.GetComponent<RectTransform>().Rotate(90f, 90f, 0f);
        createobj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }
}
