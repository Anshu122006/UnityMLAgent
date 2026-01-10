using UnityEngine;

public class PlayerControls : MonoBehaviour {
    [SerializeField, Range(0.3f, 1f)]
    private float sensitivity = 0.1f;

    [SerializeField] private Animations animationSO;
    [SerializeField] private GameObject visual;
    [SerializeField] private Transform cam;

    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float sprintSpeed = 6;

    private Animator anim;
    private Rigidbody rb;
    private InputManager input;
    private string curAnim;
    private string newAnim;
    private float yaw;
    private float pitch;

    private void Awake() {
        curAnim = animationSO.idle;
        newAnim = animationSO.idle;
    }

    private void Start() {
        input = InputManager.Instance;
        anim = visual.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        HandleAnimation();
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 turnDel = input.GetMouseDelta();

        yaw += turnDel.x * sensitivity * 300f * Time.fixedDeltaTime;
        pitch -= turnDel.y * sensitivity * 300f * Time.fixedDeltaTime;

        pitch = Mathf.Clamp(pitch, -20f, 20f);

        Quaternion rot = Quaternion.Euler(0f, yaw, 0f);
        cam.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        if (input.IsMovePressed() && input.IsSprintPressed()) {
            newAnim = animationSO.sprint;
            Vector3 del = transform.forward * sprintSpeed * Time.fixedDeltaTime;
            rb.Move(transform.position + del, rot);
        }
        else if (input.IsMovePressed()) {
            newAnim = animationSO.walk;
            Vector3 del = transform.forward * walkSpeed * Time.fixedDeltaTime;
            rb.Move(transform.position + del, rot);
        }
        else {
            newAnim = animationSO.idle;
            rb.Move(transform.position, rot);
        }
    }



    private void HandleAnimation() {
        if (newAnim == curAnim) return;

        anim.CrossFade(newAnim, 0.15f, 0);
        curAnim = newAnim;
    }
}
