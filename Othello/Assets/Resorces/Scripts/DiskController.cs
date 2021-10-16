using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : MonoBehaviour
{
    [SerializeField]
    AnimationClip WtoB;
    [SerializeField]
    AnimationClip BtoW;

    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void PlayAnim1()
    {
        anim.clip = WtoB;
        anim.Play();
    }

    public void PlayAnim2()
    {
        anim.clip = BtoW;
        anim.Play();
    }

    public void AnimEnd()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
    }
}
