using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private bool _hasTrap;
    public bool HasTrap() { return _hasTrap; }
    public void RemoveTrap() { _hasTrap = false; }

    [SerializeField]
    private Animator _anim;

    private bool _isRevealed;
    private CellType _cellType;
    public int Col;
    public int Row;
    public bool canHighlight;

    public void Start()
    {
        if (_cellType == CellType.Normal) {
            _anim = gameObject.transform.GetChild(0).GetChild(3).GetComponent<Animator>();
        }
    }
    public void Configurate(bool hasTrap, CellType cellType, int col, int row)
    {
        _hasTrap = hasTrap;
        _cellType = cellType;
        this.Col = col;
        this.Row = row;
        if (cellType == CellType.Normal) {
            _isRevealed = false;
        }

        instantiateCellRepresentation(cellType);
    }

    public void Reveal() {
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        GameObject child2 = child.transform.GetChild(2).gameObject;
        child2.SetActive(false);
        _isRevealed=true;
    }

    public bool IsRevealed() { 
        return _isRevealed;
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
                cellRepresentation = Instantiate(prefabProvider.GetPlayerStartCellPrefab(), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
                break;
            default:
                cellRepresentation = Instantiate(prefabProvider.GetNormalCellPrefab(), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
                break;
        }
        cellRepresentation.transform.parent = this.gameObject.transform;
        cellRepresentation.transform.localPosition = new Vector3(0, 0, 0);
    }

    public CellType GetCellType() {
        return _cellType;
    }

    public void ShowAvaliable() {
        /*esto hay que cambiarlo, estoy mostrando el available buscando el hijo del cell es malisimo*/
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        GameObject child2 = child.transform.GetChild(0).gameObject;
        child2.SetActive(true);
        GameObject child3 = child.transform.GetChild(1).gameObject;
        child3.SetActive(false);
        canHighlight = true;
    }

    public void ShowSelected()
    {
        /*Hago el selected visible*/
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        GameObject child2 = child.transform.GetChild(1).gameObject;
        child2.SetActive(true);
        /*Hago el available invisible*/
        GameObject child4 = child.transform.GetChild(0).gameObject;
        child4.SetActive(false);
    }

    public void HideAllSelectors() {
        /*Hago el selected visible*/
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        GameObject child2 = child.transform.GetChild(1).gameObject;
        child2.SetActive(false);
        /*Hago el available invisible*/
        GameObject child4 = child.transform.GetChild(0).gameObject;
        child4.SetActive(false);
    }

    public void ActivateTrap() {
        gameObject.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        _anim.SetTrigger("Activated");
    }
}
