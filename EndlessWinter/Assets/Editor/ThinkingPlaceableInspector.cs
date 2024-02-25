using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class NovelEditorWindow2 : EditorWindow
    {
        public static void ShowWindow()
        {
            var window = GetWindow<NovelEditorWindow2>();
            window.titleContent = new GUIContent("Novel Editor");
            window.minSize = new Vector2(800, 600);
        }
        public void CreateGUI()
        {
            VisualElement container = new VisualElement();
            // Action to perform when button is pressed.
            // Toggles the text on all buttons in 'container'.
            Action action = () =>
            {
                container.Query<Button>().ForEach((button) =>
                {
                    button.text = button.text.EndsWith("Button") ? "Button (Clicked)" : "Button";
                });
            };

            // Get a reference to the Button from UXML and assign it its action.
           /* var uxmlButton = container.Q<Button>("the-uxml-button");
            uxmlButton.RegisterCallback<MouseUpEvent>((evt) => action());*/

            // Create a new Button with an action and give it a style class.
            var csharpButton = new Button(action) { text = "C# Button" };
            csharpButton.AddToClassList("some-styled-button");
            container.Add(csharpButton);
        }
    }
}