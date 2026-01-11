using System;
using UnityEngine;

public class GlobalCameraManager : MonoBehaviour {
    public event Action<bool> OnMainPosChange;
    public static GlobalCameraManager Instance;
    [SerializeField] private Transform mainPos;

    // [SerializeField] private float doubleClickTime = 0.3f;
    // private float lastClickTime;
    // private GameObject lastClickedObject;

    private bool inMainPos = true;

    public bool InMainPos { get { return inMainPos; } set { inMainPos = value; OnMainPosChange?.Invoke(inMainPos); } }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !inMainPos) {
            SetMainCameraPos();
        }
    }

    public void SetMainCameraPos() {
        if (InMainPos) return;
        Camera mainCamera = GlobalObjects.Instance.mainCamera;
        mainCamera.transform.parent = mainPos;
        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.transform.localRotation = Quaternion.identity;
        InMainPos = true;
    }
}
