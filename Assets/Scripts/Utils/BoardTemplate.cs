using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardTemplate
{
    public static CellType[,] GetTemplate()
    {
        return new CellType[,] {
            {CellType.Normal,CellType.Normal,CellType.PStart,CellType.Normal,CellType.PStart,CellType.Normal,CellType.Normal},
            {CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal},
            {CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal},
            {CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal},
            {CellType.Chest1,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Chest2},
            {CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal},
            {CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal,CellType.Normal},
            {CellType.Normal,CellType.Normal,CellType.Normal,CellType.Chest3,CellType.Normal,CellType.Normal,CellType.Normal},
        }; ;
    }

}