Imports DevExpress.Mvvm

Namespace DocumentManagerSerialization.Common
    Public Class ViewModelState
        Inherits ViewModelBase

        Public Property State() As String
            Get
                Return GetProperty(Function() State)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() State, value)
            End Set
        End Property
        Public Property FullOwnerName() As String
            Get
                Return GetProperty(Function() FullOwnerName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() FullOwnerName, value)
            End Set
        End Property
    End Class
End Namespace
