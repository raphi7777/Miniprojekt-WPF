﻿using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.TabItems;
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
        public void CreateGadgetTest()
        {
            var gadgeothekAdminWindow = _app.GetWindow("GadgeothekAdmin", InitializeOption.NoCache);
            var gadgetGrid = gadgeothekAdminWindow.Get<ListView>("GadgetTable");

            var countGadget = gadgetGrid.Items.Count;
            CreateNewGadget(gadgeothekAdminWindow);
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

        private void CreateNewGadget(Window gadgeothekAdminWindow)
        {
            var createButton = gadgeothekAdminWindow.Get<Button>("GadgetCreateButton");
            createButton.Click();
            var createGadgetWindow = _app.GetWindow("Gadget Details");
            var nameTextBox = createGadgetWindow.Get<TextBox>("TextName");
            nameTextBox.BulkText = "iPhone Xs";
            var manufacturerTextBox = createGadgetWindow.Get<TextBox>("TextManufacturer");
            manufacturerTextBox.BulkText = "Apfel";
            var priceTextBox = createGadgetWindow.Get<TextBox>("TextPrice");
            priceTextBox.BulkText = "1399";
            var conditionComboBox = createGadgetWindow.Get<ComboBox>("ConditionComboBox");
            conditionComboBox.Select(1);
            var applyButton = createGadgetWindow.Get<Button>("SaveButton");
            applyButton.Click();
        }

        [TestMethod]
        public void CreateCustomerTest()
        {
            var gadgeothekAdminWindow = _app.GetWindow("GadgeothekAdmin", InitializeOption.NoCache);
            var tabControl = gadgeothekAdminWindow.Get<Tab>("Tabs");
            tabControl.SelectTabPage("Customer List");
            var customerGrid = gadgeothekAdminWindow.Get<ListView>("CustomerTable");

            var countCustomer = customerGrid.Items.Count;
            CreateNewCustomer(gadgeothekAdminWindow);
            var countAddedCustomer = customerGrid.Items.Count;

            Assert.IsTrue(countCustomer + 1 == countAddedCustomer);
        }

        [TestMethod]
        public void DeleteCustomerTest()
        {
            var gadgeothekAdminWindow = _app.GetWindow("GadgeothekAdmin", InitializeOption.NoCache);
            var tabControl = gadgeothekAdminWindow.Get<Tab>("Tabs");
            tabControl.SelectTabPage("Customer List");
            var customerGrid = gadgeothekAdminWindow.Get<ListView>("CustomerTable");

            var countCustomer = customerGrid.Items.Count;

            DeleteCustomer(gadgeothekAdminWindow, customerGrid);

            var countDeletedCustomer = customerGrid.Items.Count;
            Assert.IsTrue(countCustomer - 1 == countDeletedCustomer);
        }

        private void CreateNewCustomer(Window gadgeothekAdminWindow)
        {
            var createButton = gadgeothekAdminWindow.Get<Button>("CustomerCreateButton");
            createButton.Click();
            var createCustomerWindow = _app.GetWindow("Create new Customer");
            var nameTextBox = createCustomerWindow.Get<TextBox>("TextName");
            nameTextBox.BulkText = "Anna";
            var emailTextBox = createCustomerWindow.Get<TextBox>("TextEmail");
            emailTextBox.BulkText = "anna@test.ch";
            var passwordTextBox = createCustomerWindow.Get<TextBox>("TextPassword");
            passwordTextBox.BulkText = "passwort123";
            var studentnumberTextBox = createCustomerWindow.Get<TextBox>("TextStudentnumber");
            studentnumberTextBox.BulkText = "501";
            var applyButton = createCustomerWindow.Get<Button>("SaveButton");
            applyButton.Click();
        }
        
        private void DeleteCustomer(Window gadgeothekAdminWindow, ListView customerGrid)
        {
            var firstTableEntrie = customerGrid.Rows.First().Cells.Last();
            firstTableEntrie.Click();

            Window deleteDialogWindow = gadgeothekAdminWindow.MessageBox("Delete customer");
            Button okButton = deleteDialogWindow.Get<Button>("Ja");
            okButton.Click();
        }
    }
}
