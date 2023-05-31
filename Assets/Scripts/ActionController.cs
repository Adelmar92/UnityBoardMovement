using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    public Hero SelectedHero;

    [SerializeField]
    private List<Action> _availableActions;

    [SerializeField]
    private Action _selectedAction;

    private Game game;
    /*Utils*/
    [SerializeField]
    private LineDrawer _lineDrawer;

    //La celda que esta siendo hovered en el momento
    private Cell _hoveredCell;

    [SerializeField]
    private GameObject _uiGameObject;
    private Ui uiController;

    

    void Start()
    {
         game = this.gameObject.GetComponent<Game>();
        _lineDrawer = this.GetComponent<LineDrawer>();
        uiController = _uiGameObject.GetComponent<Ui>();
    }

    /*IN base of a hero it returns the possible actions available for that hero*/
    public List<Action> CalculatePossibleActions(Hero hero) {
        List<Action> actions = new List<Action>();
        if (!hero.Deployed)
        {
            Action act = new Action();
            act.DestinyCell = game.getBoard().GetHeroDeployCell();
            act.ActionType = ActionType.Deploy;
            actions.Add(act);
        }
        else {
            Debug.Log(SelectedHero);
            Cell cell = SelectedHero.transform.parent.GetComponent<Cell>();
            actions = GetPossibleActions(game.getBoard().GetCells(), cell.Row, cell.Col, SelectedHero.Movement);
        }

        return actions;
    }

    

    public void EnableActions(List<Action> actions) {
        foreach (Action action in actions)
        {
            action.DestinyCell.ShowAvaliable();
        }
        _availableActions = actions;
    }

    public void HighLightCell(Cell cell) {
        if (_hoveredCell == null || _hoveredCell != cell)
        {
            if (isCellOnActions(cell))
            {
                _lineDrawer.DrawCurveBetweenObjects(cell.gameObject, SelectedHero.gameObject);
                _hoveredCell = cell;
            }
        }
    }

    public void UnHighLightCell() {
        _hoveredCell = null;
        _lineDrawer.RemoveLine();
    }

    private bool isCellOnActions(Cell cell)
    {
        return _availableActions.Any(a => a.DestinyCell == cell);
    }

    public void SetSelectedHero(Hero selectedHero) {
        SelectedHero = selectedHero;
    }

    public bool SetActionAsSelected(Cell cell){
        if (isCellOnActions(cell))
        {
            if (_selectedAction != null) {
                _selectedAction.DestinyCell.ShowAvaliable();
            }
            Action actionOnCell = _availableActions.First(a => a.DestinyCell == cell);
            cell.ShowSelected();
            _selectedAction = actionOnCell;
            uiController.updateActionUI(actionOnCell.ActionType);
            return true;
        }
        return false;
    }

    public void StartMovingAction() {
        if (SelectedHero.Deployed == false) { 
            SelectedHero.Deployed = true;
        }
        game.setGameState(GameState.MovingHero);
        UnHighLightCell();
        _lineDrawer.RemoveLine();
        for (int i = 0; i < _availableActions.Count; i++) {
            _availableActions[i].DestinyCell.HideAllSelectors();
        }
    }

    public bool MoveHeroToPosition() {
        float speed = 3f;
        Vector3 newPosition = Vector3.Lerp(SelectedHero.gameObject.transform.position, _selectedAction.DestinyCell.gameObject.transform.position, speed * Time.deltaTime);

        // Asigna la nueva posición al objeto
        SelectedHero.gameObject.transform.position = newPosition;


        float distance = Vector3.Distance(SelectedHero.gameObject.transform.position, _selectedAction.DestinyCell.gameObject.transform.position);
        // Verifica si el objeto ha alcanzado su posición final
        if (distance < 0.05f)
        {
            SelectedHero.gameObject.transform.position = _selectedAction.DestinyCell.gameObject.transform.position;
            SelectedHero.SetSelected(false);
            SelectedHero.transform.parent = _selectedAction.DestinyCell.transform;

            if (_selectedAction.DestinyCell.GetCellType() == CellType.Normal) {
                _selectedAction.DestinyCell.Reveal();
            }

            SelectedHero = null;
            _selectedAction = null;
            _availableActions = null;
            return true;
        }

        return false;
    }

    public List<Action> GetPossibleActions(Cell[,] _cells, int playerX, int playerY, int movementCount)
    {
        List<Action> possibleActions = new List<Action>();

        int currentExpand = 1;

        bool upUnrevealed = false;
        bool downUnrevealed = false;
        bool rightUnrevealed = false;
        bool leftUnrevealed = false;


        while (currentExpand <= movementCount) {

            // Verificar movimiento hacia Izquierda
            if (playerY - currentExpand >= 0 && !leftUnrevealed)
            {
                Cell destinationCell = _cells[playerX, playerY - currentExpand];
                if (destinationCell.GetCellType() == CellType.Normal) {
                    possibleActions.Add(new Action { ActionType = ActionType.Move, DestinyCell = destinationCell });
                    if (!destinationCell.IsRevealed())
                    {
                        leftUnrevealed = true;
                    }
                }
                
            }

            // Verificar movimiento hacia derecha
            if (playerY + currentExpand < _cells.GetLength(1) && !rightUnrevealed)
            {
                Cell destinationCell = _cells[playerX, playerY + currentExpand];
                if (destinationCell.GetCellType() == CellType.Normal)
                {
                    possibleActions.Add(new Action { ActionType = ActionType.Move, DestinyCell = destinationCell });
                    if (!destinationCell.IsRevealed())
                    {
                        rightUnrevealed = true;
                    }
                }
            }
        

            // Verificar movimiento hacia la Abajo
            if (playerX - currentExpand >= 0 && !downUnrevealed)
            {
                Cell destinationCell = _cells[playerX - currentExpand, playerY];
                if (destinationCell.GetCellType() == CellType.Normal)
                {
                    possibleActions.Add(new Action { ActionType = ActionType.Move, DestinyCell = destinationCell });
                    if (!destinationCell.IsRevealed())
                    {
                        downUnrevealed = true;
                    }
                }
            }

            // Verificar movimiento hacia la Arriba
            if (playerX + currentExpand < _cells.GetLength(0) && !upUnrevealed)
            {
                Cell destinationCell = _cells[playerX + currentExpand, playerY];
                if (destinationCell.GetCellType() == CellType.Normal)
                {
                    possibleActions.Add(new Action { ActionType = ActionType.Move, DestinyCell = destinationCell });
                    if (!destinationCell.IsRevealed())
                    {
                        upUnrevealed = true;
                    }
                }
            }

            currentExpand++;
        }

        
        

        return possibleActions;
    }
}
