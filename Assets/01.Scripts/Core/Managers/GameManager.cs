using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Unit selectUnit;
    
    
    private void Update()
    {
        SelectUnit();
    }

    private void SelectUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent(out Unit unit))
                {
                    selectUnit = unit;
                    selectUnit.SelectUnit();
                }
            }
        }
    }
}
