using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicrossGameManager : MonoBehaviour
{
    [SerializeField]
    private bool easy = false;
    [SerializeField]
    private GameObject titlescreen;
    [SerializeField]
    private GameObject gamescreen;
    [SerializeField]
    private GameObject planeprefab;
    private GameObject picrossplane;
    private GameObject canvas;
    private PicrossDataObject picrosstable;
    [SerializeField]
    private GameObject block;
    [SerializeField]
    private GameObject valid;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject[] arrow;
    private GameObject[,] arrowobj = new GameObject[3, 2];
    RaycastHit raycastHit;
    private Camera maincamera;
    bool isgame = false;
    private int misscount = 0;

    private List<List<List<int>>> picross = new List<List<List<int>>>();
    private List<List<List<GameObject>>> picrossobj = new List<List<List<GameObject>>>();
    // Start is called before the first frame update
    void Start()
    {
        maincamera = camera.GetComponent<Camera>();
        raycastHit = new RaycastHit();
        picrosstable = Resources.Load<PicrossDataObject>("ScriptableObject/PicrossDataBase");
        canvas = GameObject.Find("Canvas");
        CreateScreen(titlescreen);
    }

    void Update()
    {
        if (isgame)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                BreakBlock();
            }

            if (Input.GetButton("Fire1"))
            {
            }

            if (Input.GetButtonDown("Fire2"))
            {
                DrawBlock();
            }

            if (misscount >= 3)
            {
                GameObject.Destroy(picrossplane.gameObject);
                CreateScreen(titlescreen);
                misscount = 0;
                isgame = false;
            }
        }
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

    public void LoadPicross(int number)
    {
        CreateScreen(gamescreen);
        picross.Clear();
        CreatePlane();
        var picrossdata = picrosstable.Picroses[number].p;
        for (int z = 0; z < picrossdata.z.Count; z++)
        {
            List<List<int>> xlist = new List<List<int>>();
            for (int x = 0; x < picrossdata.z[z].x.Count; x++)
            {
                List<int> ylist = new List<int>();
                for (int y = 0; y < picrossdata.z[z].x[x].y.Count; y++)
                {
                    ylist.Add(picrossdata.z[z].x[x].y[y]);
                }
                xlist.Add(ylist);
                Debug.Log(string.Join(",", ylist));
            }
            picross.Add(xlist);
        }
        ViewPicross();
    }

    void ViewPicross()
    {
        for (int z = 0; z < picross.Count; z++)
        {
            List<List<GameObject>> xlist = new List<List<GameObject>>();
            for (int x = 0; x < picross[z].Count; x++)
            {
                List<GameObject> ylist = new List<GameObject>();
                for (int y = 0; y < picross[z][x].Count; y++)
                {
                    ylist.Add(CreateBlock(z, x, y));
                }
            }
        }
        CreateArrow();
        isgame = true;
        camera.GetComponent<PicrossCameraController>().isload = true;
    }

    void CreateArrow()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 inipos = Vector3.zero;
            if (i == 0) inipos = new Vector3(picross[0].Count, 0f, 0f);
            else if (i == 1) inipos = new Vector3(0f, picross[0][0].Count, 0f);
            else if (i == 2) inipos = new Vector3(0f, 0f, picross.Count);
            arrowobj[i, 0] = Instantiate(arrow[i], inipos, Quaternion.identity);
            arrowobj[i, 1] = Instantiate(arrow[i], inipos * -1f, Quaternion.identity);
        }
    }

    void BreakBlock()
    {
        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition); //カメラからRayをマウスポインタの位置に飛ばす

        if (Physics.Raycast(ray, out raycastHit))
        {
            PicrossState hitstate = raycastHit.transform.gameObject.GetComponent<PicrossState>();

            if (raycastHit.transform.gameObject.tag != "Block") return;

            if (raycastHit.transform.gameObject.GetComponent<Renderer>().material.color != Color.yellow)
            {
                if (picross[hitstate.pos[0]][hitstate.pos[1]][hitstate.pos[2]] == 1)
                {
                    hitstate.PlayDontBreak();
                    raycastHit.transform.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                    gamescreen.GetComponent<GameScreen>().AddMiss(misscount);
                    misscount++;
                }
                else
                    hitstate.PlayBreak();
            }
        }
    }

    void DrawBlock()
    {
        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition); //カメラからRayをマウスポインタの位置に飛ばす

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.gameObject.GetComponent<Renderer>().material.color == Color.yellow)
                raycastHit.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
            else
                raycastHit.transform.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            if (raycastHit.transform.gameObject.GetComponent<PicrossState>() != null)
                raycastHit.transform.gameObject.GetComponent<PicrossState>().PlayDraw();
        }
    }

    void CreatePlane()
    {
        GameObject createobj;
        createobj = Instantiate(planeprefab, Vector3.zero, Quaternion.identity);
        picrossplane = createobj;
    }

    GameObject CreateBlock(int z, int x, int y)
    {
        Vector3 inipos = new Vector3(x - picross[0].Count / 2, 9 - y - picross[0][0].Count / 2, z - picross.Count / 2);
        GameObject createobj;
        createobj = Instantiate(block, inipos, Quaternion.identity);
        createobj.transform.parent = picrossplane.transform;
        createobj.GetComponent<PicrossState>().pos = new int[3] { z, x, y };
        //picrossobj[z][x][y] = createobj;
        string zs = "", xs = "", ys = "";
        int count = 0, zc = 0, xc = 0, yc = 0;
        for (int i = 0; i < picross.Count; i++)
        {
            if (picross[i][x][y] == 1)
            {
                if (count == 0) zc++;
                count++;
            }
            else if (count > 0)
            {
                if (zc == 1) zs += count.ToString();
                else zs += "," + count.ToString();
                count = 0;
            }

            if (i == picross.Count - 1 && count > 0)
            {
                if (zc == 1) zs += count.ToString();
                else zs += "," + count.ToString();
            }
        }

        count = 0;
        for (int i = 0; i < picross[z].Count; i++)
        {
            if (picross[z][i][y] == 1)
            {
                if (count == 0) xc++;
                count++;
            }
            else if (count > 0)
            {
                if (xc == 1) xs += count.ToString();
                else xs += "," + count.ToString();
                count = 0;
            }

            if (i == picross[z].Count - 1 && count > 0)
            {
                if (xc == 1) xs += count.ToString();
                else xs += "," + count.ToString();
            }
        }

        count = 0;
        for (int i = 0; i < picross[z][x].Count; i++)
        {
            if (picross[z][x][i] == 1)
            {
                if (count == 0) yc++;
                count++;
            }
            else if (count > 0)
            {
                if (yc == 1) ys += count.ToString();
                else ys += "," + count.ToString();
                count = 0;
            }

            if (i == picross[z][x].Count - 1 && count > 0)
            {
                if (yc == 1) ys += count.ToString();
                else ys += "," + count.ToString();
            }
        }
        createobj.GetComponent<PicrossState>().SetTxt(zs, zc, xs, xc, ys, yc);
        return createobj;
    }
}
