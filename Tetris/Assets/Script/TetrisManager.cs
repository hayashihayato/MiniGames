using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] tetmino;
    [SerializeField]
    GameObject Minos;

    public void CreateMino()
    {
        GameObject createobj;
        var rnd = Random.Range(0,7);
        createobj = Instantiate(tetmino[rnd], new Vector3(0f,20f,0f), Quaternion.identity);
        createobj.transform.parent = Minos.transform;
    }
}
