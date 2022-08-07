using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    public GameController gameController;
    public GameObject diePrefab;
    public GameObject barrierPrefab;
    public GameObject cheesePrefab;
    public GameObject startingFlagPrefab;
    public GameObject player;
    public float dieSize = 4;   // Number of units die needs to move to be adjacent to neighboring dice
    public int mazeSize = 5;
    public int wallProbability = 80; // Probability that a grid square becomes a wall when randomizing maze
    public int seed = 69;
    public bool useSeed = false;
    public bool fullGrid = false;
    public int numOfCheckpoints = 5;    // Number of checkpoints a viable path must pass through
    public int numOfStrays = 5;   // Number of checkpoints that lead player to stray off main path

    private float mazeOffset;     // Centers maze to table
    private int[,] maze;
    private (int, int)[] viablePath;
    private (int, int)[] strayPaths;
    private (int, int) startingPosition;
    private (int, int) goalPosition;
    private GameObject startPosMarker;

    private void Start()
    {
        CreateNewMaze();
    }

    private void Update()
    {
        // Generate new maze upon pressing X (for testing only)
        if (gameController.devMode && Input.GetKeyDown(KeyCode.X))
            CreateNewMaze();
    }

    private void CreateNewMaze()
    {
        ClearGrid();
        SetInitialGrid();
        GeneratePaths();
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        mazeOffset = dieSize * mazeSize / 2 - dieSize / 2;

        // Create maze
        for (int row = 0; row < mazeSize; row++)
        {
            for (int col = 0; col < mazeSize; col++)
            {
                if (maze[row, col] == 0)
                    continue;

                Vector3 pos = new Vector3(diePrefab.transform.position.x + col * dieSize, diePrefab.transform.position.y, diePrefab.transform.position.z - row * dieSize);
                Quaternion rot = Quaternion.Euler(diePrefab.transform.rotation.x + RandomRotation(), 0, diePrefab.transform.rotation.z + RandomRotation());
                Instantiate(diePrefab, pos, rot, transform);
            }
        }

        // Create barrier along perimeter of maze
        for (int col = 0; col < mazeSize + 2; col++)
        {
            // Instantiate top barrier
            Vector3 pos = new Vector3(barrierPrefab.transform.position.x + col * dieSize - dieSize, barrierPrefab.transform.position.y, barrierPrefab.transform.position.z + dieSize);
            Quaternion rot = Quaternion.Euler(barrierPrefab.transform.rotation.x + RandomRotation(), 0, barrierPrefab.transform.rotation.z + RandomRotation());
            Instantiate(barrierPrefab, pos, rot, transform);

            rot = Quaternion.Euler(barrierPrefab.transform.rotation.x + RandomRotation(), 0, barrierPrefab.transform.rotation.z + RandomRotation());

            // Instantiate bottom barrier
            pos = new Vector3(barrierPrefab.transform.position.x + col * dieSize - dieSize, barrierPrefab.transform.position.y, barrierPrefab.transform.position.z - mazeSize * dieSize);
            Instantiate(barrierPrefab, pos, rot, transform);
        }

        for (int row = 0; row < mazeSize; row++)
        {
            // Instantiate left barrier
            Vector3 pos = new Vector3(barrierPrefab.transform.position.x - dieSize, barrierPrefab.transform.position.y, barrierPrefab.transform.position.z - row * dieSize);
            Quaternion rot = Quaternion.Euler(barrierPrefab.transform.rotation.x + RandomRotation(), 0, barrierPrefab.transform.rotation.z + RandomRotation());
            Instantiate(barrierPrefab, pos, rot, transform);

            rot = Quaternion.Euler(barrierPrefab.transform.rotation.x + RandomRotation(), 0, barrierPrefab.transform.rotation.z + RandomRotation());

            // Instantiate right barrier
            pos = new Vector3(barrierPrefab.transform.position.x + mazeSize * dieSize, barrierPrefab.transform.position.y, barrierPrefab.transform.position.z - row * dieSize);
            Instantiate(barrierPrefab, pos, rot, transform);
        }

        // Spawn flag at starting position
        SpawnStartingFlag();

        // Spawn goal
        SpawnCheese();

        // Offset maze so it is centered on the table
        transform.position = new Vector3(transform.position.x - mazeOffset, transform.position.y, transform.position.z + mazeOffset);

        // Move player to location of starting flag
        player.transform.position = startPosMarker.transform.position;
    }

    private void SetInitialGrid()
    {
        maze = new int[mazeSize, mazeSize];

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

    private void GeneratePaths()
    {
        viablePath = new (int, int)[numOfCheckpoints];
        strayPaths = new (int, int)[numOfStrays];

        // Generate tuples for viable path
        for (int i = 0; i < viablePath.Length; i++)
        {
            // Item1 corresponds with row, Item2 corresponds with column
            viablePath[i] = (Random.Range(0, mazeSize), Random.Range(0, mazeSize));

            // Checkpoints should be a path
            maze[viablePath[i].Item1, viablePath[i].Item2] = 0;
        }

        // If goal and starting positions are the same, keep rerolling until different
        while (viablePath[0] == viablePath[viablePath.Length - 1])
            viablePath[0] = (Random.Range(0, mazeSize), Random.Range(0, mazeSize));

        // Set starting position as first tuple in viablePath and goal as last tuple
        startingPosition = viablePath[0];
        goalPosition = viablePath[viablePath.Length - 1];

        // Generate stray paths
        for (int i = 0; i < strayPaths.Length; i++)
        {
            strayPaths[i] = (Random.Range(0, mazeSize), Random.Range(0, mazeSize));
            maze[strayPaths[i].Item1, strayPaths[i].Item2] = 0;
        }

        // Set maze array
        for (int i = 0; i < viablePath.Length - 1; i++)
        {
            int row = viablePath[i].Item1;
            int col = viablePath[i].Item2;
            int targetRow = viablePath[i + 1].Item1;
            int targetCol = viablePath[i + 1].Item2;

            CreatePath(row, col, targetRow, targetCol);
        }

        // Set maze array for stray paths
        for (int i = 0; i < strayPaths.Length; i++)
        {
            int row = strayPaths[i].Item1;
            int col = strayPaths[i].Item2;

            (int, int) randomPath = viablePath[Random.Range(0, viablePath.Length)];

            int targetRow = randomPath.Item1;
            int targetCol = randomPath.Item2;

            CreatePath(row, col, targetRow, targetCol);

        }

        // Set stray paths for stray paths
        for (int i = 0; i < strayPaths.Length; i++)
        {
            int row = strayPaths[i].Item1;
            int col = strayPaths[i].Item2;

            int targetRow = Random.Range(0, mazeSize);
            int targetCol = Random.Range(0, mazeSize);

            CreatePath(row, col, targetRow, targetCol);
        }
    }

    private void CreatePath(int row, int col, int targetRow, int targetCol)
    {
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

    // Spawn cheese at goal position
    private void SpawnCheese()
    {
        Vector3 pos = new Vector3(cheesePrefab.transform.position.x + goalPosition.Item2 * dieSize, cheesePrefab.transform.position.y, cheesePrefab.transform.position.z - goalPosition.Item1 * dieSize);
        Instantiate(cheesePrefab, pos, cheesePrefab.transform.rotation, transform);
    }

    // Spawn flag at start position
    private void SpawnStartingFlag()
    {
        Vector3 pos = new Vector3(startingFlagPrefab.transform.position.x + startingPosition.Item2 * dieSize, startingFlagPrefab.transform.position.y, startingFlagPrefab.transform.position.z - startingPosition.Item1 * dieSize);
        startPosMarker = Instantiate(startingFlagPrefab, pos, startingFlagPrefab.transform.rotation, transform);
    }

    private int RandomRotation()
    {
        return 90 * Random.Range(-1, 3);
    }
}
