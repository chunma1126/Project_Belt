using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Cell[,] inventory = new Cell[3, 3];
        
    private void Start()
    {
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                inventory[i, j] = new Cell(transform.Find($"Cell_{index++}").GetComponent<Image>(), false);
            }
        }
    }

    
    
    
    
}
