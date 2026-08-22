USE [MrestbillingV23]
GO

-- =============================================
-- pos_voucher_usage – local audit/queue table for voucher redemptions
-- One row per voucher applied to a successfully-paid bill.
-- vu_pushstatus tracks whether the row has been confirmed to the cloud
-- (AjaxRequest=92 -> voucher_sales), so a voucher can't be redeemed twice.
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pos_voucher_usage]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[pos_voucher_usage] (
        vu_id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        vu_voucherid   INT NOT NULL DEFAULT 0,
        vu_voucherno   INT NOT NULL DEFAULT 0,
        vu_vouchercode VARCHAR(30) NOT NULL DEFAULT '',    -- full code as typed, e.g. 'A1' (prefix + number)
        vu_billno      VARCHAR(50) NOT NULL DEFAULT '0',   -- reference number = the bill's trno
        vu_billamount  DECIMAL(18,2) NOT NULL DEFAULT 0,   -- bill's net amount AFTER discount
        vu_comid       INT NOT NULL DEFAULT 0,
        vu_locid       INT NOT NULL DEFAULT 0,
        vu_shiftno     INT NOT NULL DEFAULT 0,
        vu_dayno       INT NOT NULL DEFAULT 0,
        vu_pushstatus  INT NOT NULL DEFAULT 0,              -- 0 = pending push to cloud, 1 = pushed/confirmed
        vu_created     DATETIME NOT NULL DEFAULT GETDATE(),
        vu_pushed      DATETIME NULL
    )
    CREATE INDEX IX_pos_voucher_usage_voucherno ON [dbo].[pos_voucher_usage] (vu_voucherid, vu_voucherno)
    CREATE INDEX IX_pos_voucher_usage_pushstatus ON [dbo].[pos_voucher_usage] (vu_pushstatus)
END
GO

-- Table already existed on your DB before vu_vouchercode was added - add it if missing.
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[pos_voucher_usage]') AND name = 'vu_vouchercode')
BEGIN
    ALTER TABLE [dbo].[pos_voucher_usage] ADD vu_vouchercode VARCHAR(30) NOT NULL DEFAULT ''
END
GO

/****** Object:  StoredProcedure [dbo].[sp_voucherusage] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_voucherusage]') AND type IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_voucherusage]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- sp_voucherusage - SQL Server 2008 R2 Compatible
-- @mode = 'I' Insert a new local usage record (called right after a bill saves OK)
-- @mode = 'S' Select rows still pending push to the cloud (used by the auto-sync timer)
-- @mode = 'U' Mark a row as pushed/confirmed after AjaxRequest=92 succeeds
-- @mode = 'R' Report - all usage rows between @vu_fromdate and @vu_todate (used by
--             frmVoucherUsageReport, the "Voucher Usage" screen under Voucher Usage report)
-- =============================================
CREATE PROCEDURE [dbo].[sp_voucherusage]
    @mode          VARCHAR(10),
    @vu_id         INT = 0,
    @vu_voucherid  INT = 0,
    @vu_voucherno  INT = 0,
    @vu_vouchercode VARCHAR(30) = '',
    @vu_billno     VARCHAR(50) = '0',
    @vu_billamount DECIMAL(18,2) = 0,
    @vu_comid      INT = 0,
    @vu_locid      INT = 0,
    @vu_shiftno    INT = 0,
    @vu_dayno      INT = 0,
    @vu_fromdate   DATE = NULL,
    @vu_todate     DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @mode = 'I'
    BEGIN
        INSERT INTO [dbo].[pos_voucher_usage]
            (vu_voucherid, vu_voucherno, vu_vouchercode, vu_billno, vu_billamount, vu_comid, vu_locid, vu_shiftno, vu_dayno, vu_pushstatus, vu_created)
        VALUES
            (@vu_voucherid, @vu_voucherno, @vu_vouchercode, @vu_billno, @vu_billamount, @vu_comid, @vu_locid, @vu_shiftno, @vu_dayno, 0, GETDATE())

        SELECT SCOPE_IDENTITY() AS vu_id
    END
    ELSE IF @mode = 'S'
    BEGIN
        SELECT vu_id, vu_voucherid, vu_voucherno, vu_vouchercode, vu_billno, vu_billamount,
               vu_comid, vu_locid, vu_shiftno, vu_dayno, vu_created
        FROM   [dbo].[pos_voucher_usage]
        WHERE  vu_pushstatus = 0
        ORDER BY vu_id
    END
    ELSE IF @mode = 'U'
    BEGIN
        UPDATE [dbo].[pos_voucher_usage]
        SET    vu_pushstatus = 1,
               vu_pushed = GETDATE()
        WHERE  vu_id = @vu_id
    END
    ELSE IF @mode = 'R'
    BEGIN
        SELECT vu_id, vu_voucherid, vu_voucherno, vu_vouchercode, vu_billno, vu_billamount,
               vu_comid, vu_locid, vu_shiftno, vu_dayno, vu_pushstatus, vu_created
        FROM   [dbo].[pos_voucher_usage]
        WHERE  (@vu_fromdate IS NULL OR vu_created >= @vu_fromdate)
          AND  (@vu_todate   IS NULL OR vu_created <  DATEADD(DAY, 1, @vu_todate))
        ORDER BY vu_created DESC
    END
END
GO
