using UnityEngine;
using UnityEngine.UI;

public class UIStatusColoring : MonoBehaviour
{
    Image imgComponent;
    [SerializeField] float lerpTime = 1;
    Color theNewColor;

    private void OnEnable() => HUDController.UIColoration += UpdateColoring;
    private void OnDisable() => HUDController.UIColoration -= UpdateColoring;

    // Start is called before the first frame update
    void Start() => imgComponent = GetComponent<Image>();

    // Update is called once per frame
    void UpdateColoring(Color newColor) => theNewColor = newColor;


    private void Update()
    {
        if (imgComponent != null)
            if (theNewColor != imgComponent.color)
                imgComponent.color = Color.Lerp(imgComponent.color, theNewColor, lerpTime * Time.fixedDeltaTime);
    }
}
