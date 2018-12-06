using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

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
        public void CreatGadgetTest()
        {
            var gadgeothekAdminWindow = _app.GetWindow("GadgeothekAdmin", InitializeOption.NoCache);
            var gadgetGrid = gadgeothekAdminWindow.Get<ListView>("GadgetTable");

            var countGadget = gadgetGrid.Items.Count;
            CreatNewGadget(gadgeothekAdminWindow);
            var countAddedGadget = gadgetGrid.Items.Count;

            Assert.IsTrue(countGadget + 1 == countAddedGadget);
        }

        [TestMethod]
        public void DeleteGadgetTest()
        {
            var gadgeothekAdminWindow = _app.GetWindow("GadgeothekAdmin", InitializeOption.NoCache);
            var gadgetGrid = gadgeothekAdminWindow.Get<ListView>("GadgetTable");

            var countGadget = gadgetGrid.Items.Count;

            DeleteGadget(gadgeothekAdminWindow, gadgetGrid);

            var countDeletedGadget = gadgetGrid.Items.Count;
            Assert.IsTrue(countGadget - 1 == countDeletedGadget);
        }

        private void DeleteGadget(Window gadgeothekAdminWindow, ListView gadgetGrid)
        {
            var firstTableEntrie = gadgetGrid.Rows.First().Cells.Last();
            firstTableEntrie.Click();

            Window deleteDialogWindow = gadgeothekAdminWindow.MessageBox("Delete gadget");
            Button okButton = deleteDialogWindow.Get<Button>("Ja");
            okButton.Click();
        }

        private void CreatNewGadget(Window gadgeothekAdminWindow)
        {
            var createButton = gadgeothekAdminWindow.Get<Button>("GadgetCreateButton");
            createButton.Click();
            var createGadgetWindow = _app.GetWindow("Gadget Details");
            var nameTextBox = createGadgetWindow.Get<TextBox>("TextName");
            nameTextBox.BulkText = "iPhone Xs";
            var manufacturerTextBox = createGadgetWindow.Get<TextBox>("TextManufacturer");
            manufacturerTextBox.BulkText = "Apfel";
            var priceTextBox = createGadgetWindow.Get<TextBox>("TexPrice");
            priceTextBox.BulkText = "1399";
            var conditionComboBox = createGadgetWindow.Get<ComboBox>("ConditionComboBox");
            conditionComboBox.Select(1);
            var applyButton = createGadgetWindow.Get<Button>("SaveButton");
            applyButton.Click();
        }
    }
}
