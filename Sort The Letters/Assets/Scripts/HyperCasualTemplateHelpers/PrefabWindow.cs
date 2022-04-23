#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace HyperCasualTemplateHelpers
{
    public class PrefabWindow : EditorWindow
    {

        static List<List<string>> folderPrefabs = new List<List<string>>();
        static string[] subFolder;

        static int sellectedFolder = 0;

        static bool canOpenSceneWindow = false;
        static Vector2 sceneWindowPos = Vector2.zero;


        static Vector2 prefabPanelScroll = Vector2.zero;
        static Vector2 folderPanelScroll = Vector2.zero;

        static Camera sceneCamera;

        static bool altObjeOlarakEkle = false;
        static bool seciliHaldeOlustur = false;
        static bool mousePanelAc = false;
        static bool paneliAcıkBırak = false;

        static string prefabKlasoru = "Assets/Prefabs";

        [MenuItem("Hyper-Casual Template/Prefab Window")]
        static void ShowWindow()
        {
            var window = GetWindow<PrefabWindow>();
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Seçenekler");

            altObjeOlarakEkle = GUILayout.Toggle(altObjeOlarakEkle, "Alt Obje Olarak Ekle");
            seciliHaldeOlustur = GUILayout.Toggle(seciliHaldeOlustur, "Secili Halde Olustur");
            mousePanelAc = GUILayout.Toggle(mousePanelAc, "Mouse Konumunda Panel Ac");
            paneliAcıkBırak = GUILayout.Toggle(paneliAcıkBırak, "Prefab Panelini Acik Birak");

            GUILayout.Label("Secili Klasor: " + prefabKlasoru);

            if (GUILayout.Button("Prefab Klasoru Sec"))
            {
                PrefabSecmeEkrani();
            }

            if (prefabKlasoru == "")
            {
                EditorGUILayout.HelpBox("Prefab klasoru assets altinda olmalidir", MessageType.Warning);
            }

            GUILayout.Label("Secilen Alt Klasorler");

            for (int i = 0; i < subFolder.Length; i++)
            {
                GUILayout.Label(subFolder[i] + " klasoru ");
            }
        }

        void OnEnable()
        {
            PaneliYukle();
        }

        void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneView;
        }
        void OnInspectorUpdate()
        {
            PaneliYukle();
        }

        void PaneliYukle()
        {
            SceneView.duringSceneGui -= OnSceneView;
            SceneView.duringSceneGui += OnSceneView;


            Camera[] kameralar = SceneView.GetAllSceneCameras();


            if (kameralar == null || kameralar.Length == 0)
            {
                Debug.LogWarning("Kamera bos");
                return;
            }
            else
            {
                sceneCamera = kameralar[0];
            }

            PrefablariYukle();
        }

        void OnSceneView(SceneView scene)
        {
            if (sceneCamera == null)
                return;

            Handles.BeginGUI();

            if (canOpenSceneWindow)
            {
                PrefabPaneli();
            }
            Handles.EndGUI();

            Event e = Event.current;

            switch (e.type)
            {
                case EventType.KeyUp:
                    if (e.keyCode == KeyCode.Space)
                    {
                        canOpenSceneWindow = !canOpenSceneWindow;

                        if (mousePanelAc)
                        {
                            Vector2 geciciPos = sceneCamera.ScreenToViewportPoint(Event.current.mousePosition);

                            if (geciciPos.x > 0 && geciciPos.x < 1 && geciciPos.y > 0 && geciciPos.y < 1)
                            {
                                sceneWindowPos = sceneCamera.ViewportToScreenPoint(geciciPos);
                            }
                            else
                            {
                                sceneWindowPos = Vector2.zero;
                            }
                        }
                    }
                    break;
            }
        }

        void PrefabPaneli()
        {
            GUIStyle areaStyle = new GUIStyle(GUI.skin.box);
            areaStyle.alignment = TextAnchor.UpperCenter;

            Rect panelRect;

            if (mousePanelAc)
            {
                panelRect = new Rect(sceneWindowPos.x, sceneWindowPos.y, 200, 300);
            }
            else
            {
                panelRect = new Rect(0, 0, 240, SceneView.currentDrawingSceneView.camera.scaledPixelHeight);
            }

            GUILayout.BeginArea(panelRect, areaStyle);

            folderPanelScroll = GUILayout.BeginScrollView(folderPanelScroll, true, false, GUI.skin.horizontalScrollbar, GUIStyle.none, GUILayout.MinHeight(40));

            sellectedFolder = GUILayout.Toolbar(sellectedFolder, subFolder);

            GUILayout.EndScrollView();

            prefabPanelScroll = GUILayout.BeginScrollView(prefabPanelScroll, false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.MinHeight(panelRect.height - 40));

            for (int i = 0; i < folderPrefabs[sellectedFolder].Count; i++)
            {
                int index = folderPrefabs[sellectedFolder][i].LastIndexOf("/");
                string isim = "";
                if (index >= 0)
                {
                    isim = folderPrefabs[sellectedFolder][i].Substring(index + 1, folderPrefabs[sellectedFolder][i].Length - index - 8);
                }
                else
                {
                    isim = folderPrefabs[sellectedFolder][i];
                }

                GUIContent icerik = new GUIContent();
                icerik.text = isim;
                icerik.image = prefabGorseliAl(folderPrefabs[sellectedFolder][i]);

                if (GUILayout.Button(icerik))
                {
                    ObjeOlustur(folderPrefabs[sellectedFolder][i]);
                }
            }
            GUILayout.EndScrollView();

            GUILayout.EndArea();
        }

        void ObjeOlustur(string prefabYolu)
        {
            Object obj = AssetDatabase.LoadAssetAtPath<GameObject>(prefabYolu);

            GameObject yeniObje = PrefabUtility.InstantiatePrefab(obj as GameObject, EditorSceneManager.GetActiveScene()) as GameObject;

            if (Selection.activeGameObject != null && altObjeOlarakEkle)
            {
                yeniObje.transform.parent = Selection.activeGameObject.transform;
            }

            if (seciliHaldeOlustur)
            {
                Selection.activeGameObject = yeniObje;
            }

            Undo.RegisterCreatedObjectUndo(yeniObje, "Yeni eklenen objeyi kaldir");

            if (!paneliAcıkBırak)
                canOpenSceneWindow = false;
        }


        void PrefablariYukle()
        {

            if (prefabKlasoru == "")
            {
                return;
            }

            folderPrefabs.Clear();

            string[] klasorYollari = AssetDatabase.GetSubFolders(prefabKlasoru);
            subFolder = new string[klasorYollari.Length];


            for (int i = 0; i < klasorYollari.Length; i++)
            {
                int ayirmaIndeksi = klasorYollari[i].LastIndexOf('/');
                subFolder[i] = klasorYollari[i].Substring(ayirmaIndeksi + 1);
            }

            foreach (string klasor in klasorYollari)
            {
                List<string> gecici = new List<string>();
                string[] altPrefablar = AssetDatabase.FindAssets("t:prefab", new string[] { klasor });
                foreach (string prefabGUID in altPrefablar)
                {
                    string prefabYolu = AssetDatabase.GUIDToAssetPath(prefabGUID);
                    gecici.Add(prefabYolu);
                }
                folderPrefabs.Add(gecici);
            }
        }


        void PrefabSecmeEkrani()
        {
            string geciciYol = EditorUtility.OpenFolderPanel("Prefab Klasoru", "", "folder");

            int index = geciciYol.IndexOf("/Assets/");
            if (index >= 0)
            {
                prefabKlasoru = geciciYol.Substring(index + 1);
                PrefablariYukle();
            }
            else
            {
                prefabKlasoru = "";
            }
        }


        Texture2D prefabGorseliAl(string prefabYolu)
        {
            Object obj = AssetDatabase.LoadAssetAtPath<GameObject>(prefabYolu);
            return AssetPreview.GetAssetPreview(obj);
        }

    }
}
#endif