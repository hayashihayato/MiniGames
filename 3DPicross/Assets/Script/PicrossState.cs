using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicrossState : MonoBehaviour
{
    public int[] pos { get; set; } = null;
    [SerializeField]
    private TextMesh[] ztxt;
    [SerializeField]
    private TextMesh[] xtxt;
    [SerializeField]
    private TextMesh[] ytxt;

    [SerializeField]
    AnimationClip Break;
    [SerializeField]
    AnimationClip Draw;
    [SerializeField]
    AnimationClip DontBreak;

    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void PlayBreak()
    {
        anim.clip = Break;
        anim.Play();
    }

    public void PlayDraw()
    {
        anim.clip = Draw;
        anim.Play();
    }

    public void PlayDontBreak()
    {
        anim.clip = DontBreak;
        anim.Play();
    }

    public void AnimEnd()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void SetTxt(string zs, int zc, string xs, int xc, string ys, int yc)
    {
        ztxt[0].text = zs;
        ztxt[1].text = zs;
        xtxt[0].text = xs;
        xtxt[1].text = xs;
        ytxt[0].text = ys;
        ytxt[1].text = ys;
        if (zc == 2)
        {
            ztxt[0].gameObject.transform.localScale = new Vector3(0.06f, 0.1f, 1f);
            ztxt[1].gameObject.transform.localScale = new Vector3(0.06f, 0.1f, 1f);
        }
        else if (zc == 3)
        {
            ztxt[0].gameObject.transform.localScale = new Vector3(0.04f, 0.1f, 1f);
            ztxt[1].gameObject.transform.localScale = new Vector3(0.04f, 0.1f, 1f);
        }
        else if (zc >= 4)
        {
            ztxt[0].gameObject.transform.localScale = new Vector3(0.03f, 0.1f, 1f);
            ztxt[1].gameObject.transform.localScale = new Vector3(0.03f, 0.1f, 1f);
        }

        if (xc == 2)
        {
            xtxt[0].gameObject.transform.localScale = new Vector3(0.06f, 0.1f, 1f);
            xtxt[1].gameObject.transform.localScale = new Vector3(0.06f, 0.1f, 1f);
        }
        else if (xc == 3)
        {
            xtxt[0].gameObject.transform.localScale = new Vector3(0.04f, 0.1f, 1f);
            xtxt[1].gameObject.transform.localScale = new Vector3(0.04f, 0.1f, 1f);
        }
        else if (xc >= 4)
        {
            xtxt[0].gameObject.transform.localScale = new Vector3(0.03f, 0.1f, 1f);
            xtxt[1].gameObject.transform.localScale = new Vector3(0.03f, 0.1f, 1f);
        }

        if (yc == 2)
        {
            ytxt[0].gameObject.transform.localScale = new Vector3(0.06f, 0.1f, 1f);
            ytxt[1].gameObject.transform.localScale = new Vector3(0.06f, 0.1f, 1f);
        }
        else if (yc == 3)
        {
            ytxt[0].gameObject.transform.localScale = new Vector3(0.04f, 0.1f, 1f);
            ytxt[1].gameObject.transform.localScale = new Vector3(0.04f, 0.1f, 1f);
        }
        else if (yc >= 4)
        {
            ytxt[0].gameObject.transform.localScale = new Vector3(0.03f, 0.1f, 1f);
            ytxt[1].gameObject.transform.localScale = new Vector3(0.03f, 0.1f, 1f);
        }
    }
}
