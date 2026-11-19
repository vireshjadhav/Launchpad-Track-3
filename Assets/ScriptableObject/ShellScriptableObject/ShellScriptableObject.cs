using UnityEngine;

[CreateAssetMenu(fileName = "ShellScriptableObject", menuName = "ScriptableObject/NewShellScriptableObject")]
public class ShellScriptableObject : ScriptableObject
{
    public string shellName;
    public ShellType shellTypes;
    public float explosionRadius = 5.0f;
    public float explosionForce = 1000.0f;
    public float maxDamage = 100.0f;
    public float maxLifeTime = 5.0f;
    public Vector3 size = new Vector3(1.0f, 1.0f, 1.0f);
}
