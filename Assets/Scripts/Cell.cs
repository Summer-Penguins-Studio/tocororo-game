using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellType type;
    private List<Item> items;
    [SerializeField]
    public SpriteManager spriteManager;

    public static CellType GetCellType(List<CellType> bannedTypes)
    {
        List<CellType> cellTypes = new List<CellType>();
        cellTypes.Add(CellType.ONE);
        cellTypes.Add(CellType.TWO);
        cellTypes.Add(CellType.THREE);
        cellTypes.Add(CellType.FOUR);
        cellTypes.Add(CellType.FIVE);

        foreach (CellType cellType in bannedTypes)
        {
            cellTypes.Remove(cellType);
        }

        float rand = Random.Range(0f, 1f);
        CellType type = CellType.NONE;
        switch (bannedTypes.Count) 
        {
            case 1:
                if (rand < 0.40f)
                {
                    type = cellTypes[0];
                }
                else if (rand < 0.75f)
                {
                    type = cellTypes[1];
                }
                else if (rand < 0.95f)
                {
                    type = cellTypes[2];
                }
                else
                {
                    type = cellTypes[3];
                }
                break;
            case 2:
                if (rand < 0.55f)
                {
                    type = cellTypes[0];
                }
                else if (rand < 0.95f)
                {
                    type = cellTypes[1];
                }
                else
                {
                    type = cellTypes[2];
                }
                break;
            default:
                if (rand < 0.35f)
                {
                    type = CellType.ONE;
                }
                else if (rand < 0.60f)
                {
                    type = CellType.TWO;
                }
                else if (rand < 0.80f)
                {
                    type = CellType.THREE;
                }
                else if (rand < 0.95f)
                {
                    type = CellType.FOUR;
                }
                else
                {
                    type = CellType.FIVE;
                }
                break;
        }
        return type;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool equal(Cell other) {
        return other.type == this.type;
    }

    public bool check(int posX, int posY)
    {
        return this.destroyCells(this, posX, posY, CellDirection.DEFAULT, new List<Cell>());
    }

    public bool destroyCells(Cell root, int posX, int posY, CellDirection direction, List<Cell> equals) {
        Grid grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();

        int leftPos = posX - 1;
        int rightPos = posX + 1;
        int topPos = posY - 1;
        int bottomPos = posY + 1;

        switch (direction)
        {
            case CellDirection.LEFT:
                Cell left = grid.getCellByPos(leftPos, posY);

                if (left != null && left.equal(this))
                {
                    equals.Add(left);
                    left.destroyCells(left, leftPos, posY, CellDirection.LEFT, equals);
                }
                
                return false;

            case CellDirection.RIGHT:
                Cell right = grid.getCellByPos(rightPos, posY);

                if (right != null && right.equal(this))
                {
                    equals.Add(right);
                    right.destroyCells(right, rightPos, posY, CellDirection.RIGHT, equals);
                }

                return false;

            case CellDirection.BOTTOM:
                Cell bottom = grid.getCellByPos(posX, bottomPos);

                if (bottom != null && bottom.equal(this))
                {
                    equals.Add(bottom);
                    bottom.destroyCells(bottom, posX, bottomPos, CellDirection.BOTTOM, equals);
                }

                return false;

            case CellDirection.TOP:
                Cell top = grid.getCellByPos(posX, topPos);

                if (top != null && top.equal(this))
                {
                    equals.Add(top);
                    top.destroyCells(top, posX, topPos, CellDirection.TOP, equals);
                }

                return false;

            case CellDirection.DEFAULT:
                List<Cell> leftEquals = new List<Cell>();
                leftEquals.Add(this);
                Cell leftCell = grid.getCellByPos(leftPos, posY);
                if(leftCell != null) leftCell.destroyCells(root, leftPos, posY, CellDirection.LEFT, leftEquals);

                List<Cell> rightEquals = new List<Cell>();
                rightEquals.Add(this);
                Cell rightCell = grid.getCellByPos(rightPos, posY);
                if (rightCell != null) rightCell.destroyCells(root, leftPos, posY, CellDirection.RIGHT, rightEquals);

                List<Cell> topEquals = new List<Cell>();
                topEquals.Add(this);
                Cell topCell = grid.getCellByPos(rightPos, posY);
                if (topCell != null) topCell.destroyCells(root, leftPos, posY, CellDirection.TOP, topEquals);

                List<Cell> bottomEquals = new List<Cell>();
                bottomEquals.Add(this);
                Cell bottomCell = grid.getCellByPos(rightPos, posY);
                if (bottomCell != null)  bottomCell.destroyCells(root, leftPos, posY, CellDirection.BOTTOM, bottomEquals);

                int horizontalCount = leftEquals.Count + rightEquals.Count - 1;
                int verticalCount = bottomEquals.Count + topEquals.Count - 1;

                bool isGonnaUpgrade = false;
                if(horizontalCount >= 3)
                {
                    foreach(Cell c in leftEquals) {
                        Destroy(c.gameObject);
                    }

                    foreach(Cell c in rightEquals)
                    {
                        Destroy(c.gameObject);
                    }

                    isGonnaUpgrade = true;
                }

                else if(verticalCount >= 3)
                {
                    foreach(Cell c in bottomEquals)
                    {
                        Destroy(c.gameObject);
                    }

                    foreach(Cell c in topEquals)
                    {
                        Destroy(c.gameObject);
                    }

                    isGonnaUpgrade = true;
                }

                if (isGonnaUpgrade)
                {
                    this.upgrade();
                }

                return isGonnaUpgrade;

            default:
                return false;
        }
        
    }

    public void move(Cell otherCell) 
    {
        Grid grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();

        Vector3 auxiliar = this.gameObject.transform.position;
        transform.position = otherCell.gameObject.transform.position;
        otherCell.transform.position = auxiliar;

        (int meX, int meY) = grid.findCellPosition(this);
        (int otherX, int otherY) = grid.findCellPosition(otherCell);

        if (otherX > 0 && otherY > 0 && meX > 0 && meY > 0)
        {
            GameObject aux = grid.grid[meX][meY];
            grid.grid[meX][meY] = grid.grid[otherX][otherY];
            grid.grid[otherX][otherY] = aux;
        }
    }

    private void upgrade()
    {
        switch (type)
        {
            case CellType.ONE:
                type = CellType.TWO;
                break;
            case CellType.TWO:
                type = CellType.THREE;
                break;
            case CellType.THREE:
                type = CellType.FOUR;
                break;
            case CellType.FOUR:
                type = CellType.FIVE;
                break;
            case CellType.FIVE:
                break;
        }
    }

    public void setSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteManager.getSprite(type);
    }

    private void OnMouseEnter()
    {
        ///
    }

    private void OnMouseExit()
    {
        ///
    }

    private void OnMouseDown()
    {
        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameController.SelectedCell == null)
        {
            gameController.SelectedCell = this;
        }
        else
        {
            Grid grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();

            bool close = grid.checkNeighboord(gameController.SelectedCell, this);

            if (close)
            {
                this.move(gameController.SelectedCell);
                gameController.SelectedCell = null;
            }
            else
            {
                gameController.SelectedCell = this;
            }
        }
    }
}

