using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Cell selectedCell;

    public Cell SelectedCell { get => selectedCell; set => selectedCell = value; }

    // Start is called before the first frame update
    void Start()
    {
        selectedCell = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
