using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private CellType type;
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

