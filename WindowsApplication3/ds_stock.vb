

Partial Public Class ds_stock
    Partial Class dt_stock5DataTable


    End Class

    Partial Class dt_stockDataTable

        Private Sub dt_stockDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.trndate1Column.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
