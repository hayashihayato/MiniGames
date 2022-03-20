using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicrossCameraController : MonoBehaviour
{
    public bool isload = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isload)
        {
            if (Input.GetButton("Fire3"))
            {
                this.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 0f) - transform.position, Vector3.up);
                if (Input.GetAxis("Mouse X") < 0)
                    this.transform.position += this.transform.right * -0.1f;
                else if (Input.GetAxis("Mouse X") > 0)
                    this.transform.position += this.transform.right * 0.1f;
                if (Input.GetAxis("Mouse Y") < 0)
                    this.transform.position += this.transform.up * -0.1f;
                else if (Input.GetAxis("Mouse Y") > 0)
                    this.transform.position += this.transform.up * 0.1f;
            }
        }
    }
}
