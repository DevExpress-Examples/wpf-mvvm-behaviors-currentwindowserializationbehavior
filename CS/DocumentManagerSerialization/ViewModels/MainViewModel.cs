using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DocumentManagerSerialization.Properties;
using System.Collections.Generic;
using DocumentManagerSerialization.Common;

namespace DocumentManagerSerialization.ViewModels {
    [POCOViewModel]
    public class MainViewModel : ISupportLogicalLayout {

        public IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        public ILayoutSerializationService LayoutSerializationService { get { return this.GetService<ILayoutSerializationService>(); } }
        public virtual ViewModelState State { get; set; }

        public MainViewModel() {
            State = new ViewModelState() { State = "I'm Root!" };
        }

        [Command]
        public void OnWindowClosing() {
            Settings.Default.LogicalLayout = this.SerializeDocumentManagerService();
            Settings.Default.RootLayout = LayoutSerializationService.Serialize();
            Settings.Default.Save();
        }

        [Command]
        public void OnWindowLoaded() {
            if (Settings.Default.LogicalLayout != null) {
                this.RestoreDocumentManagerService(Settings.Default.LogicalLayout);
            }
            if (Settings.Default.RootLayout != null) {
                LayoutSerializationService.Deserialize(Settings.Default.RootLayout);
            }
        }

        [Command]
        public void OpenDocument() {
            var document = DocumentManagerService.CreateDocument("DocumentView", null, this);
            document.Id = "Document" + Guid.NewGuid().ToString().Replace("-", "");
            document.DestroyOnClose = false;
            document.Title = "Root Document";
            document.Show();
        }

        #region ISupportLogicalLayout
        public bool CanSerialize {
            get { return true; }
        }

        public IEnumerable<object> LookupViewModels {
            get { return null; }
        }
        #endregion
    }
}