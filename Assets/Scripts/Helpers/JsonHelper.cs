using UnityEngine;

namespace Helpers
{
    public static class JsonHelper
    {
        public static T? FromJson<T>(string json) where T : class
        {
            var textAsset = Resources.Load<TextAsset>(json);

            if (string.IsNullOrEmpty(textAsset.text))
            {
                return null;
            }
        
            var obj = JsonUtility.FromJson<T>(textAsset.text);

            return obj;
        }
    }
}