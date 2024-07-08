/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace StalkerZero.Infrastructure.MVVM.Editors
{
    public class StringListSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        private string[] m_options;
        private Action<string> m_callback;

        public void Init(string[] options, Action<string> callback)
        {
            m_options = options;
            m_callback = callback;
        }
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var searchTree = new List<SearchTreeEntry>();
            searchTree.Add(new SearchTreeGroupEntry(new GUIContent("Search"), 0));
            foreach (var option in m_options)
            {
                var entry = new SearchTreeEntry(new GUIContent(option));
                entry.level = 1;
                entry.userData = option;
                searchTree.Add(entry);
            }
            return searchTree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            m_callback?.Invoke(SearchTreeEntry.userData as string);
            return true;
        }
    }
}
