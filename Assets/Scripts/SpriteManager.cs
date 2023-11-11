using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField]
    public Sprite earth;
    [SerializeField]
    public Sprite green;
    [SerializeField]
    public List<Sprite> grass = new List<Sprite>();
    [SerializeField]
    public List<Sprite> bush = new List<Sprite>();
    [SerializeField]
    public List<Sprite> tree = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite getSprite(CellType type) 
    {
        switch (type)
        {
            case CellType.ONE:
                return earth;
            case CellType.TWO:
                return green;
            case CellType.THREE:
                return grass[(int)Random.Range(0, 2)];
            case CellType.FOUR:
                return bush[(int)Random.Range(0, 2)];
            case CellType.FIVE:
                return tree[(int)Random.Range(0, 2)];
            default:
                return null;
        }
    }
}
