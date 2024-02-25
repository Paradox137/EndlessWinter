using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class test : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/test")]
    public static void ShowExample()
    {
        test wnd = GetWindow<test>();
        wnd.titleContent = new GUIContent("test");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
        
        // Action to perform when button is pressed.
        // Toggles the text on all buttons in 'container'.
        Action action = () =>
        {
            root.Query<Button>().ForEach((button) =>
            {
                button.text = button.text.EndsWith("Button") ? "Button (Clicked)" : "Button";
                VisualElement label2 = new Label("Hello wwwww");
                root.Add(label2);
            });
        };
        
        Button csharpButton = new Button(action) { text = "C# Button" };
        csharpButton.AddToClassList("test-styled-button");
        root.Add(csharpButton);
    }
}
