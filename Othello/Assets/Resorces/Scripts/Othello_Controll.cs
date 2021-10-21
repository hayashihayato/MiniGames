using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Othello_Controll : MonoBehaviour
{
    [SerializeField]
    private GameObject disk_prefab;
    [SerializeField]
    private GameObject valid_prefab;

    private Vector3 inipos = new Vector3(5.25f, 0f, 5.25f);
    private const float unit = -1.5f;

    public enum ecolor
    {
        black = 0,
        white = 1,
        none = 2,
    }

    public int[,] in_board_disk_state = new int[8, 8];
    private GameObject[,] in_board_disk = new GameObject[8, 8];
    private List<GameObject> validlist = new List<GameObject>();
    public int[,] Get_Board_Disk_State() { return in_board_disk_state; }

    [SerializeField]
    private CPUController cpu;

    // Start is called before the first frame update
    void Start()
    {
        //cpu = new CPUController();
        InitBoard();
    }

    void InitBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                in_board_disk_state[y, x] = (int)ecolor.none;
            }
        }
        CreateDisk(3, 3, (int)ecolor.white);
        CreateDisk(3, 4, (int)ecolor.black);
        CreateDisk(4, 3, (int)ecolor.black);
        CreateDisk(4, 4, (int)ecolor.white);
        ViewValid();
    }

    public void ViewValid()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (in_board_disk_state[y, x] != (int)ecolor.none) continue;
                int reverse_amount = 0;
                for (int yline = -1; yline < 2; yline++)
                {
                    for (int xline = -1; xline < 2; xline++)
                    {
                        if (xline == 0) if (yline == 0) continue;
                        reverse_amount += CheckLine(x, y, xline, yline, (int)ecolor.black);
                    }
                }
                if (reverse_amount > 0) CreateValid(x, y);
            }
        }
    }

    public void ClickValid(int x, int y)
    {
        for (int i = 0; i < validlist.Count; i++) Destroy(validlist[i].gameObject);
        validlist.Clear();
        CreateDisk(x, y, (int)ecolor.black);
        for (int yline = -1; yline < 2; yline++)
        {
            for (int xline = -1; xline < 2; xline++)
            {
                if (xline == 0) if (yline == 0) continue;
                ReverseLine(x, y, xline, yline, (int)ecolor.black);
            }
        }
        cpu.StartOthelloAI();
    }

    void CreateValid(int x, int y)
    {
        Vector3 objpos = inipos + new Vector3(unit * y, 0f, unit * x);
        GameObject createobj;
        createobj = Instantiate(valid_prefab, objpos, Quaternion.identity);
        createobj.GetComponent<ValidState>().pos[0] = x;
        createobj.GetComponent<ValidState>().pos[1] = y;
        validlist.Add(createobj);
    }

    public void CreateDisk(int x, int y, int color)
    {
        Vector3 objpos = new Vector3(inipos.x + (unit * y), 0f, inipos.z + (unit * x));
        GameObject createobj;
        createobj = Instantiate(disk_prefab, objpos, Quaternion.identity);
        createobj.transform.rotation = Quaternion.Euler(0f, 0f, color * 180f);
        in_board_disk[y, x] = createobj;
        in_board_disk_state[y, x] = color;
    }

    public int CheckLine(int x, int y, int xline, int yline, int diskstate)
    {
        int reverseamount = 0;
        int amount = 0;
        int i = 1;
        while (true)
        {
            if (y + yline * i < 0 || x + xline * i < 0 || y + yline * i > 7 || x + xline * i > 7) break;
            if (i == 1 && in_board_disk_state[y + yline * i, x + xline * i] == diskstate) break;
            if (in_board_disk_state[y + yline * i, x + xline * i] == (int)ecolor.none) break;
            if (in_board_disk_state[y + yline * i, x + xline * i] == diskstate)
            {
                reverseamount = amount;
                break;
            }
            amount++;
            i++;
        }
        return reverseamount;
    }

    public void ReverseLine(int x, int y, int xline, int yline, int diskstate)
    {
        List<Vector2> reverselist = new List<Vector2>();
        int i = 1;
        while (true)
        {
            if (y + yline * i < 0 || x + xline * i < 0 || y + yline * i > 7 || x + xline * i > 7)
            {
                reverselist.Clear();
                break;
            }
            if (i == 1 && in_board_disk_state[y + yline * i, x + xline * i] == diskstate)
            {
                reverselist.Clear();
                break;
            }
            if (in_board_disk_state[y + yline * i, x + xline * i] == (int)ecolor.none)
            {
                reverselist.Clear();
                break;
            }
            if (in_board_disk_state[y + yline * i, x + xline * i] == diskstate)
            {
                break;
            }
            reverselist.Add(new Vector2(x + xline * i, y + yline * i));
            i++;
        }

        if (diskstate == 0) diskstate = 1;
        else diskstate = 0;

        for (int j = 0; j < reverselist.Count; j++)
        {
            DiskController diskanim = in_board_disk[(int)reverselist[j].y, (int)reverselist[j].x].GetComponent<DiskController>();
            if (diskstate == 1)
            {
                in_board_disk[(int)reverselist[j].y, (int)reverselist[j].x].transform.position += new Vector3(0f, 0.625f, 0f);
                diskanim.PlayAnim1();
                in_board_disk_state[(int)reverselist[j].y, (int)reverselist[j].x] = 0;
            }
            else
            {
                in_board_disk[(int)reverselist[j].y, (int)reverselist[j].x].transform.position += new Vector3(0f, 0.625f, 0f);
                diskanim.PlayAnim2();
                in_board_disk_state[(int)reverselist[j].y, (int)reverselist[j].x] = 1;
            }
        }
    }
}
