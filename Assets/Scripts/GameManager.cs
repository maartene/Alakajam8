using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{

    public static int numberOfTriangles(int row)
    {
        return row * 2 + 1;
    }

    public float yScale = 0.9f;
    public float yOffset = 1.0f;
    public Triangle trianglePrefab;
    public Fish[] fishPrefabs;
    public Player playerPrefab;
    public int depth = 8;
    public DepthLight globalLight;
    public int minFishSpawnDepth = 5;

    List<Triangle> triangles = new List<Triangle>();
    Player player;
    public List<Fish> fishies = new List<Fish>();

    public bool CreateTriangle(int x, int y, int vision = 0)
    {
        if (y >= depth || y <= 0)
        {
            return false;
        }

        if (getTriangle(x, y) == null)
        {

            float xOffset = numberOfTriangles(y) * 0.25f;
            Triangle triangle = Instantiate(trianglePrefab, transform) as Triangle;
            if (player != null)
            {
                triangle.transform.position = player.transform.position;
            }
            StartCoroutine(triangle.MoveTo(new Vector3(x * 0.5f - xOffset, -y * yScale + yOffset, 0)));
            //triangle.transform.position = new Vector3(x * 0.5f - xOffset, -y * yScale + yOffset, 0);
            Vector3 scale;
            if (x % 2 == 0)
            {
                scale = Vector3.one;
            }
            else
            {
                scale = new Vector3(1, -1, 1);
            }
            triangle.transform.localScale = scale;
            triangle.x = x;
            triangle.y = y;
            triangle.gameObject.name = "triangle " + triangle.x + "," + triangle.y;
            triangles.Add(triangle);

            // small chance of spawning a fish.
            if (y > minFishSpawnDepth && Random.Range(0, 1.0f) > 0.95f)
            {               
                Fish fish = Instantiate(fishPrefabs[Random.Range(0, fishPrefabs.Length)], transform);
                fish.x = triangle.x;
                fish.y = triangle.y;
                fish.transform.position = triangle.targetPosition;
                StartCoroutine(fish.FadeIn(0.5f));
                fishies.Add(fish);
            }


        }

        if (vision > 0)
        {
            // Left
            CreateTriangle(x - 1, y, vision - 1);
            // Right
            CreateTriangle(x + 1, y, vision - 1);
            // Down
            CreateTriangle(x + 1, y + 1, vision - 1);
            // Up
            CreateTriangle(x - 1, y - 1, vision - 1);
        }

        return true;
    }

    public Triangle getTriangle(int x, int y)
    {
        if (triangles.Count == 0)
        {
            return null;
        }

        foreach (Triangle triangle in triangles)
        {
            if (triangle.x == x && triangle.y == y)
            {
                return triangle;
            }
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Create top triangle:
        float xOffset = numberOfTriangles(0) * 0.25f;
        Triangle triangle = Instantiate(trianglePrefab, transform) as Triangle;
        triangle.transform.position = new Vector3(0 * 0.5f - xOffset, -0 * yScale + yOffset, 0);
        triangle.x = 0;
        triangle.y = 0;
        triangle.gameObject.name = "triangle " + triangle.x + "," + triangle.y;
        triangles.Add(triangle);

        // Create a first triangle to go down.
        CreateTriangle(1, 1);

        player = Instantiate(playerPrefab, transform) as Player;
        player.x = 0;
        player.y = 0;
        player.transform.position = triangles[0].transform.position;

        ConnectTriangles();
    }

    void ConnectTriangles()
    {
        foreach (Triangle triangle in triangles)
        {
            triangle.rightTriangle = getTriangle(triangle.x + 1, triangle.y);
            triangle.leftTriangle = getTriangle(triangle.x - 1, triangle.y);

            if (triangle.x % 2 == 0)
            {
                triangle.downTriangle = getTriangle(triangle.x + 1, triangle.y + 1);
            } else
            {
                triangle.aboveTriangle = getTriangle(triangle.x - 1, triangle.y - 1);
            }
        }
    }

    public void Step()
    {

        foreach (Fish fish in fishies)
        {
            if (fish.direction == Fish.Direction.Right)
            {
                Triangle rightTriangle = getTriangle(fish.x + 1, fish.y);
                if (rightTriangle)
                {
                    fish.SetTargetLocation(rightTriangle);
                }
                else
                {
                    fish.SetDirection(Fish.Direction.Left);
                }
            } else
            {
                Triangle leftTriangle = getTriangle(fish.x - 1, fish.y);
                if (leftTriangle)
                {
                    fish.SetTargetLocation(leftTriangle);
                }
                else
                {
                    fish.SetDirection(Fish.Direction.Right);
                }
            }                      
        }

        globalLight.targetIntensity = 0.5f - (player.y * 0.05f);
    }
}
