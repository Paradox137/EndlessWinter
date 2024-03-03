using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameModule.DataModule;
using ModestTree;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
	public class NovelEditorWindow : EditorWindow
	{
		private List<string>[] textLists = new List<string>[4];
		private string[] newTexts = new string[4];

		private Vector2 scrollPosition = Vector2.zero;

		private VisualElement _root;
		private StyleSheet _styleSheet;

		private ListView _chaptersListView;


		private VisualElement _chapterParametersBox;
		private TreeView _charactersTreeView;

		private int _currentChapter;
		private ActorType _currentCharacter;

		private Actor _rootActor;
		private void OnEnable()
		{
			_root = rootVisualElement;
			_styleSheet = (StyleSheet)EditorGUIUtility.Load("NovelEditorWindow.uss");
			_root.styleSheets.Add(_styleSheet);
		}

		[MenuItem("Tools/Novel Editor Window")]
		public static void ShowWindow()
		{
			var window = GetWindow<NovelEditorWindow>();
			window.titleContent = new GUIContent("Novel Editor");
			window.minSize = new Vector2(1200, 700);
		}

		private void OnGUI()
		{
			CreateTextLabels();
		}
		
		public void CreateGUI()
		{
			CreateListChapters();
			CreateCharacters();
		}

		private void CreateListChapters()
		{
			Box chaptersLabel = new Box();
			chaptersLabel.AddToClassList("chapters-box");

			const int itemCount = 30;
			List<string> items = new List<string>();

			for (int i = 1; i <= itemCount; i++)
				items.Add(i.ToString());


			Func<VisualElement> makeItem = () => new Label();
			Action<VisualElement, int> bindItem = (e, i) =>
			{
				((Label)e).text = "Chapter " + items[i];
				((Label)e).AddToClassList("chapters-items");
			};

			_chaptersListView = new ListView
			{
				makeItem = makeItem,
				bindItem = bindItem,
				itemsSource = items,
				selectionType = SelectionType.Single
			};

			_chaptersListView.itemsChosen += OnChaptersClick;
			_chaptersListView.selectionChanged += OnChaptersClick;

			chaptersLabel.Add(_chaptersListView);
			_chaptersListView.AddToClassList("chapters-listview");
			_chaptersListView.hierarchy.Children().FirstOrDefault()?.AddToClassList("chapters-scrollview");
			_root.Add(chaptersLabel);
		}
		
		private void CreateCharacters()
		{
			_chapterParametersBox = new Box();
			_chapterParametersBox.AddToClassList("chapter-parameters-box");

			var charactersItems = new List<TreeViewItemData<string>>(1);

			var treeViewSubCharactersData = new List<TreeViewItemData<string>>(5);
			treeViewSubCharactersData.Add(new TreeViewItemData<string>(0, "Oleg"));
			treeViewSubCharactersData.Add(new TreeViewItemData<string>(1, "AnnaVladimitovna"));
			treeViewSubCharactersData.Add(new TreeViewItemData<string>(2, "Kat9"));
			treeViewSubCharactersData.Add(new TreeViewItemData<string>(3, "Nast9"));
			treeViewSubCharactersData.Add(new TreeViewItemData<string>(4, "Ton9"));
			treeViewSubCharactersData.Add(new TreeViewItemData<string>(5, "Veronika"));
			treeViewSubCharactersData.Add(new TreeViewItemData<string>(6, "Alisa"));

			var treeViewItemData = new TreeViewItemData<string>(110, "Characters", treeViewSubCharactersData);
			charactersItems.Add(treeViewItemData);

			Func<VisualElement> makeItem = () => new Label();
			Action<VisualElement, int> bindItem = (e, i) =>
			{
				var item = (_charactersTreeView).GetItemDataForIndex<string>(i);
				(e as Label).text = item;
			};

			_charactersTreeView = new TreeView();

			_charactersTreeView.SetRootItems(charactersItems);
			_charactersTreeView.makeItem = makeItem;
			_charactersTreeView.bindItem = bindItem;
			_charactersTreeView.selectionType = SelectionType.Multiple;
			_charactersTreeView.Rebuild();

			_charactersTreeView.selectedIndicesChanged += OnCharacterClick;

			_charactersTreeView.AddToClassList("characters-treeview");
			_chapterParametersBox.Add(_charactersTreeView);
			
			_root.Add(_chapterParametersBox);
		}

		private void CreateTextLabels()
		{
			GUILayout.BeginArea(new Rect(300, 0, position.width - 300, position.height));
			
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
			GUILayout.BeginHorizontal();

			for (int i = 0; i < 4; i++)
			{
				GUILayout.BeginVertical();

				newTexts[i] = i switch
				{
					0 => EditorGUILayout.TextField("Start " + (i + 1) + ":", newTexts[i]),
					1 => EditorGUILayout.TextField("Positive " + (i + 1) + ":", newTexts[i]),
					2 => EditorGUILayout.TextField("Negative " + (i + 1) + ":", newTexts[i]),
					3 => EditorGUILayout.TextField("End " + (i + 1) + ":", newTexts[i]),
					_ => newTexts[i]
				};
				GUILayout.Space(5);

				// Кнопка для добавления элемента в список
				if (GUILayout.Button("Add " + (i + 1)))
				{
					if (!string.IsNullOrEmpty(newTexts[i]))
					{
						if (textLists[i] == null)
						{
							textLists[i] = new List<string>();
						}
						textLists[i].Add(newTexts[i]);
						newTexts[i] = "";
					}
				}

				// Вывод текста в UI элемент
				if (textLists[i] != null)
				{
					foreach (string text in textLists[i])
					{
						EditorGUILayout.LabelField("Text " + (i + 1) + ":", text);
					}
				}

				GUILayout.EndVertical();
			}

			GUILayout.EndHorizontal();

			EditorGUILayout.EndScrollView();

			GUILayout.Space(10);

			
			// Кнопка для добавления всех введенных полей
			if (GUILayout.Button("Save All"))
			{
				Queue<string> startReplicas = new Queue<string>();
				Queue<string> positiveReplicas = new Queue<string>();
				Queue<string> negativeReplicas = new Queue<string>();
				Queue<string> endReplicas = new Queue<string>();
				
				for (int i = 0; i < textLists.Length; i++)
				{
					if(textLists[i] == null || textLists[i].IsEmpty())
						continue;
					for (int j = 0; j < textLists[i].Count; j++)
					{
						if(i==0)
							startReplicas.Enqueue(textLists[i][j]);
						else if(i==1)
							positiveReplicas.Enqueue(textLists[i][j]);
						else if(i==2)
							negativeReplicas.Enqueue(textLists[i][j]);
						else if(i==3)
							endReplicas.Enqueue(textLists[i][j]);
					}
				}
				_rootActor = new Actor(_currentCharacter, startReplicas, positiveReplicas, negativeReplicas, endReplicas);
			}

			// Кнопка для сериализации и сохранения в файле JSON
			if (GUILayout.Button("Save Text List as JSON"))
			{
				string json = JsonConvert.SerializeObject(_rootActor, Formatting.Indented);
				SaveJsonToFile(json);
			}
			GUILayout.Space(15);
			
			// Кнопка для очистки всех полей
			if (GUILayout.Button("Clear All Text Fields"))
			{
				for (int i = 0; i < 4; i++)
				{
					newTexts[i] = "";
					if (textLists[i] != null)
					{
						textLists[i].Clear();
					}
				}
			}
			
			GUILayout.EndArea();
		}
		private void SaveJsonToFile(string json)
		{
			string path = EditorUtility.SaveFilePanel("Save Text List as JSON", "", "textList", "json");
			if (path.Length != 0)
			{
				File.WriteAllText(path, json);
				Debug.Log("Text List saved as JSON: " + path);
			}
		}
		
		private void OnChaptersClick(object __sender)
		{
			Debug.Log("Chapter " + _chaptersListView.selectedIndex);

			_currentChapter = _chaptersListView.selectedIndex;
		}

		private void OnCharacterClick(object __sender)
		{
			if (_charactersTreeView.selectedIndex == 0)
				return;

			var insideIndex = _charactersTreeView.selectedIndex - 1;

			Debug.Log("Character " + insideIndex);

			_currentCharacter = (ActorType)insideIndex;
		}
		
	}
}
