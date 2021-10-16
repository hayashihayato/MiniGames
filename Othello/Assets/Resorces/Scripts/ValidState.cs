using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidState : MonoBehaviour
{
    [SerializeField]
    private Othello_Controll othello_controller;

    public int[] pos = new int[2];

    private void Start()
    {
        othello_controller = GameObject.Find("Othello_Controller").GetComponent<Othello_Controll>();
    }

    private void OnMouseDown()
    {
        othello_controller.ClickValid(pos[0], pos[1]);
    }
}
