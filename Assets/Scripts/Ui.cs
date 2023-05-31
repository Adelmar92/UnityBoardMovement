using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ui : MonoBehaviour
{
    private Button buttonDeployHero;
    private Button buttonMoveHero;


    private ActionController actionController;
    [SerializeField]
    private GameObject gameHandler;
    void Start()
    {
        actionController = gameHandler.GetComponent<ActionController>();
    }
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        buttonDeployHero = root.Q<Button>("DeployHero");
        buttonMoveHero = root.Q<Button>("MoveHero");


        buttonDeployHero.visible = false;
        buttonMoveHero.visible = false;

        buttonDeployHero.clicked += () => deployHeroClicked();
        buttonMoveHero.clicked += () => moveHeroClicked();
    }

    private void deployHeroClicked() {
        actionController.StartMovingAction();
        buttonDeployHero.visible = false;
    }

    private void moveHeroClicked()
    {
        actionController.StartMovingAction();
        buttonMoveHero.visible = false;
    }

    public void updateActionUI(ActionType actionType) {
        buttonDeployHero.visible = false;
        switch (actionType) {
            case ActionType.Deploy:
                buttonDeployHero.visible = true;
                break;
            case ActionType.Move:
                buttonMoveHero.visible = true;
                break ;
        }
    }
}
