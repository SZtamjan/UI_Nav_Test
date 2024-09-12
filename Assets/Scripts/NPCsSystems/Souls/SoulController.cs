using System.Collections.Generic;
using ScriptableObjectsScripts;
using Singleton;

namespace NPCsSystems.Souls
{
    public class SoulController : Singleton<SoulController>
    {
        public List<SoulItem> Souls;
    }
}