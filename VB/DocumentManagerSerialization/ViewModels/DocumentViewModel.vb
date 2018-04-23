Imports System
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Collections.Generic
Imports DocumentManagerSerialization.Common

Namespace DocumentManagerSerialization.ViewModels
    <POCOViewModel> _
    Public Class DocumentViewModel
        Implements ISupportLogicalLayout(Of ViewModelState), ISupportParentViewModel, IDocumentContent

        Public ReadOnly Property DocumentManagerService() As IDocumentManagerService Implements ISupportLogicalLayout(Of DocumentManagerSerialization.Common.ViewModelState).DocumentManagerService
            Get
                Return Me.GetService(Of IDocumentManagerService)()
            End Get
        End Property

        Public Overridable Property Caption() As String
        Public Overridable Property CanBeClosed() As Boolean
        Public Overridable Property State() As ViewModelState


        Public Sub New()
            State = New ViewModelState() With {.State = "Default State"}
        End Sub

        Public Sub OpenChildDocument()
            Dim document = DocumentManagerService.CreateDocument("DocumentView", Nothing, Me)
            document.DestroyOnClose = False
            document.Title = "Child Document"
            document.Id = "Child" & Guid.NewGuid().ToString().Replace("-", "")
            document.Show()
        End Sub


#Region "ISupportLogicalLayout"
        Public Function SaveState() As ViewModelState Implements ISupportLogicalLayout(Of DocumentManagerSerialization.Common.ViewModelState).SaveState
            Return State
        End Function

        Public Sub RestoreState(ByVal state As ViewModelState) Implements ISupportLogicalLayout(Of DocumentManagerSerialization.Common.ViewModelState).RestoreState
            Me.State = state
        End Sub


        Private ReadOnly Property ISupportLogicalLayout_CanSerialize() As Boolean Implements ISupportLogicalLayout.CanSerialize
            Get
                Return Not String.IsNullOrEmpty(State.State)
            End Get
        End Property

        Public ReadOnly Property LookupViewModels() As IEnumerable(Of Object) Implements ISupportLogicalLayout.LookupViewModels
            Get
                Return Nothing
            End Get
        End Property

#End Region

        #Region "ISupportParentViewModel"

        Public Overridable Property ParentViewModel() As Object Implements ISupportParentViewModel.ParentViewModel

        #End Region

        #Region "IDocumentContent"

        Public Property DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner

        Public Sub OnClose(ByVal e As System.ComponentModel.CancelEventArgs) Implements IDocumentContent.OnClose
            e.Cancel = Not CanBeClosed
        End Sub

        Public Sub OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub

        Public ReadOnly Property Title() As Object Implements IDocumentContent.Title
            Get
                Return Caption
            End Get
        End Property

        #End Region
    End Class
End Namespace