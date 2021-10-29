using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicrossGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject titlescreen;
    private GameObject canvas;
    [SerializeField]
    private PicrossDataObject picrosstable;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        CreateScreen(titlescreen);
    }

    void CreateScreen(GameObject screen)
    {
        DestroyAllScreen();
        GameObject createobj;
        createobj = Instantiate(screen, Vector3.zero, Quaternion.identity);
        createobj.transform.parent = canvas.transform;
        createobj.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    }

    public void DestroyAllScreen()
    {
        foreach (Transform t in canvas.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
    }

    void LoadPicross(int number)
    {
        Picross picrossdata = picrosstable.Picroses[number];
    }
}
