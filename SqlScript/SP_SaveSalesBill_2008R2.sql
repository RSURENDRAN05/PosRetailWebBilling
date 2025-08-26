USE [MrestbillingV23]
GO

/****** Object:  StoredProcedure [dbo].[SP_SaveSalesHeader]    Script Date: 8/26/2025 ******/
-- Drop existing procedures if they exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SaveSalesHeader]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_SaveSalesHeader]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Save Sales Header - SQL Server 2008 R2 Compatible
-- =============================================
CREATE PROCEDURE [dbo].[SP_SaveSalesHeader]
    @psih_invoice_pmid INT = 1,
    @psih_invoice_date DATE,
    @psih_invoice_prefix VARCHAR(20) = 'INV',
    @psih_invoice_tqty DECIMAL(18,2) = 0,
    @psih_invoice_tamount DECIMAL(18,2) = 0,
    @psih_invoice_titemdisper DECIMAL(18,2) = 0,
    @psih_invoice_titemdisamt DECIMAL(18,2) = 0,
    @psih_invoice_tbilldiscper DECIMAL(18,2) = 0,
    @psih_invoice_tbilldiscamt DECIMAL(18,2) = 0,
    @psih_invoice_totdiscper DECIMAL(18,2) = 0,
    @psih_invoice_totdiscamt DECIMAL(18,2) = 0,
    @psih_invoice_tgrossamt DECIMAL(18,2) = 0,
    @psih_invoice_ttaxamt DECIMAL(18,2) = 0,
    @psih_invoice_sercharge DECIMAL(18,2) = 0,
    @psih_invoice_roundoff DECIMAL(18,2) = 0,
    @psih_invoice_tnetamt DECIMAL(18,2) = 0,
    @psih_invoice_saletype VARCHAR(20) = 'Invoice',
    @psih_invoice_billtype VARCHAR(20) = 'Cash Bill',
    @psih_invoice_billstatus VARCHAR(20) = 'Closed',
    @psih_invoice_paymode VARCHAR(25) = 'cash',
    @psih_invoice_customerid VARCHAR(20) = '1',
    @psih_invoice_description VARCHAR(200) = '',
    @psih_invoice_countername VARCHAR(30) = '',
    @psih_invoice_userid INT = 1,
    @psih_invoice_comid INT = 1,
    @psih_invoice_locid INT = 1,
    @psih_invoice_print INT = 0,
    @psih_invoice_billremarks VARCHAR(300) = '',
    @psih_invoice_advamt DECIMAL(18,2) = 0,
    @psih_invoice_outstanding DECIMAL(18,2) = 0,
    @psih_invoice_givenamt DECIMAL(18,2) = 0,
    @psih_invoice_balamt DECIMAL(18,2) = 0,
    @psih_invoice_shiftno INT = 1,
    @psih_invoice_dayno INT = 1,
    @psih_invoice_webhost INT = 0,
    @psih_invoice_trno INT = 0 OUTPUT,
    @psih_invoice_id INT = 0 OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorMessage NVARCHAR(4000)

    BEGIN TRY
        BEGIN TRANSACTION

        -- Generate Transaction Number if not provided
        IF @psih_invoice_trno = 0 OR @psih_invoice_trno IS NULL
        BEGIN
            -- Use a single atomic operation to get next transaction number
            UPDATE POS_MASTER
            SET PM_TRANS_NO = PM_TRANS_NO + 1

            -- Get the updated transaction number
            SELECT @psih_invoice_trno = PM_TRANS_NO FROM POS_MASTER

            -- Ensure we have a valid transaction number
            IF @psih_invoice_trno IS NULL OR @psih_invoice_trno = 0
            BEGIN
                -- Initialize POS_MASTER if it doesn't exist or is null
                IF NOT EXISTS (SELECT 1 FROM POS_MASTER)
                BEGIN
                    INSERT INTO POS_MASTER (PM_TRANS_NO) VALUES (1)
                    SET @psih_invoice_trno = 1
                END
                ELSE
                BEGIN
                    UPDATE POS_MASTER SET PM_TRANS_NO = 1
                    SET @psih_invoice_trno = 1
                END
            END
        END

        -- Insert Header Record
        INSERT INTO [dbo].[pos_sale_invoicehdr]
        (
            [psih_invoice_trno], [psih_invoice_date], [psih_invoice_prefix],
            [psih_invoice_tqty], [psih_invoice_tamount], [psih_invoice_titemdisper],
            [psih_invoice_titemdisamt], [psih_invoice_tbilldiscper], [psih_invoice_tbilldiscamt],
            [psih_invoice_totdiscper], [psih_invoice_totdiscamt], [psih_invoice_tgrossamt],
            [psih_invoice_ttaxamt], [psih_invoice_sercharge], [psih_invoice_roundoff],
            [psih_invoice_tnetamt], [psih_invoice_saletype], [psih_invoice_billtype],
            [psih_invoice_billstatus], [psih_invoice_paymode], [psih_invoice_customerid],
            [psih_invoice_description], [psih_invoice_countername], [psih_invoice_userid],
            [psih_invoice_comid], [psih_invoice_locid],[psih_invoice_pmid],[psih_invoice_print],
            [psih_invoice_billremarks], [psih_invoice_advamt], [psih_invoice_outstanding],
            [psih_invoice_givenamt], [psih_invoice_balamt], [psih_invoice_shiftno],
            [psih_invoice_dayno], [psih_invoice_webhost], [psih_invoice_created], [psih_invoice_modified]
        )
        VALUES
        (
            @psih_invoice_trno, @psih_invoice_date, @psih_invoice_prefix,
            @psih_invoice_tqty, @psih_invoice_tamount, @psih_invoice_titemdisper,
            @psih_invoice_titemdisamt, @psih_invoice_tbilldiscper, @psih_invoice_tbilldiscamt,
            @psih_invoice_totdiscper, @psih_invoice_totdiscamt, @psih_invoice_tgrossamt,
            @psih_invoice_ttaxamt, @psih_invoice_sercharge, @psih_invoice_roundoff,
            @psih_invoice_tnetamt, @psih_invoice_saletype, @psih_invoice_billtype,
            @psih_invoice_billstatus, @psih_invoice_paymode, @psih_invoice_customerid,
            @psih_invoice_description, @psih_invoice_countername, @psih_invoice_userid,
            @psih_invoice_comid, @psih_invoice_locid,@psih_invoice_pmid,@psih_invoice_print,
            @psih_invoice_billremarks, @psih_invoice_advamt, @psih_invoice_outstanding,
            @psih_invoice_givenamt, @psih_invoice_balamt, @psih_invoice_shiftno,
            @psih_invoice_dayno, @psih_invoice_webhost, GETDATE(), GETDATE()
        )

        SET @psih_invoice_id = SCOPE_IDENTITY()

        COMMIT TRANSACTION

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION

        SET @ErrorMessage = ERROR_MESSAGE()
        RAISERROR (@ErrorMessage, 16, 1)

    END CATCH
