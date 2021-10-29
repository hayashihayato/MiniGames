using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Picross", menuName = "CreatePicross")]
public class Picross : ScriptableObject
{
    public List<data_x> z = new List<data_x>();
}

[Serializable]
public class data_x
{
    public List<data_y> x = new List<data_y>();
}

[Serializable]
public class data_y
{
    public List<int> y = new List<int>();
}
