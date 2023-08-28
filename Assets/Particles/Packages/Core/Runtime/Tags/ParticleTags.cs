using System;
using System.Collections.Generic;
using Particles.Packages.BaseParticles.Runtime.Variables;
using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Tags
{
    [AddComponentMenu("Particles/Tags/Particle Tags")]
    public sealed class ParticleTags : MonoBehaviour, ISerializationCallbackReceiver
    {
        public ReadOnlyList<StringVariable> Tags
        {
            get
            {
                if (readOnlyTags == null || readOnlyTags.Count != sortedTags.Values.Count)
                {
                    readOnlyTags = new ReadOnlyList<StringVariable>(sortedTags.Values);
                }
                
                return readOnlyTags;
            }
            
            private set => readOnlyTags = value;
        }

        private ReadOnlyList<StringVariable> readOnlyTags;

        [SerializeField] private List<StringVariable> tags = new();

        private SortedList<string, StringVariable> sortedTags = new();

        private static readonly Dictionary<string, List<GameObject>> TaggedGameObjects = new();
        private static readonly Dictionary<GameObject, ParticleTags> TagInstances = new();

        private static Action onInitialization;
        
        #region Serialization
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying && !EditorApplication.isUpdating && !EditorApplication.isCompiling) return;
#endif
            tags.Clear();
            foreach (var kvp in sortedTags)
            {
                tags.Add(kvp.Value);
            }
        }
        
        public void OnAfterDeserialize()
        {
            sortedTags = new SortedList<string, StringVariable>();

            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i] == null || tags[i].Value == null) continue;
                if (sortedTags.ContainsKey(tags[i].Value)) continue;
                sortedTags.Add(tags[i].Value, tags[i]);
            }
        }
        #endregion
        
        #region Lifecycles

        private void OnEnable()
        {
            if (!IsInitialized(gameObject))
            {
                TaggedGameObjects.Clear();
                TagInstances.Clear();
                var tagsInScene = GameObject.FindObjectsOfType<ParticleTags>();
                
                for (int i = 0; i < tagsInScene.Length; i++)
                {
                    var tags = tagsInScene[i];
                    var tagCount = tags.Tags.Count;
                    var go = tags.gameObject;
                    
                    if (IsInitialized(go)) TagInstances.Add(go, tags);
                    for (int y = 0; y < tagCount; y++)
                    {
                        var stringVariable = tags.Tags[y];
                        if (stringVariable == null) continue;
                        var tag = stringVariable.Value;
                        if (!TaggedGameObjects.ContainsKey(tag)) TaggedGameObjects.Add(tag, new List<GameObject>());
                        TaggedGameObjects[tag].Add(go);
                    }
                }

                onInitialization?.Invoke();
                onInitialization = null;
            }
        }

        private void OnDisable()
        {
            if (TagInstances.ContainsKey(gameObject)) TagInstances.Remove(gameObject);
            for (var i = 0; i < Tags.Count; i++)
            {
                var stringVariable = Tags[i];
                if (stringVariable == null) continue;
                var tag = stringVariable.Value;
                if (TaggedGameObjects.ContainsKey(tag)) TaggedGameObjects[tag].Remove(gameObject);
            }
        }
        #endregion

        public static void OnInitialization(Action handler)
        {
            var tags = GameObject.FindObjectOfType<ParticleTags>();
            if (tags != null && !IsInitialized(tags.gameObject))
            {
                onInitialization += handler;
            }
            else
            {
                handler();
            }
        }

        public bool HasTag()
        {
            if (tag == null) return false;
            return sortedTags.ContainsKey(tag);
        }

        public void AddTag(StringVariable tag)
        {
            if (tag == null || tag.Value == null) return;
            if (sortedTags.ContainsKey(tag.Value)) return;
            sortedTags.Add(tag.Value, tag);

            Tags = new ReadOnlyList<StringVariable>(sortedTags.Values);
            
            if (!TaggedGameObjects.ContainsKey(tag.Value)) TaggedGameObjects.Add(tag.Value, new List<GameObject>());
            TaggedGameObjects[tag.Value].Add(gameObject);
        }

        public void RemoveTag(string tag)
        {
            if (string.IsNullOrEmpty(tag)) return;
            if (!sortedTags.ContainsKey(tag)) return;
            sortedTags.Remove(tag);

            Tags = new ReadOnlyList<StringVariable>(sortedTags.Values);

            if (!TaggedGameObjects.ContainsKey(tag)) return; // Apparently this should never happen.
            TaggedGameObjects[tag].Remove(gameObject);
        }

        public static GameObject FindByTag(string tag)
        {
            if (!TaggedGameObjects.ContainsKey(tag)) return null;
            if (TaggedGameObjects[tag].Count < 1) return null;
            return TaggedGameObjects[tag][0];
        }
        
        public static GameObject[] FindAllByTag(string tag)
        {
            if (!TaggedGameObjects.ContainsKey(tag)) return null;
            return TaggedGameObjects[tag].ToArray();
        }

        public static void FindAllByTagNoAlloc(string tag, List<GameObject> output)
        {
            output.Clear();
            if (!TaggedGameObjects.ContainsKey(tag)) return;
            for (var i = 0; i < TaggedGameObjects[tag].Count; i++)
            {
                output.Add(TaggedGameObjects[tag][i]);
            }
        }

        public static ParticleTags GetTagsForGameObject(GameObject go)
        {
            if (IsInitialized(go)) return null;
            return TagInstances[go];
        }

        public static ReadOnlyList<StringVariable> GetTags(GameObject go)
        {
            if (IsInitialized(go)) return null;
            var tags = TagInstances[go];
            return tags.Tags;
        }

        private static bool IsInitialized(GameObject go) => TagInstances.ContainsKey(go);
    }
}