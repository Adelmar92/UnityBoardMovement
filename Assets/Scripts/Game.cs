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
    public Board getBoard() { return _boardController; }

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
    public void setGameState(GameState gameState) { _gameState = gameState; }
    //La celda que esta siendo hovered en el momento
    private GameObject _hoveredCell;

    private GameObject _hero;

    private ActionController _actionController;


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
        _actionController = this.GetComponent<ActionController>();
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

    private void buildHeroes()
    {
        //PlayerWarrior
        _hero = Instantiate(_playerWarriorPerfab, new Vector3(), Quaternion.identity);
        _hero.name = "Player Warrior";
        _hero.transform.parent = _playerWarriorSpawnCell.transform;
        _hero.transform.localPosition = new Vector3(0, 0, 0);
        Hero heroController = _hero.GetComponent<Hero>();
        heroController.Configure(false, 2);
    }

    private void Update()
    {
        if (_gameState == GameState.SelectingHero)
        {
            selectHero();
        }
        else if (_gameState == GameState.SelectingAction)
        {
            mouseHover();
            checkUserClickingCell();
        }
        else if (_gameState == GameState.MovingHero) //esto podria ser un performing action
        {
            bool completed = _actionController.MoveHeroToPosition();
            if (completed) {
                _gameState = GameState.SelectingHero;
            }
        }
    }

    private void selectHero() {
        Hero hero = checkUserClickingHero();
        if (hero != null)
        {
            _actionController.SetSelectedHero(hero);
            List<Action> actions = _actionController.CalculatePossibleActions(hero);
            _actionController.EnableActions(actions);
            _gameState = GameState.SelectingAction;
        }
    }

    private Hero checkUserClickingHero() {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "PlayerSelect")
                {
                    Debug.Log("Golpie a un eroe");
                    Hero hero = hit.transform.parent.GetComponent<Hero>();
                    hero.SetSelected(true);
                    return hero;
                }
            }
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
                Cell hitCell = hitTransform.transform.parent.transform.parent.GetComponent<Cell>();
                _actionController.HighLightCell(hitCell);
            }
            else {
                _actionController.UnHighLightCell();
            }
        }
    }

    public void checkUserClickingCell() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            /*La celda clickeable?*/
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Cellhighlight")
                {
                    Transform hitTransform = hit.transform;
                    /*Obtengo el objeto celda*/
                    Cell hitCell = hitTransform.transform.parent.transform.parent.GetComponent<Cell>();
                    _actionController.SetActionAsSelected(hitCell);
                }
            }
        }
    }

   

}
