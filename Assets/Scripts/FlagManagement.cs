using UnityEngine;
using UnityEngine.UI;

public class FlagManagement : MonoBehaviour
{
    public GameObject flag;
    public GameObject[] flags;
    public Color[] flagColors;
    public Transform parent;
    public Image flagColorUI;
    public int maxFlags = 5;

    private int currentFlagIndex;
    private Color currentFlagColor;
    private int totalFlags = 0;

    private void Start()
    {
        flags = new GameObject[maxFlags];
        currentFlagIndex = 0;
        currentFlagColor = flagColors[currentFlagIndex];
        flagColorUI.color = new Color(currentFlagColor.r, currentFlagColor.g, currentFlagColor.b, 1);
    }

    private void Update()
    {
        if (GameController.gameIsPaused)
            return;

        // Cycle next flag color
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentFlagIndex = currentFlagIndex >= flagColors.Length - 1 ? 0 : currentFlagIndex + 1;
            SetFlagColor(currentFlagIndex);
        }

        // Cycle previous flag color
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentFlagIndex = currentFlagIndex <= 0 ? flagColors.Length - 1 : currentFlagIndex - 1;
            SetFlagColor(currentFlagIndex);
        }

        // Drop a flag at player location
        if (Input.GetKeyDown(KeyCode.Space))
            DropFlag();
    }

    public Color GetFlagColor()
    {
        return currentFlagColor;
    }

    public void SetFlagColor(int index)
    {
        currentFlagColor = flagColors[index];
        flagColorUI.color = new Color(currentFlagColor.r, currentFlagColor.g, currentFlagColor.b, 1);
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
