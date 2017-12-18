using System.Collections.Generic;
using Assets.Common.Scripts.Events;
using UnityEngine;

namespace Assets.Common.Scripts.View
{
    interface IWindowsManager
    {
        EventWrap<string> WindowShowed { get; }
        EventWrap<string> WindowHidden { get; }
        void AddWindow(string id, GameObject gameObject);
        T GetWindow<T>(string id);
        //ICommand ShowWindowCommand(string id);
        //ICommand HideWindowCommand(string id);
        bool IsWindowActive(string id);
    }

    class WindowsManager : IWindowsManager
    {
        private Dictionary<string, GameObject> _windows = new Dictionary<string, GameObject>();

        public EventWrap<string> WindowShowed { get; private set; }
        public EventWrap<string> WindowHidden { get; private set; }

        public WindowsManager()
        {
            WindowShowed = new EventWrap<string>();
            WindowHidden = new EventWrap<string>();
        }

        public void AddWindow(string id, GameObject gameObject)
        {
            _windows.Add(id, gameObject);
        }

        public T GetWindow<T>(string id)
        {
            return _windows[id].GetComponent<T>();
        }

        /*
        public ICommand ShowWindowCommand(string id)
        {

        }

        public ICommand HideWindowCommand(string id)
        {

        }
        */

        public bool IsWindowActive(string id)
        {
            return _windows[id].activeSelf;
        }
    }
}
