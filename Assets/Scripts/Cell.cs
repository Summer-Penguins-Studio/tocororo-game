using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellType type;
    private List<Item> items;
    List<Sprite> sprites;


    // Start is called before the first frame update
    void Start()
    {
        setSprite();
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

    private void setSprite()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch (type)
        {
            case CellType.ONE:
                spriteRenderer.sprite = sprites[0];
                break;
            case CellType.TWO:
                spriteRenderer.sprite = sprites[1];
                break;
            case CellType.THREE:
                spriteRenderer.sprite = sprites[2];
                break;
            case CellType.FOUR:
                spriteRenderer.sprite = sprites[3];
                break;
            case CellType.FIVE:
                spriteRenderer.sprite = sprites[4];
                break;
        }
    }
}

