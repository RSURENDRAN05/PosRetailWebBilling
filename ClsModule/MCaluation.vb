Imports System.Data
Imports System.IO
Imports System.Globalization
Module MCaluation
    Public RoundType_DiscountPruchaseRate As RoundType = RoundType.DefaultValue
    Public RoundType_TaxPurchaseamt As RoundType = RoundType.DefaultValue
    Public RoundType_PurchaseNetamt As RoundType = RoundType.DefaultValue
    Public Enum Tax
        Percentage2Price
        Price2Percentage
    End Enum

    Public Enum RoundType
        RoundValue
        DecimalValue
        DefaultValue
    End Enum
    Public Function setPurchaseNetPrice(ByVal PurchasePrice As Decimal, ByVal DiscountPer As Decimal, ByVal TaxPer As Decimal, ByRef PurchaseNetamt As Decimal, ByVal _RoundType As RoundType)
        Try
            Dim _Tax As Decimal = 0.0
            Dim _Dis As Decimal = 0.0
            Dim _PurchaseNet As Decimal = 0.0


            CalPercentage(PurchasePrice, DiscountPer, _Dis, Tax.Percentage2Price, RoundType_DiscountPruchaseRate)

            PurchasePrice = PurchasePrice - _Dis

            CalPercentage(PurchasePrice, TaxPer, _Tax, Tax.Percentage2Price, RoundType_TaxPurchaseamt)

            _PurchaseNet = (PurchasePrice + _Tax)

            '   _PurchaseNet = (PurchasePrice + _Tax) - _Dis

            Select Case _RoundType
                Case RoundType.DecimalValue
                    PurchaseNetamt = ConvertDecimal(_PurchaseNet)
                Case RoundType.RoundValue
                    PurchaseNetamt = Math.Round(_PurchaseNet, 0)
                Case RoundType.DefaultValue
                    PurchaseNetamt = Math.Round(_PurchaseNet, 2)
            End Select

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SetPPNetPrice(ByVal PurchasePrice As Decimal, ByVal DiscountPer As Decimal, ByVal TaxPer As Decimal, ByVal Taxtype As Integer, ByRef PurchaseNetamt As Decimal, ByVal _RoundType As RoundType, Optional ByRef Taxmount As Decimal = 0.0)
        Try
            Dim _Tax As Decimal = 0.0
            Dim _Dis As Decimal = 0.0
            Dim _PurchaseNet As Decimal = 0.0


            ' CalPercentage(PurchasePrice, DiscountPer, _Dis, Tax.Percentage2Price, RoundType_DiscountPruchaseRate)

            PurchasePrice = PurchasePrice - DiscountPer

            If Taxtype = 1 Then

                CalPercentage(PurchasePrice, TaxPer, _Tax, Tax.Percentage2Price, RoundType_TaxPurchaseamt)

                _PurchaseNet = (PurchasePrice + _Tax)
            Else
                _PurchaseNet = PurchasePrice

            End If

            Taxmount = Math.Round(_Tax, 4)

            '   _PurchaseNet = (PurchasePrice + _Tax) - _Dis

            Select Case _RoundType
                Case RoundType.DecimalValue
                    PurchaseNetamt = (_PurchaseNet)
                Case RoundType.RoundValue
                    PurchaseNetamt = Math.Round(_PurchaseNet, 0)
                Case RoundType.DefaultValue
                    PurchaseNetamt = Math.Round(_PurchaseNet, 4)
                    ''PurchaseNetamt = _PurchaseNet
            End Select

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CalPercentage(ByVal Price As Decimal, ByVal Value As Decimal, ByRef Result As Decimal, ByVal _Tax As Tax, Optional ByVal _Roundtype As RoundType = RoundType.DefaultValue, Optional ByRef ErrorMsg As String = "") As Boolean 'As Nullable(Of Decimal)

        Try

            Dim ResultValue As Double = 0.0
            Dim pi As Double = Math.PI

            Select Case _Tax
                Case Tax.Percentage2Price

                    ResultValue = (Price / 100) * Value

                Case Tax.Price2Percentage
                    If Price <= 0 Then
                        ResultValue = 0.0
                        Return True
                    Else
                        ResultValue = (Value / Price) * 100
                    End If

            End Select

            Select Case _Roundtype
                Case RoundType.DecimalValue
                    Result = ConvertDecimal(ResultValue)
                Case RoundType.RoundValue
                    Result = Math.Round(ResultValue, 1)
                Case RoundType.DefaultValue
                    Result = Math.Round(ResultValue, 2)
                    'Result = ResultValue
            End Select

            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function
    Function _RoundOff(ByRef AMT As Decimal) As Decimal
        Try
            Dim _roundoffs As Double = 0.0
            _roundoffs = Math.Round(AMT * 2, 1) / 2
            Return _roundoffs
        Catch ex As Exception
            '.WriteErroLog(ex.Message.ToString)
            Return 0.0
        End Try
    End Function
    Private k As Decimal = 0.0
    Private KL As Decimal = 0.0
    Private min As Decimal = 0.0
    Private distxtper As Decimal = 0.0
    Public Function DiscountAddTax(ByVal BasicRate As Decimal, ByVal DisPerValue As Decimal, ByVal TaxPerValue As Decimal, ByVal TaxType As Integer, ByRef SalesValue As Decimal, ByRef TaxValue As Decimal, ByRef DiscountValue As Decimal, ByRef ErrorMsg As String, ByVal _Tax As Tax) As Boolean
        Try
            Dim j As Decimal = 0.0
            Dim k As Decimal = 0.0
            If TaxType = 1 Then



                'Here Calculate the Discount 
                If CalPercentage(BasicRate, DisPerValue, KL, _Tax, RoundType.DefaultValue, ErrorMsg) = False Then
                    Return False
                End If

                DiscountValue = Format(KL, "0.00")

                j = Format((BasicRate - Format(KL, "0.00")), "0.00")

                KL = j - (j / (TaxPerValue / 100 + 1))

                ''Here Calculate the Tax
                'If CalPercentage(j, TaxPerValue, KL, Tax.Percentage2Price, RoundType.DefaultValue, ErrorMsg) = False Then
                '    Return False
                'End If

                TaxValue = KL
                SalesValue = j 'Format((j + KL), "0.00")

            Else

                'Here Calculate the Discount 
                If CalPercentage(BasicRate, DisPerValue, KL, _Tax, RoundType.DefaultValue, ErrorMsg) = False Then
                    Return False
                End If

                DiscountValue = Format(KL, "0.00")
                j = Format((BasicRate - Format(KL, "0.00")), "0.00")
                'Here Calculate the Tax
                If CalPercentage(j, TaxPerValue, KL, _Tax, RoundType.DefaultValue, ErrorMsg) = False Then
                    Return False
                End If

                TaxValue = Format(KL, "0.00")
                SalesValue = Format((j + Format(KL, "0.00")), "0.00")

            End If



            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function
    Public Function ConvertDecimal(ByVal Value As Decimal) As Nullable(Of Decimal)
        Try
            Dim v As String = Format(Math.Round(Value, 1), "#00.00")
            Dim str As String() = v.ToString.Split(".")
            Dim v1 As Decimal = str(0)
            Dim v2 As Decimal = str(1)
            Dim result As Decimal = 0.0

            If v2 >= 0 AndAlso v2 < 0.09 Then
                result = Math.Round(Value)
                'ElseIf v2 >= 46 AndAlso v2 <= 54 Then
                '   result = v1 & ".50"
            Else
                result = Math.Ceiling(Value)
            End If
            Return result
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Module
