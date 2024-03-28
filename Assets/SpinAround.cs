using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAround : MonoBehaviour
{
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 50 * Time.deltaTime, 0);
    }
}
