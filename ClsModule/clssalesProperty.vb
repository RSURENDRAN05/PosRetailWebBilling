Public Class clssalesProperty
    Public Class properClass
        Public Shared R_BusinessDate As String
        Public Shared R_YesOrNo As String
        Public Shared R_Msgstring As String
        Public Shared R_pendinglistTable As DataTable
        Public Shared R_pendinglistBillnumber As Integer
        Public Shared R_allStatus As String
        Public Shared R_dgridRowCount As Integer
        Public Shared R_denoamt As Double
        Public Shared R_tableNo As String
        Public Shared R_TextNumKey As String
        Public Shared R_MenuCode As Integer
        Public Shared R_BooleanStatus As Boolean
        Public Shared R_CardScan As Boolean = False
        Public Shared MKeyQtyAmt As Decimal = 0
        Public Shared mkeynostatus As Boolean = False
          
        Private Property _CardScan As Boolean
            Get
                Return R_CardScan
            End Get
            Set(value As Boolean)
                R_CardScan = value
            End Set
        End Property

        Private Property _BooleanStatus As Boolean
            Get
                Return R_BooleanStatus
            End Get
            Set(value As Boolean)
                R_BooleanStatus = value
            End Set
        End Property
        Private Property _MenuCode As Integer
            Get
                Return R_MenuCode
            End Get
            Set(value As Integer)
                R_MenuCode = value
            End Set
        End Property
        Private Property _TextNumKey As String
            Get
                Return R_TextNumKey
            End Get
            Set(value As String)
                R_TextNumKey = value
            End Set
        End Property
        Private Property _tableNo As String
            Get
                Return R_tableNo
            End Get
            Set(value As String)
                R_tableNo = value
            End Set
        End Property
        Private Property _BusinessDate As String
            Get
                Return R_BusinessDate
            End Get
            Set(value As String)
                R_BusinessDate = value
            End Set
        End Property
        Private Property _YesOrNo As String
            Get
                Return R_YesOrNo
            End Get
            Set(value As String)
                R_YesOrNo = value
            End Set
        End Property
        Private Property _Msgstring As String
            Get
                Return R_Msgstring
            End Get
            Set(value As String)
                R_Msgstring = value
            End Set
        End Property
        Private Property _pendinglistTable As DataTable
            Get
                Return R_pendinglistTable
            End Get
            Set(value As DataTable)
                R_pendinglistTable = value
            End Set
        End Property
        Private Property _pendinglistBillnumber As Integer
            Get
                Return R_pendinglistBillnumber
            End Get
            Set(value As Integer)
                R_pendinglistBillnumber = value
            End Set
        End Property
        Private Property _allStatus As String
            Get
                Return R_allStatus
            End Get
            Set(value As String)
                R_allStatus = value
            End Set
        End Property
        Private Property _dgridRowCount As Integer
            Get
                Return R_dgridRowCount
            End Get
            Set(value As Integer)
                R_dgridRowCount = value
            End Set
        End Property
        Private Property _denoamt As Double
            Get
                Return R_denoamt
            End Get
            Set(value As Double)
                R_denoamt = value
            End Set
        End Property
    End Class
End Class
