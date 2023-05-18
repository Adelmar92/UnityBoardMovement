using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    private bool _hasTrap;
    private CellType _cellType;
    public int Col;
    public int Row;
    public bool canHighlight;
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

    public CellType GetCellType() {
        return _cellType;
    }

    public void ShowAvaliable() {
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        GameObject child2 = child.transform.GetChild(0).gameObject;
        child2.SetActive(true);
        canHighlight = true;
    }

    public void HighLight(bool highLight) {
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        GameObject child2 = child.transform.GetChild(1).gameObject;
        Renderer rend = child2.GetComponent<Renderer>();
        rend.enabled = highLight;
    }
}
