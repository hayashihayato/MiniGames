using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField]
    Text cputext;
    [SerializeField]
    Text playertext;
    private Othello_Controll oc;
    private int[] diskcounter;
    void Start()
    {
        oc = GameObject.Find("Othello_Controller").GetComponent<Othello_Controll>();
        diskcounter = oc.Getdiskcount();
        cputext.text = diskcounter[1].ToString();
        playertext.text = diskcounter[0].ToString();
    }
    public void StartGameScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
