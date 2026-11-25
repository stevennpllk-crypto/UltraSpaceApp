using System;
using System.IO;
using UnityEngine;

namespace Nova.Engine
{
    [Serializable]
    internal sealed class NovaSaveEnvelope<T>
    {
        public T data;
    }

    /// <summary>JSON save helper for non-sensitive game data.</summary>
    public static class NovaSaveSystem
    {
        private static string GetPath(string slot)
        {
            if (string.IsNullOrWhiteSpace(slot))
                throw new ArgumentException("Save slot cannot be empty.", nameof(slot));

            foreach (char invalid in Path.GetInvalidFileNameChars())
                slot = slot.Replace(invalid, '_');

            return Path.Combine(Application.persistentDataPath, "nova_" + slot + ".json");
        }

        public static bool Save<T>(string slot, T data, bool prettyPrint = true)
        {
            try
            {
                var envelope = new NovaSaveEnvelope<T> { data = data };
                string json = JsonUtility.ToJson(envelope, prettyPrint);
                File.WriteAllText(GetPath(slot), json);
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[NOVA] Save failed: {exception.Message}");
                return false;
            }
        }

        public static bool TryLoad<T>(string slot, out T data)
        {
            data = default(T);

            try
            {
                string path = GetPath(slot);
                if (!File.Exists(path))
                    return false;

                string json = File.ReadAllText(path);
                NovaSaveEnvelope<T> envelope =
                    JsonUtility.FromJson<NovaSaveEnvelope<T>>(json);

                if (envelope == null)
                    return false;

                data = envelope.data;
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[NOVA] Load failed: {exception.Message}");
                return false;
            }
        }

        public static bool Delete(string slot)
        {
            try
            {
                string path = GetPath(slot);
                if (!File.Exists(path))
                    return false;

                File.Delete(path);
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[NOVA] Delete failed: {exception.Message}");
                return false;
            }
        }
    }
}
