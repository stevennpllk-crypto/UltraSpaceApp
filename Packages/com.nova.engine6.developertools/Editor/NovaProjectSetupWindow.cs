#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Nova.Engine6.DeveloperTools
{
    public class NovaProjectSetupWindow : EditorWindow
    {
        private string rootFolder = "Assets/NOVA";
        private bool createScenesFolder = true;
        private bool createScriptsFolders = true;
        private bool createArtFolders = true;
        private bool createAudioFolders = true;
        private bool createDocumentationFolder = true;

        [MenuItem("Tools/NOVA/Project Setup Wizard")]
        public static void Open()
        {
            GetWindow<NovaProjectSetupWindow>("NOVA Setup");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("NOVA ENGINE 6 - Project Setup", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "ينشئ هذا المعالج هيكل مجلدات منظم داخل Assets. لا يحذف أو يستبدل ملفات موجودة.",
                MessageType.Info);

            rootFolder = EditorGUILayout.TextField("Root Folder", rootFolder);
            EditorGUILayout.Space();
            createScenesFolder = EditorGUILayout.ToggleLeft("Scenes", createScenesFolder);
            createScriptsFolders = EditorGUILayout.ToggleLeft("Scripts and Gameplay", createScriptsFolders);
            createArtFolders = EditorGUILayout.ToggleLeft("Art, Materials and Prefabs", createArtFolders);
            createAudioFolders = EditorGUILayout.ToggleLeft("Audio", createAudioFolders);
            createDocumentationFolder = EditorGUILayout.ToggleLeft("Documentation", createDocumentationFolder);

            EditorGUILayout.Space();
            if (GUILayout.Button("Create Project Folders", GUILayout.Height(32)))
                CreateFolders();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Android Build Checklist", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("1. Install Android Build Support, SDK, NDK and OpenJDK in Unity Hub.");
            EditorGUILayout.LabelField("2. Switch Platform to Android in Build Profiles / Build Settings.");
            EditorGUILayout.LabelField("3. Set a unique Package Name in Player Settings.");
            EditorGUILayout.LabelField("4. Configure Minimum API Level and target API level for your release.");
            EditorGUILayout.LabelField("5. Test on a real Android device before publishing.");
        }

        private void CreateFolders()
        {
            string normalizedRoot = rootFolder.Replace("\\", "/").TrimEnd('/');
            if (string.IsNullOrWhiteSpace(normalizedRoot) || !normalizedRoot.StartsWith("Assets"))
            {
                EditorUtility.DisplayDialog("NOVA Setup", "Root Folder must be inside Assets.", "OK");
                return;
            }

            CreateAssetFolderPath(normalizedRoot);

            if (createScenesFolder) CreateAssetFolderPath(normalizedRoot + "/Scenes");
            if (createScriptsFolders)
            {
                CreateAssetFolderPath(normalizedRoot + "/Scripts");
                CreateAssetFolderPath(normalizedRoot + "/Scripts/Gameplay");
                CreateAssetFolderPath(normalizedRoot + "/Scripts/UI");
                CreateAssetFolderPath(normalizedRoot + "/Scripts/Systems");
            }
            if (createArtFolders)
            {
                CreateAssetFolderPath(normalizedRoot + "/Art");
                CreateAssetFolderPath(normalizedRoot + "/Art/Materials");
                CreateAssetFolderPath(normalizedRoot + "/Art/Models");
                CreateAssetFolderPath(normalizedRoot + "/Prefabs");
                CreateAssetFolderPath(normalizedRoot + "/Textures");
            }
            if (createAudioFolders)
            {
                CreateAssetFolderPath(normalizedRoot + "/Audio");
                CreateAssetFolderPath(normalizedRoot + "/Audio/Music");
                CreateAssetFolderPath(normalizedRoot + "/Audio/SFX");
            }
            if (createDocumentationFolder)
                CreateAssetFolderPath(normalizedRoot + "/Documentation");

            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("NOVA Setup", "Project folders created. Existing files were not changed.", "OK");
        }

        private static void CreateAssetFolderPath(string assetPath)
        {
            string[] parts = assetPath.Split('/');
            if (parts.Length == 0 || parts[0] != "Assets")
                return;

            string current = "Assets";
            for (int i = 1; i < parts.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(parts[i]))
                    continue;

                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);

                current = next;
            }
        }
    }
}
#endif
