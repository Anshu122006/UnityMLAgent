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
            Camera mainCamera = GlobalObjects.Instance.mainCamera;
            mainCamera.transform.parent = mainPos;
            mainCamera.transform.localPosition = Vector3.zero;
            mainCamera.transform.localRotation = Quaternion.identity;
            inMainPos = true;
        }

        // if (Input.GetMouseButtonDown(0)) {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit)) {
        //         GameObject clickedObject = hit.collider.gameObject;
        //         if (clickedObject == lastClickedObject &&
        //         Time.time - lastClickTime <= doubleClickTime) {
        //             Debug.Log(clickedObject);
        //             if (clickedObject.TryGetComponent<AgentInteractionManager>(out AgentInteractionManager agent)
        //                     && inMainPos) {
        //                 agent.SetCameraPos();
        //                 inMainPos = false;
        //             }
        //         }
        //         lastClickTime = Time.time;
        //         lastClickedObject = clickedObject;
        //     }
        // }
    }
}
