using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool _hasTrap;
    private CellType _cellType;
    public int Col;
    public int Row;
    public void Configurate(bool hasTrap, CellType cellType, int col, int row)
    {
        _hasTrap = hasTrap;
        _cellType = cellType;
        this.Col = col;
        this.Row = row;

        instantiateCellRepresentation(cellType);
    }

    private void instantiateCellRepresentation(CellType cellType)
    {
        PrefabProvider prefabProvider = gameObject.GetComponent<PrefabProvider>();
        if (prefabProvider == null)
        {
            Debug.LogError("No se encontro el prefabProvider dentro del objeto Cell");
        }

        GameObject cellRepresentation;
        switch (cellType)
        {
            case (CellType.Chest1):
                cellRepresentation = Instantiate(prefabProvider.GetChestCellPrefab(), new Vector3(0, 0, 0), Quaternion.Euler(90, 90, 0));
                break;
            case (CellType.Chest2):
                cellRepresentation = Instantiate(prefabProvider.GetChestCellPrefab(), new Vector3(0, 0, 0), Quaternion.Euler(90, -90, 0));
                break;
            case (CellType.Chest3):
                cellRepresentation = Instantiate(prefabProvider.GetChestCellPrefab(), new Vector3(0, 0, 0), Quaternion.Euler(90, 90, 0));
                break;
            case (CellType.PStart):
                cellRepresentation = Instantiate(prefabProvider.GetPlayerStartCellPrefab(), new Vector3(0, 0, 0), Quaternion.Euler(90, 90, 0));
                break;
            default:
                cellRepresentation = Instantiate(prefabProvider.GetNormalCellPrefab(), new Vector3(0, 0, 0), Quaternion.Euler(90, 90, 0));
                break;
        }
        cellRepresentation.transform.parent = this.gameObject.transform;
        cellRepresentation.transform.localPosition = new Vector3(0, 0, 0);
    }
}
