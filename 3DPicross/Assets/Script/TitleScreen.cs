using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    private PicrossGameManager picrossGameManager;
    // Start is called before the first frame update
    void Start()
    {
        picrossGameManager = GameObject.Find("PicrossGameManager").GetComponent<PicrossGameManager>();
        Init();
    }

    private void Init()
    {
        startButton.onClick.AddListener(() =>
        {
            Debug.Log("Picross読み込み中");
            picrossGameManager.DestroyAllScreen();
        });
    }
}
