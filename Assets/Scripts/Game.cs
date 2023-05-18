using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private 

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
    }

    private void Update()
    {
        if (_gameState == GameState.SelectingHero) {
            GameObject heroGo = checkUserClick();
            if (heroGo != null)
            {
                Hero hero = heroGo.GetComponent<Hero>();
                if (hero != null)
                {
                    List<Action> actions = getAvailableActions(hero);
                    foreach (Action action in actions)
                    {
                        action.DestinyCell.ShowAvaliable();
                    }
                    _gameState = GameState.SelectingAction;
                }
            }
        }
        else if (_gameState == GameState.SelectingAction)
        {
            mouseHover();
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
        GameObject playerWarrior = Instantiate(_playerWarriorPerfab, new Vector3(), Quaternion.identity);
        playerWarrior.name = "Player Warrior";
        playerWarrior.transform.parent = _playerWarriorSpawnCell.transform;
        playerWarrior.transform.localPosition = new Vector3(0, 0, 0);
        Hero heroController = playerWarrior.GetComponent<Hero>();
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
                print("Hit asfsgag");
                GameObject hitTransform = hit.transform.gameObject;
                Renderer rend = hitTransform.GetComponent<Renderer>();
                rend.enabled = true;
                Cell hoverCell = hitTransform.transform.parent.parent.GetComponent<Cell>();
                if (highlightedCell != null && hoverCell != highlightedCell) {
                    highlightedCell.HighLight(false);
                }
                highlightedCell = hoverCell;
                highlightedCell.HighLight(true);
            }
            else {
                if (highlightedCell != null) {
                    highlightedCell.HighLight(false);
                }
            }
        }
    }
}
