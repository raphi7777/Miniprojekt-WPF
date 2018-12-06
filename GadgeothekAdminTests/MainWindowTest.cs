using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;

namespace GadgeothekAdminTests
{
    [TestClass]
    public class MainWindowTest
    {
        private string _baseDir;
        private string _sutPath;
        private Application _app;

        [TestInitialize()]
        public void Initialize()
        {
            _baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _sutPath = Path.Combine(_baseDir, $"{nameof(GadgeothekAdmin)}.exe");
            _app = Application.Launch(_sutPath);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _app.Close();
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
