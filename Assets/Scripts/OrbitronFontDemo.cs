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
        // Load Orbitron fonts from Resources (they should be in Assets/Fonts)
        Font[] orbitronFonts = Resources.FindObjectsOfTypeAll<Font>();
        
        fontWeights = new FontWeightDemo[]
        {
            new FontWeightDemo 
            { 
                weightName = "Regular", 
                sampleText = "ORBITRON REGULAR\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = FindFontByName(orbitronFonts, "Orbitron-Regular")
            },
            new FontWeightDemo 
            { 
                weightName = "Medium", 
                sampleText = "ORBITRON MEDIUM\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = FindFontByName(orbitronFonts, "Orbitron-Medium")
            },
            new FontWeightDemo 
            { 
                weightName = "SemiBold", 
                sampleText = "ORBITRON SEMIBOLD\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = FindFontByName(orbitronFonts, "Orbitron-SemiBold")
            },
            new FontWeightDemo 
            { 
                weightName = "Bold", 
                sampleText = "ORBITRON BOLD\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = FindFontByName(orbitronFonts, "Orbitron-Bold")
            },
            new FontWeightDemo 
            { 
                weightName = "ExtraBold", 
                sampleText = "ORBITRON EXTRABOLD\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = FindFontByName(orbitronFonts, "Orbitron-ExtraBold")
            },
            new FontWeightDemo 
            { 
                weightName = "Black", 
                sampleText = "ORBITRON BLACK\nThe quick brown fox jumps over the lazy dog.",
                orbitronFont = FindFontByName(orbitronFonts, "Orbitron-Black")
            }
        };
    }

    private Font FindFontByName(Font[] fonts, string name)
    {
        foreach (Font font in fonts)
        {
            if (font != null && font.name.Contains(name))
                return font;
        }
        return null;
    }

    private void CreateFontDisplays()
    {
        if (textContainer == null)
        {
            // Create a vertical layout group container
            GameObject container = new GameObject("FontContainer");
            container.transform.SetParent(transform);
            
            RectTransform containerRect = container.AddComponent<RectTransform>();
            containerRect.anchorMin = Vector2.zero;
            containerRect.anchorMax = Vector2.one;
            containerRect.offsetMin = new Vector2(50, 50);
            containerRect.offsetMax = new Vector2(-50, -50);
            
            VerticalLayoutGroup vlg = container.AddComponent<VerticalLayoutGroup>();
            vlg.spacing = 30f;
            vlg.childAlignment = TextAnchor.UpperCenter;
            vlg.childControlHeight = false;
            vlg.childControlWidth = true;
            vlg.childForceExpandHeight = false;
            vlg.childForceExpandWidth = true;
            
            ContentSizeFitter csf = container.AddComponent<ContentSizeFitter>();
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            
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
        rectTransform.sizeDelta = new Vector2(0, 80);

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
        layoutElement.preferredHeight = 80;
        layoutElement.flexibleHeight = 0;

        Debug.Log($"Created font display for {fontWeight.weightName}");
    }

    // Method to change font size (useful for mobile accessibility)
    public void ChangeFontSize(float multiplier)
    {
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
            
            int currentIndex = System.Array.IndexOf(colors, mainCamera.backgroundColor);
            int nextIndex = (currentIndex + 1) % colors.Length;
            mainCamera.backgroundColor = colors[nextIndex];
        }
    }
}