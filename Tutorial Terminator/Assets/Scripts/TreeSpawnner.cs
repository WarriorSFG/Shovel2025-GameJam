using UnityEngine;

public class TreeSpawnner : MonoBehaviour
{
    public Transform player;
    public GameObject treePrefab;
    public Transform spawnPoint;
    public int maxCount = 10;
    private int current = 0;
    public bool isTree;
    private float lastTreeX = 0f;
    public float distanceBetweenTrees = 5f;
    public float Yoffset=0;

    private GameManagerL2 gameManager;

    void Start()
    {
        lastTreeX = spawnPoint.position.x;
        gameManager = GetComponent<GameManagerL2>();
        //SpawnTree();
    }

    void Update()
    {
        if (current == 0 || player.position.x > lastTreeX - distanceBetweenTrees && current < maxCount)
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
        if (isTree)
        {
            Transform trigger = newTree.transform.Find("Trigger");
            Transform JPoint = newTree.transform.Find("JPoint");
            GameObject Boundary = newTree.transform.Find("Boundary").gameObject;
            if (trigger != null)
            {
                gameManager.Point.Add(trigger);
                gameManager.JPoint.Add(JPoint);
                gameManager.hasCrossed.Add(false);
                gameManager.hasJumped.Add(false);
                gameManager.Boundaries.Add(Boundary);
            }
            else
            {
                Debug.LogWarning("Trigger not found in spawned tree!");
            }
        }

    }
}
