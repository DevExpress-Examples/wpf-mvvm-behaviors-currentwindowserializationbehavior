using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using DocumentManagerSerialization.Common;

namespace DocumentManagerSerialization.ViewModels {
    [POCOViewModel]
    public class DocumentViewModel : ISupportLogicalLayout<ViewModelState>, ISupportParentViewModel, IDocumentContent {

        public IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        
        public virtual string Caption { get; set; }
        public virtual bool CanBeClosed { get; set; }
        public virtual ViewModelState State { get; set; }


        public DocumentViewModel() {
            State = new ViewModelState() { State = "Default State" };
        }

        public void OpenChildDocument() {
            var document = DocumentManagerService.CreateDocument("DocumentView", null, this);
            document.DestroyOnClose = false;
            document.Title = "Child Document";
            document.Id = "Child" + Guid.NewGuid().ToString().Replace("-", "");
            document.Show();
        }

        public ViewModelState SaveState() {
            return State;
        }

        public void RestoreState(ViewModelState state) {
            State = state;
        }

        #region ISupportLogicalLayout

        bool ISupportLogicalLayout.CanSerialize {
            get { return !String.IsNullOrEmpty(State.State); }
        }

        public IEnumerable<object> LookupViewModels {
            get { return null; }
        }

        #endregion

        #region ISupportParentViewModel

        public virtual object ParentViewModel { get; set; }

        #endregion

        #region IDocumentContent

        public IDocumentOwner DocumentOwner { get; set; }

        public void OnClose(System.ComponentModel.CancelEventArgs e) {
            e.Cancel = !CanBeClosed;
        }

        public void OnDestroy() { }

        public object Title {
            get { return Caption; }
        } 

        #endregion
    }
}