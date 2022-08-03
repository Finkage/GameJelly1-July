using UnityEngine;

public class FlagManagement : MonoBehaviour
{
    public GameObject flag;
    public GameObject[] flags;
    public Transform parent;
    public int maxFlags = 5;

    private int totalFlags = 0;

    private void Start()
    {
        flags = new GameObject[maxFlags];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DropFlag();
    }

    private void DropFlag()
    {
        int flagIndex = totalFlags % maxFlags;

        if (flags[flagIndex] != null)
            Destroy(flags[flagIndex]);

        flags[flagIndex] = Instantiate(flag, new Vector3(transform.position.x, flag.transform.position.y, transform.position.z), flag.transform.rotation, parent.transform);

        totalFlags++;
    }
}
