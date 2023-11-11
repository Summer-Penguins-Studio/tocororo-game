using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Grid map;
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
        map = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        target = map.GetCellCenter();
        transform.position = target + new Vector3(0,0,-2);
    }
}