END
GO

-- =============================================
-- Save Sales Detail
-- =============================================
-- Drop existing procedure if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SaveSalesDetail]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_SaveSalesDetail]
GO

CREATE PROCEDURE [dbo].[SP_SaveSalesDetail]
    @psid_invoice_salid INT,
    @psid_invoice_sno INT,
    @psid_invoice_date DATE,
    @psid_invoice_trno INT,
    @psid_invoice_barcode VARCHAR(50) = '',
    @psid_invoice_procode INT,
    @psid_invoice_description VARCHAR(300) = '',
    @psid_invoice_serialno VARCHAR(50) = '',
    @psid_invoice_uom VARCHAR(20) = '',
    @psid_invoice_proqty DECIMAL(18,2) = 0,
    @psid_invoice_rate DECIMAL(18,2) = 0,
    @psid_invoice_amt DECIMAL(18,2) = 0,
    @psid_invoice_itemdisp DECIMAL(18,2) = 0,
    @psid_invoice_itemdisamt DECIMAL(18,2) = 0,
    @psid_invoice_billdisp DECIMAL(18,2) = 0,
    @psid_invoice_billdisamt DECIMAL(18,2) = 0,
    @psid_invoice_totdper DECIMAL(18,2) = 0,
    @psid_invoice_totdamt DECIMAL(18,2) = 0,
    @psid_invoice_gross DECIMAL(18,2) = 0,
    @psid_invoice_taxinex INT = 0,
    @psid_invoice_taxvalue DECIMAL(18,2) = 0,
    @psid_invoice_taxamt DECIMAL(18,2) = 0,
    @psid_invoice_netamt DECIMAL(18,2) = 0,
    @psid_invoice_remarks VARCHAR(100) = '',
    @psid_invoice_batchno VARCHAR(50) = '',
    @psid_invoice_salesmanid INT = 0,
    @psid_invoice_salemanper DECIMAL(18,2) = 0,
    @psid_invoice_shiftno INT = 1,
    @psid_invoice_dayno INT = 1,
    @psid_invoice_webhost INT = 0,
    @psid_invoice_id INT = 0 OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorMessage NVARCHAR(4000)

    BEGIN TRY
        INSERT INTO [dbo].[pos_sale_invoicedtl]
        (
            [psid_invoice_salid], [psid_invoice_sno], [psid_invoice_date], [psid_invoice_trno],
            [psid_invoice_barcode], [psid_invoice_procode], [psid_invoice_description], [psid_invoice_serialno],
            [psid_invoice_uom], [psid_invoice_proqty], [psid_invoice_rate], [psid_invoice_amt],
            [psid_invoice_itemdisp], [psid_invoice_itemdisamt], [psid_invoice_billdisp], [psid_invoice_billdisamt],
            [psid_invoice_totdper], [psid_invoice_totdamt], [psid_invoice_gross], [psid_invoice_taxinex],
            [psid_invoice_taxvalue], [psid_invoice_taxamt], [psid_invoice_netamt], [psid_invoice_remarks],
            [psid_invoice_batchno], [psid_invoice_salesmanid], [psid_invoice_salemanper], [psid_invoice_shiftno],
            [psid_invoice_dayno], [psid_invoice_webhost], [psid_invoice_created], [psid_invoice_modified]
        )
        VALUES
        (
            @psid_invoice_salid, @psid_invoice_sno, @psid_invoice_date, @psid_invoice_trno,
            @psid_invoice_barcode, @psid_invoice_procode, @psid_invoice_description, @psid_invoice_serialno,
            @psid_invoice_uom, @psid_invoice_proqty, @psid_invoice_rate, @psid_invoice_amt,
            @psid_invoice_itemdisp, @psid_invoice_itemdisamt, @psid_invoice_billdisp, @psid_invoice_billdisamt,
            @psid_invoice_totdper, @psid_invoice_totdamt, @psid_invoice_gross, @psid_invoice_taxinex,
            @psid_invoice_taxvalue, @psid_invoice_taxamt, @psid_invoice_netamt, @psid_invoice_remarks,
            @psid_invoice_batchno, @psid_invoice_salesmanid, @psid_invoice_salemanper, @psid_invoice_shiftno,
            @psid_invoice_dayno, @psid_invoice_webhost, GETDATE(), GETDATE()
        )

        SET @psid_invoice_id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE()
        RAISERROR (@ErrorMessage, 16, 1)
    END CATCH
