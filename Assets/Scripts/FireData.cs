using UnityEngine;

[CreateAssetMenu(fileName = "FireData", menuName = "Scriptable Objects/FireData")]
public class FireData : ScriptableObject {
    public float damage = 10;
    public float damageDelay = 1;
}
