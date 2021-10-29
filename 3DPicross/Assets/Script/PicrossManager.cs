using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicrossManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blockprefab;

    private void CreateBlock(int x, int y, int z)
    {
        Vector3 objpos = new Vector3(x, y, z);
        GameObject createobj;
        createobj = Instantiate(blockprefab, objpos, Quaternion.identity);
    }
}
