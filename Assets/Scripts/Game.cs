using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject _board;
    private Board _boardController;
    // Start is called before the first frame update
    void Start()
    {
        this._boardController = _board.GetComponent<Board>();
        if (_boardController != null)
        {
            Debug.Log("Board controller not found in Game Handler");
        }
        buildBoard();
    }

    private void buildBoard()
    {
        Board boardController = _board.GetComponent<Board>();
        if (boardController != null)
        {
            Debug.Log("Board controller not found in Game Handler");
        }

        boardController.BuildBoard();
    }
}
