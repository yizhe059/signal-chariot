using System.Collections.Generic;
using World.SetUps;

namespace World.Modules
{
    public class ModuleLib
    {
        private List<Module> m_modules = new List<Module>();

        public void Init(List<ModuleSetUp> setUps)
        {
            foreach (var setUp in setUps)
            {
                m_modules.Add(Module.CreateModule(setUp));    
            }
            
        }

        public override string ToString()
        {
            var result = "";

            foreach (var module in m_modules)
            {
                result += $"[{module.ToString()}], ";
            }
            return result;
        }
    }
}