using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Grid map;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        transform.position = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>().GetCellCenter() + new Vector3(0,0,-2);
    }
}
