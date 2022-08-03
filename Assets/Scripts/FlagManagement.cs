using UnityEngine;

public class FlagManagement : MonoBehaviour
{
    public GameObject flag;
    public GameObject[] flags;
    public Color[] flagColors;
    public Transform parent;
    public int maxFlags = 5;

    private Color currentFlagColor;
    private int totalFlags = 0;
    private KeyCode[] numKeys = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0
    };

    private void Start()
    {
        flags = new GameObject[maxFlags];
        currentFlagColor = flagColors[0];
    }

    private void Update()
    {
        for (int i = 0; i < flagColors.Length; i++)
        {
            // Set the flag color
            if (Input.GetKeyDown(numKeys[i]))
                currentFlagColor = flagColors[i];
        }

        if (Input.GetKeyDown(KeyCode.Space))
            DropFlag();
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
