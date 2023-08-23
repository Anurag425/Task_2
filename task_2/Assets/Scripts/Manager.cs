using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject circleParent;
    public int spawnAreaPadding;
    public const float RESOLUTION = 0.1f;

    private List<GameObject> circles = new List<GameObject>();
    private Camera _cam;
    [SerializeField] private Line _linePrefab;

    private Line _currentLine;
    private void Start()
    {
        _cam = Camera.main;
        RestartGame();
    }

    private void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        }

        if (Input.GetMouseButton(0))
        {
            _currentLine.SetPosition(mousePos);
        }

        if(Input.GetMouseButtonUp(0))
        {
            CheckForIntersection();
            Destroy(_currentLine.gameObject);
        }

    }

    public void RestartGame()
    {
        foreach (GameObject circle in circles)
        {
            Destroy(circle);
        }
        circles.Clear();

        StartCoroutine(SpawnCircles());
    }

    IEnumerator SpawnCircles()
    {
        int numCircles = Random.Range(5, 11);
        for (int i = 0; i < numCircles; i++)
        {
            float xPos = Random.Range(0 + spawnAreaPadding, Screen.width - spawnAreaPadding);
            float yPos = Random.Range(0 + spawnAreaPadding, Screen.height - spawnAreaPadding);
            Vector3 spawnPosition = _cam.ScreenToWorldPoint(new Vector3(xPos, yPos, 10.0f));
            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity, circleParent.transform);
            circles.Add(circle);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void CheckForIntersection()
    {
        Vector3[] linePositions = new Vector3[_currentLine._renderer.positionCount];
        _currentLine._renderer.GetPositions(linePositions);

        foreach (GameObject circle in circles)
        {
            CircleCollider2D circleCollider = circle.GetComponent<CircleCollider2D>();

            for (int i = 0; i < linePositions.Length - 1; i++)
            {
                if (circleCollider.OverlapPoint(linePositions[i]))
                {
                    circle.SetActive(false);
                    break;
                }
            }
        }
    }
}
