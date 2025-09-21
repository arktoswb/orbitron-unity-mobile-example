using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls mobile-specific UI interactions and screen adaptation
/// </summary>
public class MobileUIController : MonoBehaviour
{
    [Header("Mobile UI Settings")]
    public Button increaseFontButton;
    public Button decreaseFontButton;
    public Button changeBackgroundButton;
    public ScrollRect scrollRect;
    
    [Header("Font Size Settings")]
    public float fontSizeMultiplier = 1.0f;
    public float fontSizeStep = 0.2f;
    public float minFontSize = 0.5f;
    public float maxFontSize = 2.0f;
    
    private OrbitronFontDemo fontDemo;
    
    void Start()
    {
        fontDemo = FindObjectOfType<OrbitronFontDemo>();
        SetupMobileUI();
        AdaptToScreenOrientation();
    }
    
    void SetupMobileUI()
    {
        // Create UI buttons if they don't exist
        if (increaseFontButton == null || decreaseFontButton == null || changeBackgroundButton == null)
        {
            CreateMobileUIButtons();
        }
        
        // Set up button listeners
        if (increaseFontButton != null)
            increaseFontButton.onClick.AddListener(() => AdjustFontSize(fontSizeStep));
            
        if (decreaseFontButton != null)
            decreaseFontButton.onClick.AddListener(() => AdjustFontSize(-fontSizeStep));
            
        if (changeBackgroundButton != null)
            changeBackgroundButton.onClick.AddListener(ChangeBackground);
    }
    
    void CreateMobileUIButtons()
    {
        // Find the Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null) return;
        
        // Create button container
        GameObject buttonContainer = new GameObject("MobileControls");
        buttonContainer.transform.SetParent(canvas.transform, false);
        
        RectTransform containerRect = buttonContainer.AddComponent<RectTransform>();
        containerRect.anchorMin = new Vector2(0, 0);
        containerRect.anchorMax = new Vector2(1, 0);
        containerRect.anchoredPosition = new Vector2(0, 100);
        containerRect.sizeDelta = new Vector2(0, 80);
        
        // Add horizontal layout group
        HorizontalLayoutGroup hlg = buttonContainer.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing = 20f;
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.childControlWidth = false;
        hlg.childControlHeight = true;
        hlg.childForceExpandWidth = false;
        hlg.childForceExpandHeight = true;
        
        // Create buttons
        increaseFontButton = CreateButton(buttonContainer.transform, "Font +", new Vector2(120, 60));
        decreaseFontButton = CreateButton(buttonContainer.transform, "Font -", new Vector2(120, 60));
        changeBackgroundButton = CreateButton(buttonContainer.transform, "Background", new Vector2(140, 60));
    }
    
    Button CreateButton(Transform parent, string text, Vector2 size)
    {
        GameObject buttonObj = new GameObject(text + "Button");
        buttonObj.transform.SetParent(parent, false);
        
        RectTransform rect = buttonObj.AddComponent<RectTransform>();
        rect.sizeDelta = size;
        
        Image image = buttonObj.AddComponent<Image>();
        image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        
        Button button = buttonObj.AddComponent<Button>();
        
        // Add text child
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);
        
        RectTransform textRect = textObj.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;
        
        Text textComponent = textObj.AddComponent<Text>();
        textComponent.text = text;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComponent.fontSize = 16;
        textComponent.color = Color.white;
        textComponent.alignment = TextAnchor.MiddleCenter;
        
        return button;
    }
    
    void AdjustFontSize(float delta)
    {
        fontSizeMultiplier = Mathf.Clamp(fontSizeMultiplier + delta, minFontSize, maxFontSize);
        
        if (fontDemo != null)
        {
            fontDemo.ChangeFontSize(fontSizeMultiplier);
        }
        
        Debug.Log($"Font size multiplier: {fontSizeMultiplier}");
    }
    
    void ChangeBackground()
    {
        if (fontDemo != null)
        {
            fontDemo.CycleBackgroundColor();
        }
    }
    
    void AdaptToScreenOrientation()
    {
        // Adjust UI layout based on screen orientation
        bool isLandscape = Screen.width > Screen.height;
        
        if (scrollRect != null)
        {
            // Adjust scroll sensitivity for mobile
            scrollRect.scrollSensitivity = isLandscape ? 30f : 20f;
        }
        
        // You can add more orientation-specific adaptations here
    }
    
    void Update()
    {
        // Handle mobile input
        HandleMobileInput();
    }
    
    void HandleMobileInput()
    {
        // Handle pinch-to-zoom for font size on mobile
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;
            
            float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
            float touchDeltaMag = (touch1.position - touch2.position).magnitude;
            
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            
            if (Mathf.Abs(deltaMagnitudeDiff) > 5f) // Threshold to avoid jitter
            {
                float pinchAmount = deltaMagnitudeDiff * 0.001f;
                AdjustFontSize(-pinchAmount);
            }
        }
        
        // Handle back button on Android
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // On mobile, this usually means back button was pressed
            Application.Quit();
        }
    }
}