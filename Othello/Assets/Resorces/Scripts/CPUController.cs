using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUController : MonoBehaviour
{
    [SerializeField]
    private Othello_Controll oc;
    private int[,] AIboard;

    public enum ecolor
    {
        black = 0,
        white = 1,
        none = 2,
    }

    public void StartOthelloAI()
    {
        StartCoroutine(InitOthelloAI());
    }

    private IEnumerator InitOthelloAI()
    {
        Debug.Log("startAI");
        yield return new WaitForSeconds(2);
        AIboard = oc.Get_Board_Disk_State();
        Debug.Log("endAI");
        MaxValidScore();
    }

    void MaxValidScore()
    {
        int mx = -1, my = -1;
        int max_score = 0;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (AIboard[y, x] != (int)ecolor.none) continue;
                int reverse_score = 0;
                for (int yline = -1; yline < 2; yline++)
                {
                    for (int xline = -1; xline < 2; xline++)
                    {
                        if (xline == 0) if (yline == 0) continue;
                        reverse_score += oc.CheckLine(x, y, xline, yline, (int)ecolor.white);
                    }
                }
                if (reverse_score >= max_score)
                {
                    if (reverse_score == max_score)
                    {
                        if (Random.Range(1, 100) > 50)
                        {
                            max_score = reverse_score;
                            mx = x;
                            my = y;
                        }
                    }
                    else
                    {
                        max_score = reverse_score;
                        mx = x;
                        my = y;
                    }
                }
            }
        }
        if (mx != -1 && my != -1)
        {
            oc.CreateDisk(mx, my, (int)ecolor.white);
            oc.Getdiskcount()[1]++;
            for (int yline = -1; yline < 2; yline++)
            {
                for (int xline = -1; xline < 2; xline++)
                {
                    if (xline == 0) if (yline == 0) continue;
                    oc.ReverseLine(mx, my, xline, yline, (int)ecolor.white);
                }
            }
        }
        if (oc.CheckEnd()) oc.GetComponent<UIManager>().CreateGameEndUI();
        else oc.ViewValid();
    }
}
