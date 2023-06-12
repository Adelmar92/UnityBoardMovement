using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{

    private Cell[,] _cells;
    public Cell[,] GetCells() { return this._cells; }


    [SerializeField]
    private int _trapAmount = 10;
    [SerializeField]
    private Cell _heroDeployCell;

    /*Prefabs*/
    [SerializeField]
    private GameObject _cellPrefab;
    public void BuildBoard()
    {
        CellType[,] boardTemplate = BoardTemplate.GetTemplate();

        _cells = new Cell[boardTemplate.GetLength(0), boardTemplate.GetLength(1)];

        int rowDim = boardTemplate.GetLength(0);
        int colDim = boardTemplate.GetLength(1);

        List<List<int>> trapCells = GetTrapCells(rowDim, colDim, boardTemplate);

        for (int row = 0; row < rowDim; row++)
        {
            for (int col = 0; col < colDim; col++)
            {
                GameObject cell = Instantiate(_cellPrefab, getCellPosition(row, col), Quaternion.identity);
                cell.name = "CELL " + row + "-" + col;
                cell.transform.parent = this.gameObject.transform;

                Cell cellController = cell.GetComponent<Cell>();

                bool hasTrap = trapCells.Any(x => x[0] == row && x[1] == col);

                if (hasTrap)
                {
                    Debug.Log("Has Trap " + row + "-" + col);
                }

                cellController.Configurate(hasTrap, boardTemplate[row, col], col , row);
                _cells[row, col] = cellController;

                //Asigno la celda que se usa para desplegar el personaje
                if (cellController.GetCellType() == CellType.PStart) {
                    _heroDeployCell = cellController;
                }
            }
        }
    }
    public List<List<int>> GetTrapCells(int rowDim, int colDim, CellType[,] boardTemplate)
    {
        List<List<int>> randomCells = new List<List<int>>();

        while (randomCells.Count <= _trapAmount)
        {
            int rowToAdd = Random.Range(0, rowDim);
            int colToAdd = Random.Range(0, colDim);

            bool exists = randomCells.Any(x => x[0] == rowToAdd && x[1] == colToAdd);

            if (boardTemplate[rowToAdd, colToAdd] == CellType.Normal && !exists)
            {
                randomCells.Add(new List<int> { rowToAdd, colToAdd });
            }

        }
        return randomCells;
    }
    private Vector3 getCellPosition(int row, int col)
    {
        return new Vector3(col, 0, row);
    }

    public Cell GetHeroDeployCell() { 
       return _heroDeployCell;
    }
    

}
