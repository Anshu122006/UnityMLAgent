using UnityEngine;

[CreateAssetMenu(fileName = "Animations", menuName = "Scriptable Objects/Animations")]
public class Animations : ScriptableObject
{
    public string idle = "idle";
    public string walk = "walk";
    public string sprint = "sprint";
}
