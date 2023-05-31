using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject _board;
    private Board _boardController;

    //Heroes Prefabs
    [SerializeField]
    private GameObject _playerWarriorPerfab;

    //El Spawn cell es donde el personaje se crea. 
    [SerializeField]
    private GameObject _playerWarriorSpawnCell;

    //Heroes Deploy 
    [SerializeField]
    private Cell _playerDeployCell;

    //Game State
    [SerializeField]
    private GameState _gameState;

    //La celda que esta siendo hovered en el momento
    private GameObject _hoveredCell;

    private GameObject _hero;


    /*Utils*/
    [SerializeField]
    private LineDrawer _lineDrawer;

    /*Actions*/
    private List<Action> _currentActions;

    // Start is called before the first frame update
    void Start()
    {
        this._boardController = _board.GetComponent<Board>();
        if (_boardController != null)
        {
            Debug.Log("Board controller not found in Game Handler");
        }
        buildBoard();
        buildHeroes();
        _gameState = GameState.SelectingHero;
        _lineDrawer = this.GetComponent<LineDrawer>();
    }

    private void Update()
    {
        if (_gameState == GameState.SelectingHero) {
            selectHero();
        }
        else if (_gameState == GameState.SelectingAction)
        {
            mouseHover();
            OnClick();
        }
        
    }

    private void selectHero() {
        GameObject heroGo = checkUserClick();
        if (heroGo != null)
        {
            Hero hero = heroGo.GetComponent<Hero>();
            if (hero != null)
            {
                _currentActions = getAvailableActions(hero);
                foreach (Action action in _currentActions)
                {
                    action.DestinyCell.ShowAvaliable();
                }
                _gameState = GameState.SelectingAction;
            }
        }
    }

    private void buildBoard()
    {
        Board boardController = _board.GetComponent<Board>();
        if (boardController != null)
        {
            Debug.Log("Board controller not found in Game Handler");
        }

        boardController.BuildBoard();

        _playerDeployCell = boardController.GetHeroDeployCell();
    }

    private void buildHeroes() {
        //PlayerWarrior
        _hero = Instantiate(_playerWarriorPerfab, new Vector3(), Quaternion.identity);
        _hero.name = "Player Warrior";
        _hero.transform.parent = _playerWarriorSpawnCell.transform;
        _hero.transform.localPosition = new Vector3(0, 0, 0);
        Hero heroController = _hero.GetComponent<Hero>();
        heroController.Configure(false, 2);
    }

    /*Calculador de acciones*/
    private List<Action> getAvailableActions(Hero hero) { 
        List<Action> actions = new List<Action>();
        if (!hero.Deployed) {
            Action act = new Action();
            act.DestinyCell = _boardController.GetHeroDeployCell();
            act.ActionType = ActionType.Deploy;
            actions.Add(act);
        }

        return actions;
    }

    private GameObject checkUserClick() {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "PlayerSelect")
                    {
                        GameObject hitTransform = hit.transform.gameObject;
                        Renderer rend = hitTransform.GetComponent<Renderer>(); 
                        rend.enabled = true;
                        return hitTransform.transform.parent.gameObject;
                    }
                }
            }
            return null;
        }
        return null;
    }

    /**/
    public Cell highlightedCell;
    private void mouseHover()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Cellhighlight")
            {
                GameObject hitTransform = hit.transform.gameObject;
                if (_hoveredCell == null || _hoveredCell != hitTransform) {
                    _hoveredCell = hitTransform;
                    Cell hitCell = hitTransform.transform.parent.transform.parent.GetComponent<Cell>();
                    Debug.Log(hitCell);
                    if (IsCellOnActions(hitCell)) {
                        _lineDrawer.DrawCurveBetweenObjects(hitTransform, _hero);
                    }
                }
            }
            else {
                _hoveredCell = null;
                _lineDrawer.RemoveLine();
            }
        }
    }

    public void OnClick() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            /*La celda clickeable?*/
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Cellhighlight")
                {
                    Debug.Log("Se hizo click");
                    Transform hitTransform = hit.transform;
                    /*Obtengo el objeto celda*/
                    Cell hitCell = hitTransform.transform.parent.transform.parent.GetComponent<Cell>();
                    Debug.Log("Se hizo click" + hitCell);
                    //la hit cell es parte de las posibles acciones?
                    if (IsCellOnActions(hitCell)) {
                        hitCell.ShowSelected();
                        _gameState = GameState.ActonSelected;
                    }
                }
            }
        }
    }

    private bool IsCellOnActions(Cell cell) {
        for (int i = 0; i < _currentActions.Count; i++)
        {
            if (_currentActions[i].DestinyCell == cell)
            {
                return true;
            }
        }
        return false;
    }

}
