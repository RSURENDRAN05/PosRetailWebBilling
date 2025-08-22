Imports System.Data
Imports System.Net
Imports System.Text
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports DevExpress.XtraEditors

Module managementModule
#Region "CreatePM"
    ''' <summary>
    ''' Structure to hold POS Master data
    ''' </summary>
    Public Structure PosMasterData
        Public PM_ID As Integer
        Public PM_MACHINE_NAME As String
        Public PM_BUINESS_DATE As String
        Public PM_TRANS_NO As String
        Public PM_USER_ID As Integer
        Public PSR_BILL_NUMBER As String
        Public PM_DAY_NO As Integer
        Public PM_SHIFT_NO As Integer
        Public PM_DAY_ST As String
        Public PM_SHIFT_ST As String
        Public PM_PREFIX As String
        Public PM_BATCH_NUMBER As String
        Public PM_MAILSTATUS As String
        Public PM_MONTHDATE As String
        Public PM_AUTOUPDATE As String
        Public PM_WEBID As String
        Public PM_RESTID As String
    End Structure

    ''' <summary>
    ''' Insert new POS Master record
    ''' </summary>
    ''' <param name="posData">POS Master data structure</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function InsertPosMaster(posData As PosMasterData, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "MgmtRequest=1&Action=POS_MASTER&Operation=INSERT&Comid={0}&Locid={1}" &
                "&PM_MACHINE_NAME={2}&PM_BUINESS_DATE={3}&PM_TRANS_NO={4}&PM_USER_ID={5}" &
                "&PSR_BILL_NUMBER={6}&PM_DAY_NO={7}&PM_SHIFT_NO={8}&PM_DAY_ST={9}" &
                "&PM_SHIFT_ST={10}&PM_PREFIX={11}&PM_BATCH_NUMBER={12}&PM_MAILSTATUS={13}" &
                "&PM_MONTHDATE={14}&PM_AUTOUPDATE={15}&PM_WEBID={16}&PM_RESTID={17}",
                comId, locId,
                Uri.EscapeDataString(posData.PM_MACHINE_NAME),
                Uri.EscapeDataString(posData.PM_BUINESS_DATE),
                Uri.EscapeDataString(posData.PM_TRANS_NO),
                posData.PM_USER_ID,
                Uri.EscapeDataString(posData.PSR_BILL_NUMBER),
                posData.PM_DAY_NO,
                posData.PM_SHIFT_NO,
                Uri.EscapeDataString(posData.PM_DAY_ST),
                Uri.EscapeDataString(posData.PM_SHIFT_ST),
                Uri.EscapeDataString(posData.PM_PREFIX),
                Uri.EscapeDataString(posData.PM_BATCH_NUMBER),
                Uri.EscapeDataString(posData.PM_MAILSTATUS),
                Uri.EscapeDataString(posData.PM_MONTHDATE),
                Uri.EscapeDataString(posData.PM_AUTOUPDATE),
                Uri.EscapeDataString(posData.PM_WEBID),
                Uri.EscapeDataString(posData.PM_RESTID)
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))
            Dim message As String = jsonResponse("Msg").ToString()

            Return New Tuple(Of Boolean, String)(success, message)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error inserting POS Master: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Update existing POS Master record
    ''' </summary>
    ''' <param name="posData">POS Master data structure</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function UpdatePosMaster(posData As PosMasterData, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "MgmtRequest=1&Action=POS_MASTER&Operation=UPDATE&Comid={0}&Locid={1}&PM_ID={2}" &
                "&PM_MACHINE_NAME={3}&PM_BUINESS_DATE={4}&PM_TRANS_NO={5}&PM_USER_ID={6}" &
                "&PSR_BILL_NUMBER={7}&PM_DAY_NO={8}&PM_SHIFT_NO={9}&PM_DAY_ST={10}" &
                "&PM_SHIFT_ST={11}&PM_PREFIX={12}&PM_BATCH_NUMBER={13}&PM_MAILSTATUS={14}" &
                "&PM_MONTHDATE={15}&PM_AUTOUPDATE={16}&PM_WEBID={17}&PM_RESTID={18}",
                comId, locId, posData.PM_ID,
                Uri.EscapeDataString(posData.PM_MACHINE_NAME),
                Uri.EscapeDataString(posData.PM_BUINESS_DATE),
                Uri.EscapeDataString(posData.PM_TRANS_NO),
                posData.PM_USER_ID,
                Uri.EscapeDataString(posData.PSR_BILL_NUMBER),
                posData.PM_DAY_NO,
                posData.PM_SHIFT_NO,
                Uri.EscapeDataString(posData.PM_DAY_ST),
                Uri.EscapeDataString(posData.PM_SHIFT_ST),
                Uri.EscapeDataString(posData.PM_PREFIX),
                Uri.EscapeDataString(posData.PM_BATCH_NUMBER),
                Uri.EscapeDataString(posData.PM_MAILSTATUS),
                Uri.EscapeDataString(posData.PM_MONTHDATE),
                Uri.EscapeDataString(posData.PM_AUTOUPDATE),
                Uri.EscapeDataString(posData.PM_WEBID),
                Uri.EscapeDataString(posData.PM_RESTID)
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))
            Dim message As String = jsonResponse("Msg").ToString()

            Return New Tuple(Of Boolean, String)(success, message)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error updating POS Master: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Get POS Master records by company and location ID
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>DataTable with POS Master records</returns>
    Public Function GetPosMasterRecords(comId As Integer, locId As Integer) As DataTable
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "MgmtRequest=1&Action=POS_MASTER&Operation=GET&Comid={0}&Locid={1}",
                comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))

            If success AndAlso jsonResponse("Data") IsNot Nothing Then
                Dim dataTable As DataTable = jsonResponse("Data").ToObject(Of DataTable)()
                Return dataTable
            Else
                Return New DataTable()
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error getting POS Master records: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Helper function to send GET request using WebClient with TLS 1.2
    ''' </summary>
    ''' <param name="url">Base URL</param>
    ''' <param name="postData">Query parameters to append to URL</param>
    ''' <returns>Response string</returns>
    Private Function SendPostRequest(url As String, postData As String) As String
        Try
            ' Set security protocol to TLS 1.2 to avoid SSL/TLS issues
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            ' Construct full URL with query parameters
            Dim fullUrl As String = url & postData

            ' Debug: Log the full URL being called
            Console.WriteLine("Calling URL: " & fullUrl)

            ' Use WebClient for simple GET request
            Dim json As String = New System.Net.WebClient().DownloadString(fullUrl)

            ' Debug: Log the response
            Console.WriteLine("Response received: " & json.Substring(0, Math.Min(500, json.Length)))

            Return json

        Catch webEx As System.Net.WebException
            ' Handle specific web exceptions
            Dim errorMessage As String = "HTTP Request failed: " & webEx.Message
            If webEx.Response IsNot Nothing Then
                Try
                    Using reader As New System.IO.StreamReader(webEx.Response.GetResponseStream())
                        Dim errorResponse As String = reader.ReadToEnd()
                        errorMessage &= " Server Response: " & errorResponse.Substring(0, Math.Min(200, errorResponse.Length))
                    End Using
                Catch
                    ' Ignore if can't read error response
                End Try
            End If
            XtraMessageBox.Show(errorMessage, "HTTP Request Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw New Exception(errorMessage)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "HTTP Request Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw New Exception("HTTP Request failed: " & ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Create a new POS Master data structure with default values
    ''' </summary>
    ''' <returns>New PosMasterData structure</returns>
    Public Function CreateNewPosMasterData() As PosMasterData
        Dim newData As New PosMasterData()
        newData.PM_ID = 0
        newData.PM_MACHINE_NAME = Environment.MachineName
        newData.PM_BUINESS_DATE = DateTime.Now.ToString("yyyy-MM-dd")
        newData.PM_TRANS_NO = "1"
        newData.PM_USER_ID = functionModule._companyInfo.UserId
        newData.PSR_BILL_NUMBER = "1"
        newData.PM_DAY_NO = 1
        newData.PM_SHIFT_NO = 1
        newData.PM_DAY_ST = "Open"
        newData.PM_SHIFT_ST = "Open"
        newData.PM_PREFIX = "Inv-25"
        newData.PM_BATCH_NUMBER = "1"
        newData.PM_MAILSTATUS = "Pending"
        newData.PM_MONTHDATE = DateTime.Now.ToString("yyyy-MM")
        newData.PM_AUTOUPDATE = "1"
        newData.PM_WEBID = "0"
        newData.PM_RESTID = "0"
        Return newData
    End Function

    ''' <summary>
    ''' Save POS Master record (Insert if PM_ID = 0, Update if PM_ID > 0)
    ''' </summary>
    ''' <param name="posData">POS Master data</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function SavePosMaster(posData As PosMasterData, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        If posData.PM_ID = 0 Then
            Return InsertPosMaster(posData, comId, locId)
        Else
            Return UpdatePosMaster(posData, comId, locId)
        End If
    End Function

    ''' <summary>
    ''' Validate POS Master data before saving
    ''' </summary>
    ''' <param name="posData">POS Master data to validate</param>
    ''' <returns>Validation result with message</returns>
    Public Function ValidatePosMasterData(posData As PosMasterData) As Tuple(Of Boolean, String)
        ' Required field validations
        If String.IsNullOrEmpty(posData.PM_MACHINE_NAME) Then
            Return New Tuple(Of Boolean, String)(False, "Machine Name is required")
        End If

        If String.IsNullOrEmpty(posData.PM_BUINESS_DATE) Then
            Return New Tuple(Of Boolean, String)(False, "Business Date is required")
        End If

        If posData.PM_USER_ID <= 0 Then
            Return New Tuple(Of Boolean, String)(False, "Valid User ID is required")
        End If

        ' Date format validation
        Dim businessDate As DateTime
        If Not DateTime.TryParse(posData.PM_BUINESS_DATE, businessDate) Then
            Return New Tuple(Of Boolean, String)(False, "Invalid Business Date format")
        End If

        Return New Tuple(Of Boolean, String)(True, "Validation passed")
    End Function

    ''' <summary>
    ''' Show success message
    ''' </summary>
    ''' <param name="message">Message to show</param>
    Public Sub ShowSuccessMessage(message As String)
        XtraMessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Show error message
    ''' </summary>
    ''' <param name="message">Error message to show</param>
    Public Sub ShowErrorMessage(message As String)
        XtraMessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ''' <summary>
    ''' Validate if Company ID and Location ID exist and are active before insert/update
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function ValidateCompanyLocation(comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for GET operation to validate company/location
            Dim postData As String = String.Format(
                "MgmtRequest=1&Action=POS_MASTER&Operation=GET&Comid={0}&Locid={1}",
                comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Debug: Check if response is empty or invalid
            If String.IsNullOrEmpty(response.Trim()) Then
                Return New Tuple(Of Boolean, String)(False, "Empty response from server")
            End If

            ' Check if response is HTML error page
            If response.Trim().StartsWith("<") Then
                Return New Tuple(Of Boolean, String)(False, "Server returned HTML error page instead of JSON")
            End If

            Try
                ' Parse response
                Dim jsonResponse As JObject = JObject.Parse(response)
                Dim success As Boolean = CBool(jsonResponse("Success"))

                If success Then
                    ' Company and location combination is valid
                    Return New Tuple(Of Boolean, String)(True, "Company and Location are valid")
                Else
                    ' Check the message to determine the exact issue
                    Dim message As String = If(jsonResponse("Msg") IsNot Nothing, jsonResponse("Msg").ToString(), "Invalid Company ID or Location ID")
                    Return New Tuple(Of Boolean, String)(False, message)
                End If
            Catch jsonEx As Exception
                ' JSON parsing failed
                Return New Tuple(Of Boolean, String)(False, "Invalid JSON response from server. Response: " & response.Substring(0, Math.Min(200, response.Length)))
            End Try

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error validating Company/Location: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Insert new POS Master record with validation
    ''' </summary>
    ''' <param name="posData">POS Master data structure</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="skipValidation">Skip company/location validation (default: False)</param>
    ''' <returns>Success status and message</returns>
    Public Function InsertPosMasterWithValidation(posData As PosMasterData, comId As Integer, locId As Integer, Optional skipValidation As Boolean = False) As Tuple(Of Boolean, String)
        Try
            ' Validate company and location first (unless skipped)
            If Not skipValidation Then
                Dim validation = ValidateCompanyLocation(comId, locId)
                If Not validation.Item1 Then
                    Return New Tuple(Of Boolean, String)(False, "Validation failed: " & validation.Item2)
                End If
            End If

            ' Proceed with insert
            Return InsertPosMaster(posData, comId, locId)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error inserting POS Master with validation: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Update POS Master record with validation
    ''' </summary>
    ''' <param name="posData">POS Master data structure</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="skipValidation">Skip company/location validation (default: False)</param>
    ''' <returns>Success status and message</returns>
    Public Function UpdatePosMasterWithValidation(posData As PosMasterData, comId As Integer, locId As Integer, Optional skipValidation As Boolean = False) As Tuple(Of Boolean, String)
        Try
            ' Validate company and location first (unless skipped)
            If Not skipValidation Then
                Dim validation = ValidateCompanyLocation(comId, locId)
                If Not validation.Item1 Then
                    Return New Tuple(Of Boolean, String)(False, "Validation failed: " & validation.Item2)
                End If
            End If

            ' Proceed with update
            Return UpdatePosMaster(posData, comId, locId)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error updating POS Master with validation: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Save POS Master record with validation (Insert if PM_ID = 0, Update if PM_ID > 0)
    ''' </summary>
    ''' <param name="posData">POS Master data</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="skipValidation">Skip company/location validation (default: False)</param>
    ''' <returns>Success status and message</returns>
    Public Function SavePosMasterWithValidation(posData As PosMasterData, comId As Integer, locId As Integer, Optional skipValidation As Boolean = False) As Tuple(Of Boolean, String)
        If posData.PM_ID = 0 Then
            Return InsertPosMasterWithValidation(posData, comId, locId, skipValidation)
        Else
            Return UpdatePosMasterWithValidation(posData, comId, locId, skipValidation)
        End If
    End Function


#End Region
#Region "POS Master"
    ''' <summary>
    ''' Update Transaction Number (PM_TRANS_NO)
    ''' </summary>
    ''' <param name="pmId">POS Master ID</param>
    ''' <param name="transNo">New transaction number</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function UpdateTransactionNumber(pmId As Integer, transNo As String, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for MgmtRequest=6 (UpdateTransNo)
            Dim postData As String = String.Format(
                "MgmtRequest=2&PM_ID={0}&PM_TRANS_NO={1}&Comid={2}&Locid={3}",
                pmId, Uri.EscapeDataString(transNo), comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))
            Dim message As String = jsonResponse("Msg").ToString()

            Return New Tuple(Of Boolean, String)(success, message)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error updating transaction number: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Update Bill Number (PSR_BILL_NUMBER)
    ''' </summary>
    ''' <param name="pmId">POS Master ID</param>
    ''' <param name="billNumber">New bill number</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function UpdateBillNumber(pmId As Integer, billNumber As String, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for MgmtRequest=7 (UpdateBillNumber)
            Dim postData As String = String.Format(
                "MgmtRequest=3&PM_ID={0}&PM_BILL_NUMBER={1}&Comid={2}&Locid={3}",
                pmId, Uri.EscapeDataString(billNumber), comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))
            Dim message As String = jsonResponse("Msg").ToString()

            Return New Tuple(Of Boolean, String)(success, message)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error updating bill number: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Update Batch Number (PM_BATCH_NUMBER)
    ''' </summary>
    ''' <param name="pmId">POS Master ID</param>
    ''' <param name="batchNumber">New batch number</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function UpdateBatchNumber(pmId As Integer, batchNumber As String, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for MgmtRequest=8 (UpdateBatchNumber)
            Dim postData As String = String.Format(
                "MgmtRequest=4&PM_ID={0}&PM_BATCH_NUMBER={1}&Comid={2}&Locid={3}",
                pmId, Uri.EscapeDataString(batchNumber), comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))
            Dim message As String = jsonResponse("Msg").ToString()

            Return New Tuple(Of Boolean, String)(success, message)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error updating batch number: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Update Auto Update Setting (PM_AUTOUPDATE)
    ''' </summary>
    ''' <param name="pmId">POS Master ID</param>
    ''' <param name="autoUpdate">Auto update setting (0 or 1)</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function UpdateAutoUpdate(pmId As Integer, autoUpdate As String, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for MgmtRequest=9 (UpdateAutoUpdate)
            Dim postData As String = String.Format(
                "MgmtRequest=5&PM_ID={0}&PM_AUTOUPDATE={1}&Comid={2}&Locid={3}",
                pmId, Uri.EscapeDataString(autoUpdate), comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))
            Dim message As String = jsonResponse("Msg").ToString()

            Return New Tuple(Of Boolean, String)(success, message)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error updating auto update setting: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Update Mail Status (PM_MAILSTATUS)
    ''' </summary>
    ''' <param name="pmId">POS Master ID</param>
    ''' <param name="mailStatus">Mail status</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function UpdateMailStatus(pmId As Integer, mailStatus As String, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for MgmtRequest=10 (UpdateMailStatus)
            Dim postData As String = String.Format(
                "MgmtRequest=6&PM_ID={0}&PM_MAILSTATUS={1}&Comid={2}&Locid={3}",
                pmId, Uri.EscapeDataString(mailStatus), comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))
            Dim message As String = jsonResponse("Msg").ToString()

            Return New Tuple(Of Boolean, String)(success, message)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error updating mail status: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Update Month Date (PM_MONTHDATE)
    ''' </summary>
    ''' <param name="pmId">POS Master ID</param>
    ''' <param name="monthDate">Month date</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status and message</returns>
    Public Function UpdateMonthDate(pmId As Integer, monthDate As String, comId As Integer, locId As Integer) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for MgmtRequest=11 (UpdateMonthDate)
            Dim postData As String = String.Format(
                "MgmtRequest=7&PM_ID={0}&PM_MONTHDATE={1}&Comid={2}&Locid={3}",
                pmId, Uri.EscapeDataString(monthDate), comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))
            Dim message As String = jsonResponse("Msg").ToString()

            Return New Tuple(Of Boolean, String)(success, message)

        Catch ex As Exception
            Return New Tuple(Of Boolean, String)(False, "Error updating month date: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Get POS Master data by PM_ID, Company ID, and Location ID
    ''' </summary>
    ''' <param name="pmId">POS Master ID</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>DataTable with POS Master data</returns>
    Public Function GetPosMasterByID(pmId As Integer, comId As Integer, locId As Integer) As DataTable
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for MgmtRequest=12 (GetAllData)
            Dim postData As String = String.Format(
                "MgmtRequest=8&PM_ID={0}&Comid={1}&Locid={2}",
                pmId, comId, locId
            )

            ' Send POST request
            Dim response As String = SendPostRequest(url, postData)

            ' Parse response
            Dim jsonResponse As JObject = JObject.Parse(response)
            Dim success As Boolean = CBool(jsonResponse("Success"))

            If success AndAlso jsonResponse("Data") IsNot Nothing Then
                ' Convert JSON array directly to DataTable
                Dim dataTable As DataTable = jsonResponse("Data").ToObject(Of DataTable)()
                Return dataTable
            Else
                Return New DataTable()
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error retrieving POS Master data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New DataTable()
        End Try
    End Function

#End Region
#Region "ShiftClose"
    ''' <summary>
    ''' Structure to hold Shift Close data
    ''' </summary>
    Public Structure ShiftCloseData
        Public PSC_ID As Integer
        Public PSC_CURDATE As String
        Public PSC_OPBALANCE As Decimal
        Public PSC_TODAYSALES As Decimal
        Public PSC_TOTDISCOUNT As Decimal
        Public PSC_TOTTAX As Decimal
        Public PSC_NETAMT As Decimal
        Public PSC_SERVICETAX As Decimal
        Public PSC_TODAYIN As Decimal
        Public PSC_TODAYOUT As Decimal
        Public PSC_TODAYBANKING As Decimal
        Public PSC_CLSBALANCE As Decimal
        Public PSC_STATE As String
        Public PSC_COMID As Integer
        Public PSC_LOCID As Integer
        Public PSC_SHIFTNO As Integer
        Public PSC_DAYNO As Integer
        Public PSC_PCNAME As String
        Public PSC_TOTBILLS As Integer
        Public PSC_CANCELAMT As Decimal
        Public PSC_CREDITSALES As Decimal
        Public PSC_OPENDRAWER As Integer
        Public PSC_CLSDRAWER As Integer
        Public PSC_USERID As String
        Public PSC_SMAIL As String
        Public PSC_PRINT As String
    End Structure

    ''' <summary>
    ''' Create new shift
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="opBalance">Opening Balance</param>
    ''' <param name="shiftNo">Shift Number</param>
    ''' <param name="dayNo">Day Number</param>
    ''' <param name="pcName">PC Name</param>
    ''' <param name="userId">User ID</param>
    ''' <returns>Success status and data</returns>
    Public Function CreateNewShift(comId As Integer, locId As Integer, Optional opBalance As Decimal = 0,
                                 Optional shiftNo As Integer = 1, Optional dayNo As Integer = 1,
                                 Optional pcName As String = "", Optional userId As String = "") As Tuple(Of Boolean, String, DataTable)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "ShiftCloseRequest=1&Comid={0}&Locid={1}&PSC_OPBALANCE={2}&PSC_SHIFTNO={3}" &
                "&PSC_DAYNO={4}&PSC_PCNAME={5}&PSC_USERID={6}",
                comId, locId, opBalance, shiftNo, dayNo,
                System.Uri.EscapeDataString(pcName),
                System.Uri.EscapeDataString(userId))

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim responseString As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(responseString)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Dim dataTable As New DataTable()
                    If jsonResponse("Data") IsNot Nothing Then
                        dataTable = jsonResponse("Data").ToObject(Of DataTable)()
                    End If
                    Return Tuple.Create(True, jsonResponse("Msg").ToString(), dataTable)
                Else
                    Return Tuple.Create(False, jsonResponse("Msg").ToString(), New DataTable())
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error creating new shift: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, "Error: " & ex.Message, New DataTable())
        End Try
    End Function

    ''' <summary>
    ''' Validate shift status before closing
    ''' </summary>
    ''' <param name="pscId">Shift Close ID</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Validation result with status</returns>
    Public Function ValidateShiftStatus(pscId As Integer, comId As Integer, locId As Integer) As Tuple(Of Boolean, Boolean, String, String, Integer)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data for validation
            Dim postData As String = String.Format(
                "ShiftCloseRequest=5&PSC_ID={0}&Comid={1}&Locid={2}",
                pscId, comId, locId)

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim responseString As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(responseString)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Dim canClose As Boolean = jsonResponse("CanClose").ToObject(Of Boolean)()
                    Dim shiftState As String = jsonResponse("ShiftState").ToString()
                    Dim shiftNo As Integer = jsonResponse("ShiftNo").ToObject(Of Integer)()
                    Dim message As String = jsonResponse("Msg").ToString()

                    Return Tuple.Create(True, canClose, shiftState, message, shiftNo)
                Else
                    Return Tuple.Create(False, False, "", jsonResponse("Msg").ToString(), 0)
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error validating shift status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, False, "", "Error: " & ex.Message, 0)
        End Try
    End Function

    ''' <summary>
    ''' Close shift with validation and update POS Master
    ''' </summary>
    ''' <param name="shiftData">Shift close data</param>
    ''' <param name="pmId">POS Master ID for updating shift number</param>
    ''' <returns>Success status and message</returns>
    Public Function CloseShift(shiftData As ShiftCloseData, Optional pmId As Integer = 0) As Tuple(Of Boolean, String)
        Try
            ' First validate the shift
            Dim validation = ValidateShiftStatus(shiftData.PSC_ID, shiftData.PSC_COMID, shiftData.PSC_LOCID)

            If Not validation.Item1 Then
                Return Tuple.Create(False, validation.Item4) ' Return validation error
            End If

            If Not validation.Item2 Then
                Return Tuple.Create(False, validation.Item4) ' Cannot close - already closed or other issue
            End If

            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "ShiftCloseRequest=2&PSC_ID={0}&Comid={1}&Locid={2}&PM_ID={3}" &
                "&PSC_TODAYSALES={4}&PSC_TOTDISCOUNT={5}&PSC_TOTTAX={6}&PSC_NETAMT={7}" &
                "&PSC_SERVICETAX={8}&PSC_TODAYIN={9}&PSC_TODAYOUT={10}&PSC_TODAYBANKING={11}" &
                "&PSC_CLSBALANCE={12}&PSC_TOTBILLS={13}&PSC_CANCELAMT={14}&PSC_CREDITSALES={15}" &
                "&PSC_OPENDRAWER={16}&PSC_CLSDRAWER={17}&PSC_SMAIL={18}&PSC_PRINT={19}&PSC_STATE=Close",
                shiftData.PSC_ID, shiftData.PSC_COMID, shiftData.PSC_LOCID, pmId,
                shiftData.PSC_TODAYSALES, shiftData.PSC_TOTDISCOUNT, shiftData.PSC_TOTTAX,
                shiftData.PSC_NETAMT, shiftData.PSC_SERVICETAX, shiftData.PSC_TODAYIN,
                shiftData.PSC_TODAYOUT, shiftData.PSC_TODAYBANKING, shiftData.PSC_CLSBALANCE,
                shiftData.PSC_TOTBILLS, shiftData.PSC_CANCELAMT, shiftData.PSC_CREDITSALES,
                shiftData.PSC_OPENDRAWER, shiftData.PSC_CLSDRAWER,
                System.Uri.EscapeDataString(shiftData.PSC_SMAIL),
                System.Uri.EscapeDataString(shiftData.PSC_PRINT))

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim responseString As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(responseString)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Return Tuple.Create(True, jsonResponse("Msg").ToString())
                Else
                    Return Tuple.Create(False, jsonResponse("Msg").ToString())
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error closing shift: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, "Error: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Get current open shift
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Current open shift data</returns>
    Public Function GetCurrentOpenShift(comId As Integer, locId As Integer) As DataTable
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "ShiftCloseRequest=3&Comid={0}&Locid={1}",
                comId, locId)

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim responseString As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(responseString)

                If jsonResponse("Success").ToObject(Of Boolean)() AndAlso jsonResponse("Data") IsNot Nothing Then
                    ' Convert single record to DataTable
                    Dim dataTable As New DataTable()
                    Dim record As JObject = jsonResponse("Data")

                    ' Create columns
                    For Each prop As JProperty In record.Properties()
                        dataTable.Columns.Add(prop.Name)
                    Next

                    ' Add data row
                    Dim row As DataRow = dataTable.NewRow()
                    For Each prop As JProperty In record.Properties()
                        row(prop.Name) = If(prop.Value.Type = JTokenType.Null, DBNull.Value, prop.Value.ToString())
                    Next
                    dataTable.Rows.Add(row)

                    Return dataTable
                Else
                    Return New DataTable()
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error retrieving current open shift: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Get all shifts for company/location
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="limit">Number of records to retrieve (default 50)</param>
    ''' <returns>DataTable with shift records</returns>
    Public Function GetAllShifts(comId As Integer, locId As Integer, Optional limit As Integer = 50) As DataTable
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "ShiftCloseRequest=4&Comid={0}&Locid={1}&LIMIT={2}",
                comId, locId, limit)

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim responseString As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(responseString)

                If jsonResponse("Success").ToObject(Of Boolean)() AndAlso jsonResponse("Data") IsNot Nothing Then
                    Dim dataTable As DataTable = jsonResponse("Data").ToObject(Of DataTable)()
                    Return dataTable
                Else
                    Return New DataTable()
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error retrieving shift data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Helper function to create ShiftCloseData structure from form inputs
    ''' </summary>
    ''' <param name="pscId">Shift ID</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="todaySales">Today's Sales</param>
    ''' <param name="totalDiscount">Total Discount</param>
    ''' <param name="totalTax">Total Tax</param>
    ''' <param name="netAmount">Net Amount</param>
    ''' <param name="serviceTax">Service Tax</param>
    ''' <param name="todayIn">Today In</param>
    ''' <param name="todayOut">Today Out</param>
    ''' <param name="todayBanking">Today Banking</param>
    ''' <param name="closeBalance">Closing Balance</param>
    ''' <param name="totalBills">Total Bills</param>
    ''' <param name="cancelAmount">Cancel Amount</param>
    ''' <param name="creditSales">Credit Sales</param>
    ''' <param name="openDrawer">Open Drawer Count</param>
    ''' <param name="closeDrawer">Close Drawer Count</param>
    ''' <param name="email">Email Status</param>
    ''' <param name="print">Print Status</param>
    ''' <returns>ShiftCloseData structure</returns>
    Public Function CreateShiftCloseData(pscId As Integer, comId As Integer, locId As Integer,
                                       todaySales As Decimal, totalDiscount As Decimal, totalTax As Decimal,
                                       netAmount As Decimal, serviceTax As Decimal, todayIn As Decimal,
                                       todayOut As Decimal, todayBanking As Decimal, closeBalance As Decimal,
                                       totalBills As Integer, cancelAmount As Decimal, creditSales As Decimal,
                                       openDrawer As Integer, closeDrawer As Integer,
                                       Optional email As String = "", Optional print As String = "") As ShiftCloseData

        Dim shiftData As New ShiftCloseData()
        shiftData.PSC_ID = pscId
        shiftData.PSC_COMID = comId
        shiftData.PSC_LOCID = locId
        shiftData.PSC_TODAYSALES = todaySales
        shiftData.PSC_TOTDISCOUNT = totalDiscount
        shiftData.PSC_TOTTAX = totalTax
        shiftData.PSC_NETAMT = netAmount
        shiftData.PSC_SERVICETAX = serviceTax
        shiftData.PSC_TODAYIN = todayIn
        shiftData.PSC_TODAYOUT = todayOut
        shiftData.PSC_TODAYBANKING = todayBanking
        shiftData.PSC_CLSBALANCE = closeBalance
        shiftData.PSC_TOTBILLS = totalBills
        shiftData.PSC_CANCELAMT = cancelAmount
        shiftData.PSC_CREDITSALES = creditSales
        shiftData.PSC_OPENDRAWER = openDrawer
        shiftData.PSC_CLSDRAWER = closeDrawer
        shiftData.PSC_SMAIL = email
        shiftData.PSC_PRINT = print
        shiftData.PSC_STATE = "Close"

        Return shiftData
    End Function

    ''' <summary>
    ''' Validate current shift status and create new shift if needed
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="userId">User ID</param>
    ''' <param name="pcName">PC Name</param>
    ''' <returns>Tuple with success status, action taken, message, and shift data</returns>
    Public Function ValidateCurrentShiftAndCreate(comId As Integer, locId As Integer,
                                                 Optional userId As String = "",
                                                 Optional pcName As String = "") As Tuple(Of Boolean, String, String, Object)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "ShiftCloseRequest=6&Comid={0}&Locid={1}&USERID={2}&PCNAME={3}",
                comId, locId,
                System.Uri.EscapeDataString(userId),
                System.Uri.EscapeDataString(pcName))

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim responseString As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(responseString)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Dim action As String = jsonResponse("Action").ToString()
                    Dim message As String = jsonResponse("Message").ToString()

                    ' Create result object with all data
                    Dim resultData As Object = New With {
                        .Action = action,
                        .Message = message,
                        .ShiftNo = If(jsonResponse("ShiftNo") IsNot Nothing, jsonResponse("ShiftNo").ToObject(Of Integer?)(), Nothing),
                        .DayNo = If(jsonResponse("DayNo") IsNot Nothing, jsonResponse("DayNo").ToObject(Of Integer?)(), Nothing),
                        .PM_ID = If(jsonResponse("PM_ID") IsNot Nothing, jsonResponse("PM_ID").ToObject(Of Integer?)(), Nothing),
                        .ShiftData = If(jsonResponse("Data") IsNot Nothing, jsonResponse("Data"), Nothing)
                    }

                    Return Tuple.Create(True, action, message, resultData)
                Else
                    Dim action As String = If(jsonResponse("Action") IsNot Nothing, jsonResponse("Action").ToString(), "error")
                    Dim message As String = jsonResponse("Msg").ToString()
                    Return Tuple.Create(False, action, message, CType(Nothing, Object))
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error validating shift status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, "error", "Error: " & ex.Message, CType(Nothing, Object))
        End Try
    End Function
