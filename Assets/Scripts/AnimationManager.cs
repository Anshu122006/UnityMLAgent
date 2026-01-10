using UnityEngine;
using UnityEngine.Assertions.Must;

public class AnimationManager : MonoBehaviour {
    [SerializeField] private Animator anim;
    [SerializeField] private AgentStateManager stateManager;

    private string curAnim;

    private void Update() {
        UpdateAnimation();
    }
    private void UpdateAnimation() {
        if (stateManager.agentState == AgentStateManager.AgentState.idle && curAnim != "idle") {
            anim.CrossFade("idle", 0.1f);
            curAnim = "idle";
        }
        else if (stateManager.agentState == AgentStateManager.AgentState.walk && curAnim != "walk") {
            anim.CrossFade("walk", 0.1f);
            curAnim = "walk";
        }
        else if (stateManager.agentState == AgentStateManager.AgentState.sprint && curAnim != "sprint") {
            anim.CrossFade("sprint", 0.1f);
            curAnim = "sprint";
        }
    }
}