END
GO

-- =============================================
-- Update Sales Header (for Edit mode)
-- =============================================
-- Drop existing procedure if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateSalesHeader]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_UpdateSalesHeader]
GO

CREATE PROCEDURE [dbo].[SP_UpdateSalesHeader]
    @psih_invoice_id INT,
    @psih_invoice_tqty DECIMAL(18,2) = 0,
    @psih_invoice_tamount DECIMAL(18,2) = 0,
    @psih_invoice_titemdisper DECIMAL(18,2) = 0,
    @psih_invoice_titemdisamt DECIMAL(18,2) = 0,
    @psih_invoice_tbilldiscper DECIMAL(18,2) = 0,
    @psih_invoice_tbilldiscamt DECIMAL(18,2) = 0,
    @psih_invoice_totdiscper DECIMAL(18,2) = 0,
    @psih_invoice_totdiscamt DECIMAL(18,2) = 0,
    @psih_invoice_tgrossamt DECIMAL(18,2) = 0,
    @psih_invoice_ttaxamt DECIMAL(18,2) = 0,
    @psih_invoice_sercharge DECIMAL(18,2) = 0,
    @psih_invoice_roundoff DECIMAL(18,2) = 0,
    @psih_invoice_tnetamt DECIMAL(18,2) = 0,
    @psih_invoice_billtype VARCHAR(20) = 'Cash Bill',
    @psih_invoice_billstatus VARCHAR(20) = 'Closed',
    @psih_invoice_paymode VARCHAR(25) = 'cash',
    @psih_invoice_customerid VARCHAR(20) = '1',
    @psih_invoice_description VARCHAR(200) = '',
    @psih_invoice_billremarks VARCHAR(300) = '',
    @psih_invoice_advamt DECIMAL(18,2) = 0,
    @psih_invoice_outstanding DECIMAL(18,2) = 0,
    @psih_invoice_givenamt DECIMAL(18,2) = 0,
    @psih_invoice_balamt DECIMAL(18,2) = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorMessage NVARCHAR(4000)

    BEGIN TRY
        UPDATE [dbo].[pos_sale_invoicehdr]
        SET
            [psih_invoice_tqty] = @psih_invoice_tqty,
            [psih_invoice_tamount] = @psih_invoice_tamount,
            [psih_invoice_titemdisper] = @psih_invoice_titemdisper,
            [psih_invoice_titemdisamt] = @psih_invoice_titemdisamt,
            [psih_invoice_tbilldiscper] = @psih_invoice_tbilldiscper,
            [psih_invoice_tbilldiscamt] = @psih_invoice_tbilldiscamt,
            [psih_invoice_totdiscper] = @psih_invoice_totdiscper,
            [psih_invoice_totdiscamt] = @psih_invoice_totdiscamt,
            [psih_invoice_tgrossamt] = @psih_invoice_tgrossamt,
            [psih_invoice_ttaxamt] = @psih_invoice_ttaxamt,
            [psih_invoice_sercharge] = @psih_invoice_sercharge,
            [psih_invoice_roundoff] = @psih_invoice_roundoff,
            [psih_invoice_tnetamt] = @psih_invoice_tnetamt,
            [psih_invoice_billtype] = @psih_invoice_billtype,
            [psih_invoice_billstatus] = @psih_invoice_billstatus,
            [psih_invoice_paymode] = @psih_invoice_paymode,
            [psih_invoice_customerid] = @psih_invoice_customerid,
            [psih_invoice_description] = @psih_invoice_description,
            [psih_invoice_billremarks] = @psih_invoice_billremarks,
            [psih_invoice_advamt] = @psih_invoice_advamt,
            [psih_invoice_outstanding] = @psih_invoice_outstanding,
            [psih_invoice_givenamt] = @psih_invoice_givenamt,
            [psih_invoice_balamt] = @psih_invoice_balamt,
            [psih_invoice_modified] = GETDATE()
        WHERE [psih_invoice_id] = @psih_invoice_id

    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE()
        RAISERROR (@ErrorMessage, 16, 1)
    END CATCH
END
GO

-- =============================================
-- Save Payment Mode Details
-- =============================================
-- Drop existing procedure if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SavePaymentMode]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[SP_SavePaymentMode]
GO

CREATE PROCEDURE [dbo].[SP_SavePaymentMode]
    @Sal_ID INT,
    @Paymode INT,
    @Amount DECIMAL(18,2),
    @ShiftNo INT = 1,
    @Dayno INT = 1
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorMessage NVARCHAR(4000)

    BEGIN TRY
        -- Delete existing payment modes for this sale first (for updates)
        DELETE FROM [dbo].[Sal_PayMode] WHERE [Sal_ID] = @Sal_ID

        -- Insert new payment mode record
        INSERT INTO [dbo].[Sal_PayMode]
        (
            [Sal_ID],
            [Paymode],
            [Amount],
            [ShiftNo],
            [Dayno],
            [Created]
        )
        VALUES
        (
            @Sal_ID,
            @Paymode,
            @Amount,
            @ShiftNo,
            @Dayno,
            GETDATE()
        )

    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE()
        RAISERROR (@ErrorMessage, 16, 1)
    END CATCH
END
GO
