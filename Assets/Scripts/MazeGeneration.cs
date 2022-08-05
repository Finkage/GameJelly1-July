using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    public GameObject diePrefab;
    public float dieSize = 4;   // Number of units die needs to move to be adjacent to neighboring dice
    public int mazeSize = 5;
    public int wallProbability = 80; // Probability that a grid square becomes a wall when randomizing maze
    public int seed = 69;
    public bool useSeed = false;
    public bool fullGrid = false;
    public int numOfCheckpoints = 5;    // Number of checkpoints a viable path must pass through

    private float mazeOffset;     // Centers maze to table
    private int[,] maze;
    private (int, int)[] viablePath;

    private void Start()
    {
        mazeOffset = dieSize * mazeSize / 2 - dieSize / 2;
        maze = new int[mazeSize, mazeSize];
        viablePath = new (int, int)[numOfCheckpoints];
    }

    private void Update()
    {
        // Generate new maze upon pressing X (for testing only)
        if (Input.GetKeyDown(KeyCode.X))
            CreateNewMaze();
    }

    private void CreateNewMaze()
    {
        ClearGrid();
        SetInitialGrid();
        GenerateViablePath();
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        for (int row = 0; row < mazeSize; row++)
        {
            for (int col = 0; col < mazeSize; col++)
            {
                if (maze[row, col] == 0)
                    continue;

                Vector3 pos = new Vector3(diePrefab.transform.position.x + col * dieSize, diePrefab.transform.position.y, diePrefab.transform.position.z - row * dieSize);
                Instantiate(diePrefab, pos, diePrefab.transform.rotation, transform);
            }
        }

        transform.position = new Vector3(transform.position.x - mazeOffset, transform.position.y, transform.position.z + mazeOffset);
    }

    private void SetInitialGrid()
    {
        if (useSeed)
            Random.InitState(seed);

        for (int row = 0; row < mazeSize; row++)
        {
            for (int col = 0; col < mazeSize; col++)
            {
                if (fullGrid)
                    maze[row, col] = 1;
                else
                    maze[row, col] = Random.Range(1, 101) < wallProbability ? 1 : 0;
            }
        }
    }

    private void ClearGrid()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void GenerateViablePath()
    {
        // Generate tuples for viable path
        for (int i = 0; i < viablePath.Length; i++)
        {
            // Item1 corresponds with row, Item2 corresponds with column
            viablePath[i] = (Random.Range(0, mazeSize), Random.Range(0, mazeSize));

            // Checkpoints should be a path
            maze[viablePath[i].Item1, viablePath[i].Item2] = 0;
        }

        // Set maze array
        for (int i = 0; i < viablePath.Length - 1; i++)
        {
            int row = viablePath[i].Item1;
            int targetRow = viablePath[i + 1].Item1;
            int col = viablePath[i].Item2;
            int targetCol = viablePath[i + 1].Item2;

            while (row != targetRow)
            {
                row = row < targetRow ? row + 1 : row - 1;
                maze[row, col] = 0;
            }

            while (col != targetCol)
            {
                col = col < targetCol ? col + 1 : col - 1;
                maze[row, col] = 0;
            }
        }
    }
}
