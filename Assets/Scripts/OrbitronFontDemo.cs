using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Demonstrates different Orbitron font weights in a mobile-friendly interface
/// </summary>
public class OrbitronFontDemo : MonoBehaviour
{
    [System.Serializable]
    public class FontWeightDemo
    {
        public string weightName;
        public Font orbitronFont;
        public string sampleText;
    }

    [Header("Font Demonstration Settings")]
    public FontWeightDemo[] fontWeights;
    
    [Header("UI Components")]
    public Transform textContainer;
    public GameObject textPrefab;
    
    [Header("Layout Settings")]
    public float textSpacing = 100f;
    public int baseFontSize = 24;
    public Color textColor = Color.white;

    private void Start()
    {
        SetupFontDemo();
    }

    private void SetupFontDemo()
    {
        if (fontWeights == null || fontWeights.Length == 0)
        {
            // If no fonts are configured, create default setup
            SetupDefaultFonts();
        }

        CreateFontDisplays();
    }

    private void SetupDefaultFonts()
    {
        // Load Orbitron fonts from Resources/Fonts folder
        fontWeights = new FontWeightDemo[]
        {
            new FontWeightDemo 
            { 
                weightName = "Regular", 
                sampleText = "ORBITRON REGULAR\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = Resources.Load<Font>("Fonts/Orbitron-Regular")
            },
            new FontWeightDemo 
            { 
                weightName = "Medium", 
                sampleText = "ORBITRON MEDIUM\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = Resources.Load<Font>("Fonts/Orbitron-Medium")
            },
            new FontWeightDemo 
            { 
                weightName = "SemiBold", 
                sampleText = "ORBITRON SEMIBOLD\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = Resources.Load<Font>("Fonts/Orbitron-SemiBold")
            },
            new FontWeightDemo 
            { 
                weightName = "Bold", 
                sampleText = "ORBITRON BOLD\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = Resources.Load<Font>("Fonts/Orbitron-Bold")
            },
            new FontWeightDemo 
            { 
                weightName = "ExtraBold", 
                sampleText = "ORBITRON EXTRABOLD\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = Resources.Load<Font>("Fonts/Orbitron-ExtraBold")
            },
            new FontWeightDemo 
            { 
                weightName = "Black", 
                sampleText = "ORBITRON BLACK\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = Resources.Load<Font>("Fonts/Orbitron-Black")
            }
        };

        // Debug log to verify fonts loaded
        foreach (var fw in fontWeights)
        {
            if (fw.orbitronFont == null)
            {
                Debug.LogError($"Failed to load font: {fw.weightName}. Make sure fonts are in Assets/Resources/Fonts/");
            }
            else
            {
                Debug.Log($"Successfully loaded font: {fw.weightName}");
            }
        }
    }

    private void CreateFontDisplays()
    {
        if (textContainer == null)
        {
            // Create a scroll view container for mobile
            GameObject scrollViewObj = new GameObject("ScrollView");
            scrollViewObj.transform.SetParent(transform);
            
            RectTransform scrollRect = scrollViewObj.AddComponent<RectTransform>();
            scrollRect.anchorMin = Vector2.zero;
            scrollRect.anchorMax = Vector2.one;
            scrollRect.offsetMin = new Vector2(20, 20);
            scrollRect.offsetMax = new Vector2(-20, -20);

            ScrollRect scroll = scrollViewObj.AddComponent<ScrollRect>();
            scroll.horizontal = false;
            scroll.vertical = true;
            scroll.movementType = ScrollRect.MovementType.Elastic;

            // Create content container
            GameObject container = new GameObject("FontContainer");
            container.transform.SetParent(scrollViewObj.transform);
            
            RectTransform containerRect = container.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0, 1);
            containerRect.anchorMax = new Vector2(1, 1);
            containerRect.pivot = new Vector2(0.5f, 1);
            containerRect.sizeDelta = new Vector2(0, 0);
            
            VerticalLayoutGroup vlg = container.AddComponent<VerticalLayoutGroup>();
            vlg.spacing = 40f;
            vlg.childAlignment = TextAnchor.UpperCenter;
            vlg.childControlHeight = false;
            vlg.childControlWidth = true;
            vlg.childForceExpandHeight = false;
            vlg.childForceExpandWidth = true;
            vlg.padding = new RectOffset(20, 20, 20, 20);
            
            ContentSizeFitter csf = container.AddComponent<ContentSizeFitter>();
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            
            scroll.content = containerRect;
            textContainer = container.transform;
        }

        for (int i = 0; i < fontWeights.Length; i++)
        {
            CreateFontDisplay(fontWeights[i], i);
        }
    }

    private void CreateFontDisplay(FontWeightDemo fontWeight, int index)
    {
        if (fontWeight.orbitronFont == null)
        {
            Debug.LogWarning($"Font not found for {fontWeight.weightName}");
            return;
        }

        // Create text gameobject
        GameObject textObject = new GameObject($"Text_{fontWeight.weightName}");
        textObject.transform.SetParent(textContainer);
        
        RectTransform rectTransform = textObject.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(0.5f, 1);
        rectTransform.sizeDelta = new Vector2(0, 120);

        // Add Text component
        Text textComponent = textObject.AddComponent<Text>();
        textComponent.font = fontWeight.orbitronFont;
        textComponent.text = fontWeight.sampleText;
        textComponent.fontSize = baseFontSize;
        textComponent.color = textColor;
        textComponent.alignment = TextAnchor.MiddleCenter;
        textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
        textComponent.verticalOverflow = VerticalWrapMode.Overflow;

        // Add layout element for proper sizing
        LayoutElement layoutElement = textObject.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 120;
        layoutElement.flexibleHeight = 0;

        Debug.Log($"Created font display for {fontWeight.weightName}");
    }

    // Method to change font size (useful for mobile accessibility)
    public void ChangeFontSize(float multiplier)
    {
        if (textContainer == null) return;
        
        Text[] allTexts = textContainer.GetComponentsInChildren<Text>();
        foreach (Text text in allTexts)
        {
            text.fontSize = Mathf.RoundToInt(baseFontSize * multiplier);
        }
    }

    // Method to cycle through different background colors
    public void CycleBackgroundColor()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Color[] colors = { 
                new Color(0.1f, 0.1f, 0.1f), // Dark gray
                new Color(0.2f, 0.3f, 0.5f), // Blue
                new Color(0.3f, 0.2f, 0.4f), // Purple
                new Color(0.1f, 0.2f, 0.1f)  // Dark green
            };
            
            // Find closest matching color
            int currentIndex = 0;
            float minDistance = float.MaxValue;
            for (int i = 0; i < colors.Length; i++)
            {
                float distance = Vector3.Distance(
                    new Vector3(mainCamera.backgroundColor.r, mainCamera.backgroundColor.g, mainCamera.backgroundColor.b),
                    new Vector3(colors[i].r, colors[i].g, colors[i].b)
                );
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentIndex = i;
                }
            }
            
            int nextIndex = (currentIndex + 1) % colors.Length;
            mainCamera.backgroundColor = colors[nextIndex];
        }
    }
}