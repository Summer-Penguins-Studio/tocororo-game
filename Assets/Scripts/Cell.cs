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
            //this.METODODEHECTORBRO;
            //gameController.SelectedCell.METODODEHECTORBRO;
        }
    }
}

