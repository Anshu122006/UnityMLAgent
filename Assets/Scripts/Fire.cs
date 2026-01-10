using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    [SerializeField] private LayerMask agentLayer;
    [SerializeField] private FireData data;

    private Coroutine damageDelayCoroutine;

    private void OnTriggerStay(Collider col) {
        if ((agentLayer & (1 << col.gameObject.layer)) != 0) {
            if (damageDelayCoroutine == null) {
                Debug.Log(data.damage);
                damageDelayCoroutine = StartCoroutine(DamageDelayTimer());
            }
        }
    }

    private IEnumerator DamageDelayTimer() {
        yield return new WaitForSeconds(data.damageDelay);
        damageDelayCoroutine = null;
    }
}
