using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellType type;
    private List<Item> items;
    [SerializeField]
    public SpriteManager spriteManager;
    public bool remove = false;

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

    public bool check()
    {
        Grid grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        (int posX, int posY) = grid.findCellPosition(this);

        int leftPos = posY - 1;
        int rightPos = posY + 1;
        int topPos = posX - 1;
        int bottomPos = posX + 1;

        List<Cell> leftEquals = new List<Cell>();
        leftEquals.Add(this);
        Cell leftCell = grid.getCellByPos(posX, leftPos);
        if (leftCell != null && leftCell.equal(this))
        {
            leftEquals.Add(leftCell);
            leftCell.destroyCells(posX, leftPos, CellDirection.LEFT, leftEquals);
        }

        List<Cell> rightEquals = new List<Cell>();
        rightEquals.Add(this);
        Cell rightCell = grid.getCellByPos(posX, rightPos);
        if (rightCell != null && rightCell.equal(this))
        {
            rightEquals.Add(rightCell);
            rightCell.destroyCells(posX, rightPos, CellDirection.RIGHT, rightEquals);
        }

        List<Cell> topEquals = new List<Cell>();
        topEquals.Add(this);
        Cell topCell = grid.getCellByPos(topPos, posY);
        if (topCell != null && topCell.equal(this))
        {
            topEquals.Add(topCell);
            topCell.destroyCells(topPos, posY, CellDirection.TOP, topEquals);
        }

        List<Cell> bottomEquals = new List<Cell>();
        bottomEquals.Add(this);
        Cell bottomCell = grid.getCellByPos(bottomPos, posY);
        if (bottomCell != null && bottomCell.equal(this))
        {
            bottomEquals.Add(bottomCell);            
            bottomCell.destroyCells(bottomPos, posY, CellDirection.BOTTOM, bottomEquals);
        }

        int horizontalCount = leftEquals.Count + rightEquals.Count - 1;
        int verticalCount = bottomEquals.Count + topEquals.Count - 1;

        print("Position: " + posX + " " + posY);
        print(leftEquals.Count);
        print(rightEquals.Count);
        print(topEquals.Count);
        print(bottomEquals.Count);
        print("\n");

        bool isGonnaUpgrade = false;
        if (horizontalCount >= 3)
        {
            foreach (Cell c in leftEquals)
            {
                if (!(c == this))
                {
                    Destroy(c.gameObject);
                    c.remove = true;
                }
            }

            foreach (Cell c in rightEquals)
            {
                if (!(c == this))
                {
                    Destroy(c.gameObject);
                    c.remove = true;
                }
            }

            isGonnaUpgrade = true;
        }

        else if (verticalCount >= 3)
        {
            foreach (Cell c in bottomEquals)
            {
                if (!(c == this))
                {
                    Destroy(c.gameObject);
                    c.remove = true;
                }
            }

            foreach (Cell c in topEquals)
            {
                if (!(c == this))
                {
                    Destroy(c.gameObject);
                    c.remove = true;
                }
            }

            isGonnaUpgrade = true;
        }

        if (isGonnaUpgrade)
        {
            this.upgrade();
            this.setSprite();
        }

        return isGonnaUpgrade;
    }

    public bool destroyCells(int posX, int posY, CellDirection direction, List<Cell> equals) {
        Grid grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();

        int leftPos = posY - 1;
        int rightPos = posY + 1;
        int topPos = posX - 1;
        int bottomPos = posX + 1;

        switch (direction)
        {
            case CellDirection.LEFT:
                Cell left = grid.getCellByPos(posX, leftPos);

                if (left != null && left.equal(this))
                {
                    equals.Add(left);
                    left.destroyCells(posX, leftPos, CellDirection.LEFT, equals);
                }
                
                return false;

            case CellDirection.RIGHT:
                Cell right = grid.getCellByPos(posX, rightPos);

                if (right != null && right.equal(this))
                {
                    equals.Add(right);
                    right.destroyCells(posX, rightPos, CellDirection.RIGHT, equals);
                }

                return false;

            case CellDirection.BOTTOM:
                Cell bottom = grid.getCellByPos(bottomPos, posY);

                if (bottom != null && bottom.equal(this))
                {
                    equals.Add(bottom);
                    bottom.destroyCells(bottomPos, posY, CellDirection.BOTTOM, equals);
                }

                return false;

            case CellDirection.TOP:
                Cell top = grid.getCellByPos(topPos, posY);

                if (top != null && top.equal(this))
                {
                    equals.Add(top);
                    top.destroyCells(topPos, posY, CellDirection.TOP, equals);
                }

                return false;

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
                grid.onClick(gameController.SelectedCell, this);
                gameController.SelectedCell = null;
            }
            else
            {
                gameController.SelectedCell = this;
            }
        }
    }
}

