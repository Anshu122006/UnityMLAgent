using UnityEngine;

public class AgentInteractionManager : MonoBehaviour {
    [SerializeField] private Transform cameraPos;

    public void SetCameraPos() {
        Camera mainCamera = GlobalObjects.Instance.mainCamera;
        GlobalCameraManager.Instance.InMainPos = false;
        mainCamera.orthographic = false;
        mainCamera.transform.parent = cameraPos;
        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.transform.localRotation = Quaternion.identity;
    }
}
