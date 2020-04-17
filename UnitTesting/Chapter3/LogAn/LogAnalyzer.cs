using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3.LogAn
{
    public interface IExtensionManager
    {
        bool IsValid(string fileName);
    }
    public class LogAnalyzer
    {
        private IExtensionManager manager;
        public IExtensionManager ExtensionManager
        {
            get { return manager; }
            set { manager = value; }
        }
        public LogAnalyzer(IExtensionManager mgr)
        {
            manager = mgr;
        }
        public bool IsValidLogFileName(string fileName)
        {
            return manager.IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length > 5;
        }
    }
}
