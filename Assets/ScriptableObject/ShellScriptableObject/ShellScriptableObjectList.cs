using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShellListScriptableObject", menuName = "ScriptableObject/NewShellListScriptableObject")]
public class ShellScriptableObjectList : ScriptableObject
{
    public List<ShellScriptableObject> shells;
}
