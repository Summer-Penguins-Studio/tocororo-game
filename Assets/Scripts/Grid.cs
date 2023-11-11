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
            CellType previousCell = CellType.NONE;
            CellType grandPreviousCell = CellType.NONE;
            CellType topCell = CellType.NONE;
            CellType topTopCell = CellType.NONE;
            for (int j = 0; j < width; j++)
            {
                List<CellType> bannedTypes = new List<CellType>();
                if (previousCell == grandPreviousCell)
                {
                    bannedTypes.Add(previousCell);
                }
                if (i > 0)
                {
                    topCell = grid[i - 1][j].GetComponent<Cell>().type;
                }
                if (i > 1)
                {
                    topTopCell = grid[i - 2][j].GetComponent<Cell>().type;
                }
                if (topCell == topTopCell)
                {
                    bannedTypes.Add(topCell);
                }
                CellType actualCell = Cell.GetCellType(bannedTypes);
                Vector3 position = new Vector3(startPositionX, startPositionY, 0f);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, gameObject.transform);
                cell.GetComponent<SpriteRenderer>().sortingOrder = i;
                cell.GetComponent<Cell>().type = actualCell;
                cell.GetComponent<Cell>().setSprite();
                grandPreviousCell = previousCell;
                previousCell = actualCell;
                topTopCell = topCell;
                startPositionX++;
                newRow.Add(cell);
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

    public Cell getCellByPos(int x, int y) {
        if(x < 0 || x > this.grid.Count - 1)
        {
            return null;
        }
        else
        {
            if(y < 0 || y > this.grid[x].Count - 1)
            {
                return null;
            }
            else
            {
                return this.getCell(this.grid[x][y]);
            }
        }
    }

    public (int, int) findCellPosition(Cell cell)
    {
        int x = -1;
        int y = -1;

        for(int i = 0; i < this.grid.Count; i++)
        {
            for(int j =0; j < this.grid[i].Count; j++)
            {
                Cell currentCell = this.getCell(this.grid[i][j]);

                if(currentCell == cell)
                {
                    x = i;
                    y = j;
                }
            }
        }

        return (x, y);
    }

    public Cell getCell(GameObject obj){
        return obj.GetComponent<Cell>();
    }

    public void onClick(){
        while (this.destroyCells())
        {
            this.adjust();
        }
    }

    private void adjust() {
        int startPositionX = 0;

        for (int i = this.grid.Count - 1; i >= 0; i--)
        {
            for(int j = 0; j < this.grid[i].Count; j++)
            {
                Cell cell = this.getCell(this.grid[i][j]);

                if(cell == null && i == 0)
                {
                    Vector3 position = new Vector3(startPositionX, 0, 0f);
                    GameObject newCell = Instantiate(cellPrefab, position, Quaternion.identity, gameObject.transform);

                    this.grid[i][j] = newCell;
                }

                if(cell == null && i != 0)
                {
                    int topPos = i - 1;

                    this.grid[i][j] = this.grid[topPos][j];
                    this.grid[topPos][j] = null;

                }

                startPositionX++;
            }

            startPositionX = 0;
        }
    }

    private bool destroyCells()
    {
        bool change = false;

        for (int i = 0; i < this.grid.Count && !change; i++)
        {
            for (int j = 0; j < this.grid[i].Count && !change; j++)
            {
                Cell cell = this.getCell(this.grid[i][j]);
                change = cell.check(i, j);
            }
        }

        return change;
    }
}
