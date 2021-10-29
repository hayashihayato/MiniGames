using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PicrossDataBase", menuName = "CreatePicrossDataBase")]
public class PicrossDataObject : ScriptableObject
{
    public List<Picross> Picroses = new List<Picross>();
}
