using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private float spawnOffset = 1.1f;
    [SerializeField] private float swayForce = 1f;
    [SerializeField] private float maxTiltAngle = 30f;
    [SerializeField] private TextMeshProUGUI scoreText;

    private List<GameObject> blocks = new List<GameObject>();
    private Camera mainCam;
    private float initialCamY;

    void Start()
    {
        mainCam = Camera.main;
        initialCamY = mainCam.transform.position.y;
        UpdateScore();
    }

    void Update()
    {
        HandleInput();
        ApplySway();
        CheckTilt();
    }

    void HandleInput()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            SpawnBlock(Input.mousePosition);
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            SpawnBlock(Input.GetTouch(0).position);
#endif
    }

    void SpawnBlock(Vector2 screenPos)
    {
        float depth = -mainCam.transform.position.z;
        Vector3 worldPoint = mainCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));

        float y = 0.5f + blocks.Count * spawnOffset;
        Vector3 spawnPos = new Vector3(worldPoint.x, y, 0);

        GameObject b = Instantiate(blockPrefab, spawnPos, Quaternion.identity);

        float sx = Random.Range(0.8f, 1.2f);
        float sz = Random.Range(0.8f, 1.2f);
        b.transform.localScale = new Vector3(sx, 1f, sz);

        Renderer rend = b.GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = Random.ColorHSV();

        blocks.Add(b);
        UpdateScore();
        UpdateCameraPosition();
    }

    void ApplySway()
    {
        foreach (var b in blocks)
        {
            Rigidbody rb = b.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(Vector3.right * swayForce * blocks.Count * Time.deltaTime);
        }
    }

    void CheckTilt()
    {
        if (blocks.Count < 2) return;

        Vector3 bottom = blocks[0].transform.position;
        Vector3 top = blocks[blocks.Count - 1].transform.position;

        float angle = Vector3.Angle(Vector3.up, top - bottom);

        if (angle > maxTiltAngle)
            ResetTower();  
    }

    void ResetTower()
    {
        foreach (var b in blocks)
            Destroy(b);

        blocks.Clear();
        UpdateScore();
        UpdateCameraPosition();
    }

    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + blocks.Count;
    }

    void UpdateCameraPosition()
    {
        Vector3 camPos = mainCam.transform.position;
        camPos.y = initialCamY + blocks.Count * 0.4f;
        mainCam.transform.position = camPos;
    }
}
