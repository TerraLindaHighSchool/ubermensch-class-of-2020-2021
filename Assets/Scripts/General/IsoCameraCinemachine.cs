using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class IsoCameraCinemachine : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("IsoCamera Target").transform;
        this.gameObject.GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.Find("pTorus1").transform;
    }
}