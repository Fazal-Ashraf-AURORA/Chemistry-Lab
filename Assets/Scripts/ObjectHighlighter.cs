using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHighlighter : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            var outlineComponent = highlight.GetComponent<Outline>();
            if (outlineComponent != null)
            {
                outlineComponent.enabled = false;
            }
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) // Ensure EventSystem exists
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                Outline outlineComponent = highlight.GetComponent<Outline>();
                if (outlineComponent == null)
                {
                    outlineComponent = highlight.gameObject.AddComponent<Outline>();
                }

                outlineComponent.OutlineColor = Color.magenta;
                outlineComponent.OutlineWidth = 7.0f;
                outlineComponent.enabled = true;
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            if (highlight)
            {
                if (selection != null)
                {
                    var selectionOutline = selection.GetComponent<Outline>();
                    if (selectionOutline != null)
                    {
                        selectionOutline.enabled = false;
                    }
                }

                selection = raycastHit.transform;
                var newSelectionOutline = selection.GetComponent<Outline>();
                if (newSelectionOutline != null)
                {
                    newSelectionOutline.enabled = true;
                }

                highlight = null;
            }
            else
            {
                if (selection)
                {
                    var selectionOutline = selection.GetComponent<Outline>();
                    if (selectionOutline != null)
                    {
                        selectionOutline.enabled = false;
                    }
                    selection = null;
                }
            }
        }
    }
}
