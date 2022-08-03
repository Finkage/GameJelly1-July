using UnityEngine;

public class FlagManagement : MonoBehaviour
{
    public GameObject flag;
    public GameObject[] flags;
    public Color[] flagColors;
    public Transform parent;
    public int maxFlags = 5;

    private int currentFlagIndex;
    private Color currentFlagColor;
    private int totalFlags = 0;

    private void Start()
    {
        flags = new GameObject[maxFlags];
        currentFlagIndex = 0;
        currentFlagColor = flagColors[0];
    }

    private void Update()
    {
        // Cycle next flag color
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentFlagIndex = currentFlagIndex >= flagColors.Length - 1 ? 0 : currentFlagIndex + 1;
            currentFlagColor = flagColors[currentFlagIndex];
        }

        // Cycle previous flag color
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentFlagIndex = currentFlagIndex <= 0 ? flagColors.Length - 1 : currentFlagIndex - 1;
            currentFlagColor = flagColors[currentFlagIndex];
        }

        if (Input.GetKeyDown(KeyCode.Space))
            DropFlag();
    }

    public Color GetFlagColor()
    {
        return currentFlagColor;
    }

    private void DropFlag()
    {
        int flagIndex = totalFlags % maxFlags;

        if (flags[flagIndex] != null)
            Destroy(flags[flagIndex]);

        flags[flagIndex] = Instantiate(flag, new Vector3(transform.position.x, flag.transform.position.y, transform.position.z), flag.transform.rotation, parent.transform);
        flags[flagIndex].GetComponent<Renderer>().material.color = currentFlagColor;

        totalFlags++;
    }
}
