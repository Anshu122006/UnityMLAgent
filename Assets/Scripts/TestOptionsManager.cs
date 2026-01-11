using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TestOptionsManager : MonoBehaviour {
    [Header("Basic fields")]
    [SerializeField] private Animator anim;
    [SerializeField] private Transform visPos;
    [SerializeField] private Transform hidePos;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private LayerMask fireLayer;

    [Header("Related to the arrow Button")]
    [SerializeField] private GameObject arrowOpen;
    [SerializeField] private GameObject arrowClose;

    [Header("Related to camera")]
    [SerializeField] private GameObject orthographic;
    [SerializeField] private GameObject perspective;
    [SerializeField] private GameObject fix;

    [Header("Components")]
    [SerializeField] private Button addFire;
    [SerializeField] private Button removeFire;
    [SerializeField] private Button addAgent;
    [SerializeField] private AgentListManager listManager;

    [Header("Prefabs")]
    [SerializeField] private GameObject firePref;
    [SerializeField] private GameObject agentPref;

    [Header("Cursor Icons")]
    [SerializeField] Texture2D fireIcon;
    [SerializeField] Texture2D fireExtinguisherIcon;
    [SerializeField] Texture2D agentIcon;

    [Header("Booleans")]
    public bool addFireEnabled = false;
    public bool removeFireEnabled = false;
    public bool addAgentEnabled = false;
    private bool wasOrthographic = false;

    private void Start() {
        GlobalCameraManager.Instance.OnMainPosChange += HandleCameraPosChange;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                LayerMask surface = 1 << hit.collider.gameObject.layer;
                if ((floorLayer & surface) != 0 && addFireEnabled) {
                    Instantiate(firePref, hit.point, Quaternion.identity);
                }
                else if ((floorLayer & surface) != 0 & addAgentEnabled) {
                    GameObject agent = Instantiate(agentPref, hit.point, quaternion.identity);
                    listManager.AddItem(agent);
                }
                else if ((fireLayer & surface) != 0) {
                    Destroy(hit.collider.gameObject);
                }
                Debug.Log(surface);
            }
        }
    }

    public void stayAtHidePos() {
        transform.position = hidePos.position;
        arrowClose.SetActive(false);
        arrowOpen.SetActive(true);
    }
    public void stayAtVisPos() {
        transform.position = visPos.position;
        arrowOpen.SetActive(false);
        arrowClose.SetActive(true);
    }

    public void SlideIn() {
        anim?.CrossFade("SlideIn", 0.3f);
    }

    public void SlideOut() {
        anim.CrossFade("SlideOut", 0.3f);
    }

    public void ChangeToOrthographicView() {
        if (fix.activeSelf) return;

        Camera mainCamera = GlobalObjects.Instance.mainCamera;
        mainCamera.orthographic = true;
        orthographic.SetActive(false);
        perspective.SetActive(true);

        wasOrthographic = true;
    }

    public void ChangeToPerspectiveView() {
        if (fix.activeSelf) return;

        Camera mainCamera = GlobalObjects.Instance.mainCamera;
        mainCamera.orthographic = false;
        perspective.SetActive(false);
        orthographic.SetActive(true);

        wasOrthographic = false;
    }

    private void HandleCameraPosChange(bool inMainPos) {
        if (inMainPos) {
            orthographic.SetActive(false);
            perspective.SetActive(false);
            fix.SetActive(false);
            if (wasOrthographic) {
                orthographic.SetActive(true);
                GlobalObjects.Instance.mainCamera.orthographic = true;
            }
            else {
                perspective.SetActive(true);
                GlobalObjects.Instance.mainCamera.orthographic = false;
            }

            addFire.interactable = true;
            removeFire.interactable = true;
            addAgent.interactable = true;
        }
        else {
            orthographic.SetActive(false);
            perspective.SetActive(false);
            fix.SetActive(true);

            addFireEnabled = false;
            removeFireEnabled = false;
            addAgentEnabled = false;

            addFire.interactable = false;
            removeFire.interactable = false;
            addAgent.interactable = false;
        }
    }

    public void EnableAddFire() {
        if (addFireEnabled) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            addFireEnabled = false;
        }
        else {
            Vector2 hotspot = new Vector2(fireIcon.width * 0.5f, fireIcon.height * 0.5f);
            Cursor.SetCursor(fireIcon, hotspot, CursorMode.Auto);

            removeFireEnabled = false;
            addAgentEnabled = false;
            addFireEnabled = true;
        }
    }

    public void EnableRemoveFire() {
        if (removeFireEnabled) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            removeFireEnabled = false;
        }
        else {
            Vector2 hotspot = new Vector2(fireExtinguisherIcon.width * 0.5f, fireExtinguisherIcon.height * 0.5f);
            Cursor.SetCursor(fireExtinguisherIcon, hotspot, CursorMode.Auto);

            addFireEnabled = false;
            addAgentEnabled = false;
            removeFireEnabled = true;
        }
    }

    public void EnableAddAgent() {
        if (addAgentEnabled) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            addAgentEnabled = false;
        }
        else {
            Vector2 hotspot = new Vector2(agentIcon.width * 0.5f, agentIcon.height * 0.5f);
            Cursor.SetCursor(agentIcon, hotspot, CursorMode.Auto);

            addFireEnabled = false;
            removeFireEnabled = false;
            addAgentEnabled = true;
        }
    }
}
