using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TankScriptableObjectList", menuName = "ScriptableObject/NewTankListScriptableObject")]
public class TankScriptableObjectList : ScriptableObject
{
    public List<TankScriptableObject> tanks;
}
