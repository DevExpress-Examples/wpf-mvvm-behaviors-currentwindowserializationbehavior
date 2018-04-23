Imports System.Windows

Namespace DocumentManagerSerialization
    Partial Public Class App
        Inherits Application

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            MyBase.OnStartup(e)
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName()
        End Sub
        Protected Overrides Sub OnExit(ByVal e As ExitEventArgs)
            MyBase.OnExit(e)
            DevExpress.Xpf.Core.ApplicationThemeHelper.SaveApplicationThemeName()
        End Sub
    End Class
End Namespace
