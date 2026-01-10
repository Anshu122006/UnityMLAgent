using System.Collections.Generic;
using UnityEngine;

public class GlobalObjects : MonoBehaviour {
    public static GlobalObjects Instance;
    [SerializeField] public Camera mainCamera;
    [SerializeField] public List<Transform> startPoints;
    [SerializeField] public List<Transform> exitPoints;

    public Transform StartPoint => startPoints[Random.Range(0, startPoints.Count)];
    public Transform ExitPoint => exitPoints[Random.Range(0, exitPoints.Count)];

    private void Awake() {
        Instance = this;
    }
}
