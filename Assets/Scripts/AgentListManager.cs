using System.Collections.Generic;
using UnityEngine;

public class AgentListManager : MonoBehaviour {
    [SerializeField] private Transform content;
    [SerializeField] private GameObject listItemPref;
    private Dictionary<string, AgentListItem> itemsDict = new();
    public void AddItem(GameObject agent) {
        AgentListItem listItem = Instantiate(listItemPref, content).GetComponent<AgentListItem>();
        string id = listItem.Initialize(agent, this);
        itemsDict[id] = listItem;
    }

    public void removeItem(string id) {
        itemsDict.Remove(id);
    }
}
