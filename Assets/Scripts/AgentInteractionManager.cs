using UnityEngine;

public class AgentInteractionManager : MonoBehaviour {
    [SerializeField] private Transform cameraPos;

    public void SetCameraPos() {
        Camera mainCamera = GlobalObjects.Instance.mainCamera;
        mainCamera.transform.parent = cameraPos;
        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.transform.localRotation = Quaternion.identity;
    }
}
