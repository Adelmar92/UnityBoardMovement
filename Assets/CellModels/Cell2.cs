using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell2 : MonoBehaviour
{
    /*prefabs*/
    [SerializeField]
    private GameObject _grass;
    [SerializeField]
    private GameObject _availableAura;
    [SerializeField]
    private GameObject _selectedAura;


    [SerializeField]
    private bool _revealed;
    [SerializeField]
    private bool _available;
    [SerializeField]
    private bool _selected;


    // Update is called once per frame
    void Update()
    {
        if (_revealed) {
            Reveal();
        }

        SetAvailable(_available);
        SetSelected(_selected);
    }

    public void Reveal() { 
        _grass.SetActive(false);
    }

    public void SetAvailable(bool available) {
        _availableAura.SetActive(available);
    }

    public void SetSelected(bool available)
    {
        _selectedAura.SetActive(available);
    }
}
