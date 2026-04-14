using UnityEngine;

public class NotesPanelController : MonoBehaviour
{
    public RectTransform pageA;
    public RectTransform pageB;

    private bool showingPageA = true;
    private bool isActive = false;

    public void SetActive(bool active)
    {
        isActive = active;
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            FlipPage();
        }
    }

    void FlipPage()
    {
        showingPageA = !showingPageA;

        if (showingPageA)
        {
            pageA.SetAsLastSibling(); // Set in front
        }
        else
        {
            pageB.SetAsLastSibling();
        }
    }
}