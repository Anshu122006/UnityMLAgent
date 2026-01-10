using UnityEngine;

public class AgentStateManager : MonoBehaviour {
    public enum AgentState {
        idle,
        walk,
        sprint,
    }

    public AgentState agentState;
}
