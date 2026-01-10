using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Collections.Generic;

public class AvoidFireAgent : Agent {
    [SerializeField] private AgentStateManager stateManager;
    [SerializeField] private MovementManager moveManager;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask fireLayer;
    [SerializeField] private LayerMask rewardLayer;
    [SerializeField] private LayerMask exitLayer;

    private List<TriggerChecker> checkers = new();

    public override void OnEpisodeBegin() {
        transform.position = GlobalObjects.Instance.StartPoint.position;
        for (int i = 0; i < checkers.Count; i++)
            checkers[i].isChecked = false;
        checkers = new();
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(GlobalObjects.Instance.ExitPoint.position);
    }

    public override void OnActionReceived(ActionBuffers actions) {
        int state = actions.DiscreteActions[0];

        if (state == 0) {
            stateManager.agentState = AgentStateManager.AgentState.idle;
        }
        else if (state == 1) {
            stateManager.agentState = AgentStateManager.AgentState.walk;

            float moveX = actions.ContinuousActions[0];
            float moveZ = actions.ContinuousActions[1];

            Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;
            moveManager.ChangeDir(moveDir);
        }
        else if (state == 2) {
            stateManager.agentState = AgentStateManager.AgentState.sprint;

            float moveX = actions.ContinuousActions[0];
            float moveZ = actions.ContinuousActions[1];

            Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;
            moveManager.ChangeDir(moveDir);
        }
    }

    private void OnCollisionStay(Collision collision) {
        if ((wallLayer & (1 << collision.gameObject.layer)) != 0) {
            AddReward(-0.3f);
        }
        else if ((obstacleLayer & (1 << collision.gameObject.layer)) != 0) {
            AddReward(-2);
        }
    }

    private void OnTriggerStay(Collider other) {
        if ((obstacleLayer & (1 << other.gameObject.layer)) != 0) {
            AddReward(-3);
        }
        else if ((rewardLayer & (1 << other.gameObject.layer)) != 0) {
            if (other.TryGetComponent<TriggerChecker>(out TriggerChecker checker) && !checker.isChecked) {
                AddReward(10);
                checker.isChecked = true;
                checkers.Add(checker);
            }
        }
        else if ((exitLayer & (1 << other.gameObject.layer)) != 0) {
            Debug.Log("Exit reached by " + gameObject.name);
            AddReward(30);
            EndEpisode();
        }
    }
}