#End Region

#Region "Day Close Management"
    ''' <summary>
    ''' Create a new day record
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="opBalance">Opening balance</param>
    ''' <param name="shiftNo">Shift number</param>
    ''' <param name="dayNo">Day number</param>
    ''' <param name="pcName">PC name</param>
    ''' <param name="userId">User ID</param>
    ''' <returns>Success status, message and day data</returns>
    Public Function CreateNewDay(comId As Integer, locId As Integer, opBalance As Decimal, shiftNo As Integer, dayNo As Integer, pcName As String, userId As String) As Tuple(Of Boolean, String, Object)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "DayCloseRequest=1&Comid={0}&Locid={1}&PSD_OPBALANCE={2}&PSD_SHIFTNO={3}&PSD_DAYNO={4}&PSD_PCNAME={5}&PSD_USERID={6}",
                comId, locId, opBalance, shiftNo, dayNo, Uri.EscapeDataString(pcName), Uri.EscapeDataString(userId)
            )

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim response As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(response)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Dim dayData As Object = jsonResponse("Data").ToObject(Of Object)()
                    Return Tuple.Create(True, jsonResponse("Msg").ToString(), dayData)
                Else
                    Return Tuple.Create(False, jsonResponse("Msg").ToString(), CType(Nothing, Object))
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error creating new day: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, "Error: " & ex.Message, CType(Nothing, Object))
        End Try
    End Function

    ''' <summary>
    ''' Close a day and update POS Master
    ''' </summary>
    ''' <param name="dayId">Day ID to close</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="pmId">POS Master ID</param>
    ''' <param name="dayCloseData">Dictionary containing day close data</param>
    ''' <returns>Success status and message</returns>
    Public Function CloseDayAndIncrement(dayId As Integer, comId As Integer, locId As Integer, pmId As Integer, dayCloseData As Dictionary(Of String, Object)) As Tuple(Of Boolean, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Build POST data with day close information
            Dim postDataBuilder As New StringBuilder()
            postDataBuilder.AppendFormat("DayCloseRequest=2&PSD_ID={0}&Comid={1}&Locid={2}&PM_ID={3}", dayId, comId, locId, pmId)

            ' Add day close data fields
            For Each kvp In dayCloseData
                postDataBuilder.AppendFormat("&{0}={1}", kvp.Key.ToUpper(), Uri.EscapeDataString(kvp.Value.ToString()))
            Next

            Dim postData As String = postDataBuilder.ToString()

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim response As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(response)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Return Tuple.Create(True, jsonResponse("Msg").ToString())
                Else
                    Return Tuple.Create(False, jsonResponse("Msg").ToString())
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error closing day: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, "Error: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Get current open day record
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status, message and day data</returns>
    Public Function GetCurrentOpenDay(comId As Integer, locId As Integer) As Tuple(Of Boolean, String, Object)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            Dim postData As String = String.Format("DayCloseRequest=3&Comid={0}&Locid={1}", comId, locId)

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim response As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(response)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Dim dayData As Object = jsonResponse("Data").ToObject(Of Object)()
                    Return Tuple.Create(True, jsonResponse("Msg").ToString(), dayData)
                Else
                    Return Tuple.Create(False, jsonResponse("Msg").ToString(), CType(Nothing, Object))
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error getting current open day: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, "Error: " & ex.Message, CType(Nothing, Object))
        End Try
    End Function

    ''' <summary>
    ''' Get day data records with optional limit
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="limit">Optional limit for records (default 50)</param>
    ''' <returns>DataTable with day records</returns>
    Public Function GetDayData(comId As Integer, locId As Integer, Optional limit As Integer = 50) As DataTable
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            Dim postData As String = String.Format("DayCloseRequest=4&Comid={0}&Locid={1}&LIMIT={2}", comId, locId, limit)

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim response As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(response)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Dim jsonData As JArray = CType(jsonResponse("Data"), JArray)
                    Dim dt As New DataTable()

                    If jsonData.Count > 0 Then
                        ' Create columns from first record
                        For Each prop As JProperty In DirectCast(jsonData(0), JObject).Properties()
                            dt.Columns.Add(prop.Name, GetType(Object))
                        Next

                        ' Add rows
                        For Each item As JObject In jsonData
                            Dim row As DataRow = dt.NewRow()
                            For Each prop As JProperty In item.Properties()
                                row(prop.Name) = If(prop.Value.Type = JTokenType.Null, DBNull.Value, prop.Value.ToObject(Of Object)())
                            Next
                            dt.Rows.Add(row)
                        Next
                    End If

                    Return dt
                End If

                Return New DataTable()
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error getting day data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Validate day status before closing
    ''' </summary>
    ''' <param name="dayId">Day ID to validate</param>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <returns>Success status, can close flag, day state, day number and message</returns>
    Public Function ValidateDayBeforeClose(dayId As Integer, comId As Integer, locId As Integer) As Tuple(Of Boolean, Boolean, String, Integer, String)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            Dim postData As String = String.Format("DayCloseRequest=5&PSD_ID={0}&Comid={1}&Locid={2}", dayId, comId, locId)

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim response As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(response)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Dim canClose As Boolean = jsonResponse("CanClose").ToObject(Of Boolean)()
                    Dim dayState As String = jsonResponse("DayState").ToString()
                    Dim dayNo As Integer = jsonResponse("DayNo").ToObject(Of Integer)()
                    Dim message As String = jsonResponse("Msg").ToString()

                    Return Tuple.Create(True, canClose, dayState, dayNo, message)
                Else
                    Return Tuple.Create(False, False, "", 0, jsonResponse("Msg").ToString())
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error validating day: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, False, "", 0, "Error: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Validate current day and create if needed (similar to shift validation)
    ''' </summary>
    ''' <param name="comId">Company ID</param>
    ''' <param name="locId">Location ID</param>
    ''' <param name="userId">User ID</param>
    ''' <param name="pcName">PC Name</param>
    ''' <returns>Success status, action, message and result data</returns>
    Public Function ValidateCurrentDayAndCreate(comId As Integer, locId As Integer, userId As String, pcName As String) As Tuple(Of Boolean, String, String, Object)
        Try
            Dim url As String = functionModule.M_Details.LinkAjaxRequest

            ' Prepare POST data
            Dim postData As String = String.Format(
                "DayCloseRequest=6&Comid={0}&Locid={1}&USERID={2}&PCNAME={3}",
                comId, locId, Uri.EscapeDataString(userId), Uri.EscapeDataString(pcName)
            )

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim response As String = client.UploadString(url, postData)
                Dim jsonResponse As JObject = JObject.Parse(response)

                If jsonResponse("Success").ToObject(Of Boolean)() Then
                    Dim action As String = jsonResponse("Action").ToString()
                    Dim message As String = jsonResponse("Message").ToString()

                    ' Create result data object with day information
                    Dim resultData = New With {
                        .DayNo = If(jsonResponse("DayNo") IsNot Nothing, jsonResponse("DayNo").ToObject(Of Integer?)(), Nothing),
                        .ShiftNo = If(jsonResponse("ShiftNo") IsNot Nothing, jsonResponse("ShiftNo").ToObject(Of Integer?)(), Nothing),
                        .PM_ID = If(jsonResponse("PM_ID") IsNot Nothing, jsonResponse("PM_ID").ToObject(Of Integer?)(), Nothing),
                        .Data = If(jsonResponse("Data") IsNot Nothing, jsonResponse("Data").ToObject(Of Object)(), Nothing)
                    }

                    Return Tuple.Create(True, action, message, CType(resultData, Object))
                Else
                    Dim action As String = If(jsonResponse("Action") IsNot Nothing, jsonResponse("Action").ToString(), "error")
                    Dim message As String = jsonResponse("Msg").ToString()
                    Return Tuple.Create(False, action, message, CType(Nothing, Object))
                End If
            End Using

        Catch ex As Exception
            XtraMessageBox.Show("Error validating day status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Tuple.Create(False, "error", "Error: " & ex.Message, CType(Nothing, Object))
        End Try
    End Function
#End Region
End Module
Public Class SalesHeader
    Public Property psih_invoice_pmid As String
    Public Property psih_invoice_id As String
    Public Property psih_invoice_trno As String
    Public Property psih_invoice_date As String
    Public Property psih_invoice_prefix As String
    Public Property psih_invoice_tqty As Decimal
    Public Property psih_invoice_tamount As Decimal
    Public Property psih_invoice_titemdisper As Decimal
    Public Property psih_invoice_titemdisamt As String
    Public Property psih_invoice_tbilldiscper As Decimal
    Public Property psih_invoice_tbilldiscamt As Decimal
    Public Property psih_invoice_totdiscper As Decimal
    Public Property psih_invoice_totdiscamt As Decimal
    Public Property psih_invoice_tgrossamt As String
    Public Property psih_invoice_ttaxamt As Decimal
    Public Property psih_invoice_sercharge As Decimal
    Public Property psih_invoice_roundoff As Decimal
    Public Property psih_invoice_tnetamt As Decimal
    Public Property psih_invoice_saletype As String 'invoice or quotation
    Public Property psih_invoice_billtype As String 'Cash Bill,Credit Bill
    Public Property psih_invoice_billstatus As String
    Public Property psih_invoice_paymode As String 'cash,credit,card
    Public Property psih_invoice_customerid As String
    Public Property psih_invoice_description As String 'Store customername
    Public Property psih_invoice_userid As String
    Public Property psih_invoice_comid As String
    Public Property psih_invoice_locid As String
    Public Property psih_invoice_billremarks As String
    Public Property psih_invoice_advamt As Decimal
    Public Property psih_invoice_outstanding As Decimal
    Public Property psih_invoice_givenamt As Decimal
    Public Property psih_invoice_balamt As Decimal
    Public Property psih_invoice_shiftno As String
    Public Property psih_invoice_dayno As String
End Class
Public Class SalesDetails
    Public Property psid_invoice_id As String
    Public Property psid_invoice_salid As String
    Public Property psid_invoice_sno As String
    Public Property psid_invoice_date As DateTime
    Public Property psid_invoice_trno As String
    Public Property psid_invoice_barcode As String
    Public Property psid_invoice_procode As String
    Public Property psid_invoice_description As String
    Public Property psid_invoice_serialno As String
    Public Property psid_invoice_uom As String
    Public Property psid_invoice_proqty As Decimal
    Public Property psid_invoice_rate As Decimal
    Public Property psid_invoice_amt As Decimal
    Public Property psid_invoice_itemdisp As Decimal
    Public Property psid_invoice_itemdisamt As Decimal
    Public Property psid_invoice_billdisp As Decimal
    Public Property psid_invoice_billdisamt As Decimal
    Public Property psid_invoice_totdper As Decimal
    Public Property psid_invoice_totdamt As Decimal
    Public Property psid_invoice_gross As Decimal
    Public Property psid_invoice_taxinex As String
    Public Property psid_invoice_taxvalue As Decimal
    Public Property psid_invoice_taxamt As Decimal
    Public Property psid_invoice_netamt As Decimal
    Public Property psid_invoice_remarks As String
    Public Property psid_invoice_batchno As String
    Public Property psid_invoice_salesmanid As String
    Public Property psid_invoice_salemanper As Decimal
    Public Property psid_invoice_shiftno As String
    Public Property psid_invoice_dayno As String
    Public Sub New(dataRow As DataRow)
        psid_invoice_sno = If(dataRow("SNO") IsNot DBNull.Value, dataRow("SNO").ToString(), "")
        psid_invoice_barcode = If(dataRow("BARCODE") IsNot DBNull.Value, dataRow("BARCODE").ToString(), "")
        psid_invoice_procode = If(dataRow("ITEMCODE") IsNot DBNull.Value, dataRow("ITEMCODE").ToString(), "")
        psid_invoice_description = If(dataRow("ITEMNAME") IsNot DBNull.Value, dataRow("ITEMNAME").ToString(), "")
        psid_invoice_serialno = If(dataRow("SERIALNO") IsNot DBNull.Value, dataRow("SERIALNO").ToString(), "")
        psid_invoice_uom = If(dataRow("UOM") IsNot DBNull.Value, dataRow("UOM").ToString(), "")
        psid_invoice_proqty = If(dataRow("QTY") IsNot DBNull.Value, Convert.ToDecimal(dataRow("QTY")), 0)
        psid_invoice_rate = If(dataRow("RATE") IsNot DBNull.Value, Convert.ToDecimal(dataRow("RATE")), 0)
        psid_invoice_amt = If(dataRow("TAMOUNT") IsNot DBNull.Value, Convert.ToDecimal(dataRow("TAMOUNT")), 0)
        psid_invoice_itemdisp = If(dataRow("ITEM_DPER") IsNot DBNull.Value, Convert.ToDecimal(dataRow("ITEM_DPER")), 0)
        psid_invoice_itemdisamt = If(dataRow("ITEM_DAMT") IsNot DBNull.Value, Convert.ToDecimal(dataRow("ITEM_DAMT")), 0)
        psid_invoice_billdisp = If(dataRow("BILL_DPER") IsNot DBNull.Value, Convert.ToDecimal(dataRow("BILL_DPER")), 0)
        psid_invoice_billdisamt = If(dataRow("BILL_DAMT") IsNot DBNull.Value, Convert.ToDecimal(dataRow("BILL_DAMT")), 0)
        psid_invoice_totdper = If(dataRow("TOTAL_DPER") IsNot DBNull.Value, Convert.ToDecimal(dataRow("TOTAL_DPER")), 0)
        psid_invoice_totdamt = If(dataRow("TOTAL_DAMT") IsNot DBNull.Value, Convert.ToDecimal(dataRow("TOTAL_DAMT")), 0)
        psid_invoice_gross = If(dataRow("GAMOUNT") IsNot DBNull.Value, Convert.ToDecimal(dataRow("GAMOUNT")), 0)
        psid_invoice_taxvalue = If(dataRow("TAXVALUE") IsNot DBNull.Value, Convert.ToDecimal(dataRow("TAXVALUE")), 0)
        psid_invoice_taxamt = If(dataRow("TAXAMT") IsNot DBNull.Value, Convert.ToDecimal(dataRow("TAXAMT")), 0)
        psid_invoice_netamt = If(dataRow("NETAMT") IsNot DBNull.Value, Convert.ToDecimal(dataRow("NETAMT")), 0)
        psid_invoice_remarks = If(dataRow("ITEMREMARS") IsNot DBNull.Value, dataRow("ITEMREMARS").ToString(), "")
        psid_invoice_batchno = If(dataRow("BATCHNO") IsNot DBNull.Value, dataRow("BATCHNO").ToString(), "")
        psid_invoice_salesmanid = If(dataRow("SALESPERSONID") IsNot DBNull.Value, dataRow("SALESPERSONID").ToString(), "")
        psid_invoice_salemanper = If(dataRow("SALESMANPER") IsNot DBNull.Value, Convert.ToDecimal(dataRow("SALESMANPER")), 0)
    End Sub
End Class
'TRUNCATE TABLE pos_sale_invoicehdr;
'TRUNCATE TABLE pos_sale_invoicedtl;
'TRUNCATE TABLE journaldetails;
