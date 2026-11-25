#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Nova.Engine6.DeveloperTools
{
    public class NovaProjectValidatorWindow : EditorWindow
    {
        private readonly List<string> results = new List<string>();
        private Vector2 scroll;
        private bool hasRun;

        [MenuItem("Tools/NOVA/Project Validator")]
        public static void Open()
        {
            GetWindow<NovaProjectValidatorWindow>("NOVA Validator");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("NOVA ENGINE 6 - Project Validator", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "فحص أولي لإعدادات المشروع. النتائج لا تعني أن المشروع تم تجميعه أو اختباره.",
                MessageType.Info);

            if (GUILayout.Button("Run Validation", GUILayout.Height(30)))
                RunValidation();

            if (!hasRun)
                return;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Validation Results", EditorStyles.boldLabel);
            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (string item in results)
                EditorGUILayout.HelpBox(item, item.StartsWith("PASS") ? MessageType.Info :
                    item.StartsWith("WARN") ? MessageType.Warning : MessageType.Error);
            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Clear Results"))
            {
                results.Clear();
                hasRun = false;
            }
        }

        private void RunValidation()
        {
            results.Clear();
            hasRun = true;

            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            int enabledScenes = 0;
            foreach (EditorBuildSettingsScene scene in scenes)
            {
                if (scene.enabled && !string.IsNullOrEmpty(scene.path))
                    enabledScenes++;
            }

            AddCheck(scenes.Length > 0,
                "Build Settings contains " + scenes.Length + " scene(s).",
                "No scenes are listed in Build Settings.");

            AddCheck(enabledScenes > 0,
                enabledScenes + " enabled scene(s) found.",
                "No enabled scenes found in Build Settings.");

            AddCheck(!string.IsNullOrWhiteSpace(PlayerSettings.companyName),
                "Company Name is set.",
                "Company Name is empty.");

            AddCheck(!string.IsNullOrWhiteSpace(PlayerSettings.productName),
                "Product Name is set.",
                "Product Name is empty.");

            string packagesPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../Packages"));
            string[] requiredPackages = {
                "com.nova.engine6.core",
                "com.nova.engine6.graphics",
                "com.nova.engine6.physics",
                "com.nova.engine6.ai",
                "com.nova.engine6.world",
                "com.nova.engine6.audio",
                "com.nova.engine6.input",
                "com.nova.engine6.ui"
            };

            foreach (string packageName in requiredPackages)
            {
                bool exists = Directory.Exists(Path.Combine(packagesPath, packageName));
                AddCheck(exists, packageName + " found.", packageName + " is missing from Packages.");
            }

            results.Insert(0, "PASS Unity Editor detected: " + Application.unityVersion);
        }

        private void AddCheck(bool passed, string success, string failure)
        {
            results.Add((passed ? "PASS " : "WARN ") + (passed ? success : failure));
        }
    }
}
#endif
