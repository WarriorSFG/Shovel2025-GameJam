using Unity.VisualScripting;
using UnityEngine;

public class TreeSpawnner : MonoBehaviour
{
    public Transform player;
    public GameObject treePrefab;
    public Transform spawnPoint;
    public int maxCount = 10;
    private int current = 0;

    private float lastTreeX = 0f;
    public float distanceBetweenTrees = 5f;
    public float Yoffset=0;

    private GameManagerL2 gameManager;

    void Start()
    {
        lastTreeX = spawnPoint.position.x;
        gameManager = GetComponent<GameManagerL2>();
        SpawnTree();
    }

    void Update()
    {
        if (player.position.x > lastTreeX - distanceBetweenTrees && current < maxCount)
        {
            SpawnTree();
        }
    }

    public void SpawnTree()
    {
        Vector3 treePos = new Vector3(lastTreeX + distanceBetweenTrees, spawnPoint.position.y + Yoffset, 0);
        GameObject newTree = Instantiate(treePrefab, treePos, Quaternion.identity);
        lastTreeX += distanceBetweenTrees;
        current++; 
        gameManager.hasCrossed.Add(false);

        // Get child triggers by name
        Transform trigger = newTree.transform.Find("Trigger");
        Transform boundary = newTree.transform.Find("Boundary");

        if (trigger != null && boundary != null && gameManager != null)
        {
            gameManager.Point.Add(trigger);
            gameManager.Boundaries.Add(boundary.gameObject);
            gameManager.hasCrossed.Add(false); // make sure hasCrossed is public in GameManagerL2
        }
        else
        {
            Debug.LogWarning("Missing 'Trigger' or 'Boundary' child in treePrefab.");
        }
    }
}
