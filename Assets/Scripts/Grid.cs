using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int height;
    public int width;
    public GameObject cellPrefab;

    public List<List<GameObject>> grid;
    // Start is called before the first frame update
    void Start()
    {
        initGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void initGrid()
    {
        int startPositionX = 0;
        int startPositionY = 0;
        grid = new List<List<GameObject>>();

        for (int i = 0; i < height; i++)
        {
            List<GameObject> newRow = new List<GameObject>();
            for (int j = 0; j < width; j++)
            {
                Vector3 position = new Vector3(startPositionX, startPositionY, 0f);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, gameObject.transform);
                //GameObject test = Instantiate(boxTest, position, Quaternion.identity);
                //test.GetComponent<SpriteRenderer>().sprite = tileWater;

                startPositionX++;
            }
            startPositionX = 0;
            startPositionY--;

            grid.Add(newRow);
        }
    }
    public Vector3 GetCellCenter()
    {
        return new Vector3(width / 2 - 0.5f, -(height / 2 - 0.5f), 0);
    }
}
