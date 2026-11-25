using System;
using System.Collections.Generic;

namespace Nova.Engine.AI
{
    public static class NovaDecisionUtility
    {
        public struct Option<T>
        {
            public T Value;
            public float Score;

            public Option(T value, float score)
            {
                Value = value;
                Score = score;
            }
        }

        public static bool TryChooseBest<T>(IReadOnlyList<Option<T>> options, out T choice)
        {
            choice = default(T);
            if (options == null || options.Count == 0) return false;

            int bestIndex = 0;
            float bestScore = options[0].Score;

            for (int i = 1; i < options.Count; i++)
            {
                if (options[i].Score > bestScore)
                {
                    bestScore = options[i].Score;
                    bestIndex = i;
                }
            }

            choice = options[bestIndex].Value;
            return true;
        }

        public static float NormalizeScore(float value, float min, float max)
        {
            if (Math.Abs(max - min) < 0.0001f) return 0f;
            float normalized = (value - min) / (max - min);
            return Math.Max(0f, Math.Min(1f, normalized));
        }
    }
}
