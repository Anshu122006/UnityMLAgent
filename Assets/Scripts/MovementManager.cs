using UnityEngine;

public class MovementManager : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AgentStateManager stateManager;
    [SerializeField] private float walkSpeed = 1;
    [SerializeField] private float sprintSpeed = 2;
    [SerializeField] private float rotateSpeed = 6f;

    private Vector3 curDir;
    private Vector3 newDir;

    private void Update() {
        if (newDir != curDir) {
            transform.forward = Vector3.Lerp(transform.forward, newDir, rotateSpeed * Time.deltaTime).normalized;
            curDir = transform.forward;
        }
    }

    private void FixedUpdate() {
        if (stateManager.agentState != AgentStateManager.AgentState.idle) {
            Vector3 dir = transform.forward.normalized;
            float moveDist = (stateManager.agentState == AgentStateManager.AgentState.walk ? walkSpeed : sprintSpeed) * Time.deltaTime;
            rb.MovePosition(transform.position + dir * moveDist);
        }
    }

    public void ChangeDir(Vector3 val) {
        newDir = val.normalized;
    }
}
