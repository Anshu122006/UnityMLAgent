using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour {
    [Header("Prefabs")]
    [SerializeField] private List<Transform> firePrefs;

    [Header("Layer masks")]
    [SerializeField] private LayerMask fireLayer;
    [SerializeField] private LayerMask furnitureLayer;
    [SerializeField] private LayerMask flamableLayer;

    [Header("Fire parameters")]
    [SerializeField][Range(0, 6)] private float spreadRange;
    [SerializeField][Range(0, 6)] private float spreadDelay;
    [SerializeField][Range(0f, 3)] private float density;

    public bool fireOn;
    private int maxFireCount;

    private BoxCollider col;
    private List<Vector3> emptyPos = new();
    private List<Vector3> occupiedPos = new();
    private List<GameObject> fireParticles = new();

    private Coroutine spreadDelayCoroutine;

    private void Start() {
        col = GetComponent<BoxCollider>();
        Vector3 size = col.bounds.size;
        float volume = size.x * size.y * size.z;
        maxFireCount = Mathf.Max(1, (int)(volume * density));
        GenerateFirePositions();
    }

    private void OnMouseDown() {
        if (fireOn) StopFire();
        else StartFire();
    }

    public void StartFire() {
        if (fireOn) return;
        fireOn = true;
        CatchFire();
    }

    public void StopFire() {
        if (!fireOn) return;
        for (int i = 0; i < fireParticles.Count; i++) Destroy(fireParticles[i]);
        if (spreadDelayCoroutine != null) {
            StopCoroutine(SpreadDelayTimer());
            spreadDelayCoroutine = null;
        }
        GenerateFirePositions();
        fireOn = false;
    }

    private void CatchFire() {
        if (!fireOn) return;
        if (firePrefs.Count == 0 || emptyPos.Count == 0) return;

        int i = Random.Range(0, emptyPos.Count);
        int j = Random.Range(0, firePrefs.Count);

        Transform fire = Instantiate(firePrefs[j], emptyPos[i], Quaternion.identity);

        occupiedPos.Add(emptyPos[i]);
        fireParticles.Add(fire.gameObject);
        emptyPos.RemoveAt(i);

        Vector3 range = col.bounds.size * 0.5f + Vector3.one * spreadRange;
        Collider[] targets = Physics.OverlapBox(transform.position, range, Quaternion.identity, furnitureLayer);
        foreach (var target in targets) {
            if (target.transform.TryGetComponent<Furniture>(out Furniture fur)) {
                if (!fur.fireOn) {
                    fur.StartFire();
                    break;
                }
            }
        }
        spreadDelayCoroutine = StartCoroutine(SpreadDelayTimer());
    }

    private void GenerateFirePositions() {
        emptyPos = new();
        occupiedPos = new();
        Bounds b = col.bounds;
        int cellsPerAxis = Mathf.CeilToInt(Mathf.Pow(maxFireCount, 1f / 3f));

        Vector3 cellSize = new Vector3(
            b.size.x / cellsPerAxis,
            b.size.y / cellsPerAxis,
            b.size.z / cellsPerAxis
        );

        Vector3 start = b.min + cellSize * 0.5f;

        for (int x = 0; x < cellsPerAxis; x++)
            for (int y = 0; y < cellsPerAxis; y++)
                for (int z = 0; z < cellsPerAxis; z++) {
                    Vector3 pos = start + new Vector3(
                        x * cellSize.x,
                        y * cellSize.y,
                        z * cellSize.z
                    );
                    emptyPos.Add(pos);
                }
    }

    private IEnumerator SpreadDelayTimer() {
        float time = Random.Range(spreadDelay * 0.8f, spreadDelay);
        yield return new WaitForSeconds(time);
        CatchFire();
    }
}
