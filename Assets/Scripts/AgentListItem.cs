using System;
using TMPro;
using UnityEngine;

public class AgentListItem : MonoBehaviour {
    public AgentInteractionManager agent;
    public string agentId;

    public TextMeshProUGUI nameText;
    private AgentListManager listManager;
    public string Initialize(GameObject agent, AgentListManager listManager) {
        agentId = Guid.NewGuid().ToString();
        this.listManager = listManager;
        this.agent = agent.GetComponent<AgentInteractionManager>();

        nameText.text = agentId.Substring(0, 15);
        return agentId;
    }

    public void RemoveItem() {
        GlobalCameraManager.Instance.SetMainCameraPos();
        Destroy(agent.gameObject);
        agent = null;
        listManager.removeItem(agentId);
        Destroy(gameObject);
    }

    public void SetAgentCameraView() {
        agent.SetCameraPos();
    }
}
