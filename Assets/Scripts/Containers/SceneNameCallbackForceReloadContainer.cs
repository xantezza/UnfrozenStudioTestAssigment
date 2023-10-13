using System;

namespace Containers
{
    public struct SceneNameCallbackForceReloadContainer
    {
        public SceneNameCallbackForceReloadContainer(string sceneName, Action callback = null, bool forceReload = false)
        {
            SceneName = sceneName;
            Callback = callback;
            ForceReload = forceReload;
        }

        public readonly string SceneName;
        public readonly Action Callback;
        public readonly bool ForceReload;
    }
}