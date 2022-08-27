using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject cameraholder = null;
    private void Update()
    {
        transform.position = new Vector3(cameraholder.transform.position.x, transform.position.y, transform.position.z);
    }
}
