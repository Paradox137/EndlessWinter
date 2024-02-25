using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class NovelEditorWindow : EditorWindow
    {
        private VisualElement _root;
        private StyleSheet _styleSheet;

        private void OnEnable()
        {
            _root = rootVisualElement;
            _styleSheet = (StyleSheet) EditorGUIUtility.Load("NovelEditorWindow.uss");
            _root.styleSheets.Add(_styleSheet);
        }
        
        [MenuItem("Tools/Novel Editor Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<NovelEditorWindow>();
            window.titleContent = new GUIContent("Novel Editor");
            window.minSize = new Vector2(1200, 700);
        }

        public void CreateGUI()
        {
            CreateListChapters();
        }

        private void CreateListChapters()
        {
            Box chaptersLabel = new Box();
            chaptersLabel.AddToClassList("chapters-box");
            
            const int itemCount = 30;
            List<string> items = new List<string>();
            
            for (int i = 0; i <= itemCount; i++)
                items.Add(i.ToString());
            
            
            Func<VisualElement> makeItem = () => new Label();
            Action<VisualElement, int> bindItem = (e, i) =>
            {
                ((Label)e).text = "Chapter " + items[i];
                ((Label)e).AddToClassList("chapters-items");
            };
            
            ListView listView = new ListView
            {
                makeItem = makeItem,
                bindItem = bindItem,
                itemsSource = items,
                selectionType = SelectionType.Single
            };
            listView.itemsChosen += Debug.Log;
            listView.selectionChanged += Debug.Log;
            
            
            chaptersLabel.Add(listView);
            listView.AddToClassList("chapters-listview");
            listView.hierarchy.Children().FirstOrDefault()?.AddToClassList("chapters-scrollview");
            _root.Add(chaptersLabel);
            //_root.Add(listView);
        }
    }
}