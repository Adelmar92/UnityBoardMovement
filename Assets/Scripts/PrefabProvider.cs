using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PrefabProvider : MonoBehaviour
{
    [SerializeField]
    private GameObject _normalCellPrefab;
    [SerializeField]
    private GameObject _playerStartCellPrefab;
    [SerializeField]
    private GameObject _chestCellPrefab;

    public GameObject GetNormalCellPrefab()
    {
        return _normalCellPrefab;
    }
    public GameObject GetPlayerStartCellPrefab()
    {
        return _playerStartCellPrefab;
    }
    public GameObject GetChestCellPrefab()
    {
        return _chestCellPrefab;
    }

}

