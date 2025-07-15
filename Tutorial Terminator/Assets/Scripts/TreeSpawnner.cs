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

    void Start()
    {
        lastTreeX = spawnPoint.position.x;
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
        Vector3 treePos = new Vector3(lastTreeX + distanceBetweenTrees, spawnPoint.position.y, 0);
        Instantiate(treePrefab, treePos, Quaternion.identity);
        lastTreeX += distanceBetweenTrees;
        current++;
    }
}
