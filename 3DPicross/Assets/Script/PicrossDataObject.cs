using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PicrossDataBase", menuName = "CreatePicrossDataBase")]
public class PicrossDataObject : ScriptableObject
{
    public List<Data> Picroses = new List<Data>();
}

[System.Serializable]
public class Data
{
    public string name;
    public Picross p;
}
