Imports System
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Collections.Generic
Imports DocumentManagerSerialization.Common

Namespace DocumentManagerSerialization.ViewModels
    <POCOViewModel> _
    Public Class MainViewModel
        Implements ISupportLogicalLayout

        Public ReadOnly Property DocumentManagerService() As IDocumentManagerService Implements ISupportLogicalLayout.DocumentManagerService
            Get
                Return Me.GetService(Of IDocumentManagerService)()
            End Get
        End Property
        Public ReadOnly Property LayoutSerializationService() As ILayoutSerializationService
            Get
                Return Me.GetService(Of ILayoutSerializationService)()
            End Get
        End Property
        Public Overridable Property State() As ViewModelState

        Public Sub New()
            State = New ViewModelState() With {.State = "I'm Root!"}
        End Sub

        <Command> _
        Public Sub OnWindowClosing()
            My.Settings.Default.LogicalLayout = Me.SerializeDocumentManagerService()
            My.Settings.Default.RootLayout = LayoutSerializationService.Serialize()
            My.Settings.Default.Save()
        End Sub

        <Command> _
        Public Sub OnWindowLoaded()
            If My.Settings.Default.LogicalLayout IsNot Nothing Then
                Me.RestoreDocumentManagerService(My.Settings.Default.LogicalLayout)
            End If
            If My.Settings.Default.RootLayout IsNot Nothing Then
                LayoutSerializationService.Deserialize(My.Settings.Default.RootLayout)
            End If
        End Sub

        <Command> _
        Public Sub OpenDocument()
            Dim document = DocumentManagerService.CreateDocument("DocumentView", Nothing, Me)
            document.Id = "Document" & Guid.NewGuid().ToString().Replace("-", "")
            document.DestroyOnClose = False
            document.Title = "Root Document"
            document.Show()
        End Sub

        #Region "ISupportLogicalLayout"
        Public ReadOnly Property CanSerialize() As Boolean Implements ISupportLogicalLayout.CanSerialize
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property LookupViewModels() As IEnumerable(Of Object) Implements ISupportLogicalLayout.LookupViewModels
            Get
                Return Nothing
            End Get
        End Property
        #End Region
    End Class
End Namespace