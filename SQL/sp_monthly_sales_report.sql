DELIMITER $$

DROP PROCEDURE IF EXISTS sp_monthly_sales_report $$
CREATE PROCEDURE sp_monthly_sales_report(
    IN pYear INT,
    IN pMonth INT,
    IN pComId INT,
    IN pLocId INT
)
BEGIN
    DECLARE vMonYear VARCHAR(10);
    DECLARE vFromDate DATE;
    DECLARE vToDate DATE;

    SET vFromDate = STR_TO_DATE(CONCAT(pYear, '-', LPAD(pMonth, 2, '0'), '-01'), '%Y-%m-%d');
    SET vToDate = DATE_ADD(vFromDate, INTERVAL 1 MONTH);
    SET vMonYear = DATE_FORMAT(vFromDate, '%b-%Y');

    -- 1) Sales by Salesman / Branch (SalesmanData)
    SELECT
        vMonYear AS MonYear,
        pe.emp_id AS EmpID,
        pe.emp_printname AS Salesman,
        pm.pcm_name AS CompanyName,
        pl.plm_name AS LocationName,
        CAST(SUM(psid.psid_invoice_netamt) AS DECIMAL(18,2)) AS NetAmt,
      CAST(SUM(psid.psid_invoice_netamt * psid.psid_invoice_salemanper / 100) AS DECIMAL(18,2)) AS Commission,
      CAST(SUM(psid.psid_invoice_proqty) AS DECIMAL(18,2)) AS Qty
    FROM pos_sale_invoicedtl AS psid
    INNER JOIN pos_sale_invoicehdr AS psih
        ON psih.psih_invoice_trno = psid.psid_invoice_trno
       AND psih.psih_invoice_billstatus = 'Closed'
       AND psih.psih_invoice_comid = psid.psid_invoice_comid
       AND psih.psih_invoice_locid = psid.psid_invoice_locid
    INNER JOIN pos_employeeinfo AS pe ON pe.emp_id = psid.psid_invoice_salesmanid
    INNER JOIN pos_company_mast AS pm ON pm.pcm_id = psid.psid_invoice_comid
    INNER JOIN pos_location_mast AS pl ON pl.plm_id = psid.psid_invoice_locid
    WHERE psid.psid_invoice_date >= vFromDate
      AND psid.psid_invoice_date < vToDate
      AND (pComId = 0 OR psid.psid_invoice_comid = pComId)
      AND (pLocId = 0 OR psid.psid_invoice_locid = pLocId)
    GROUP BY pe.emp_id, pe.emp_printname, pm.pcm_name, pl.plm_name
    ORDER BY NetAmt DESC, pe.emp_printname;

    -- 2) Itemwise Sales & Commission (ItemwiseData)
    SELECT
        vMonYear AS MonYear,
        pe.emp_id AS EmpID,
        pe.emp_printname AS Salesman,
        psid.psid_invoice_description AS ItemName,
        pm.pcm_name AS CompanyName,
        pl.plm_name AS LocationName,
        CAST(SUM(psid.psid_invoice_netamt) AS DECIMAL(18,2)) AS NetAmt,
      CAST(SUM(psid.psid_invoice_netamt * psid.psid_invoice_salemanper / 100) AS DECIMAL(18,2)) AS Commission,
      CAST(SUM(psid.psid_invoice_proqty) AS DECIMAL(18,2)) AS Qty
    FROM pos_sale_invoicedtl AS psid
    INNER JOIN pos_sale_invoicehdr AS psih
        ON psih.psih_invoice_trno = psid.psid_invoice_trno
       AND psih.psih_invoice_billstatus = 'Closed'
       AND psih.psih_invoice_comid = psid.psid_invoice_comid
       AND psih.psih_invoice_locid = psid.psid_invoice_locid
    INNER JOIN pos_employeeinfo AS pe ON pe.emp_id = psid.psid_invoice_salesmanid
    INNER JOIN pos_company_mast AS pm ON pm.pcm_id = psid.psid_invoice_comid
    INNER JOIN pos_location_mast AS pl ON pl.plm_id = psid.psid_invoice_locid
    WHERE psid.psid_invoice_date >= vFromDate
      AND psid.psid_invoice_date < vToDate
      AND (pComId = 0 OR psid.psid_invoice_comid = pComId)
      AND (pLocId = 0 OR psid.psid_invoice_locid = pLocId)
    GROUP BY psid.psid_invoice_description, pe.emp_id, pe.emp_printname, pm.pcm_name, pl.plm_name
    ORDER BY NetAmt DESC, psid.psid_invoice_description;

    -- 3) Advance Payments Summary (AdvanceData)
    SELECT
        vMonYear AS MonYear,
        pe.emp_id AS EmpID,
        pe.emp_printname AS Salesman,
        pm.pcm_name AS CompanyName,
        pl.plm_name AS LocationName,
        CAST(SUM(ppd.payd_amount) AS DECIMAL(18,2)) AS TotalAdvance
    FROM pos_payout_dtl AS ppd
    INNER JOIN pos_employeeinfo AS pe ON ppd.payd_ledgerid = pe.emp_id
    INNER JOIN pos_company_mast AS pm ON pm.pcm_id = ppd.ComId
    INNER JOIN pos_location_mast AS pl ON pl.plm_id = ppd.LocId
    WHERE ppd.payd_datetime >= vFromDate
      AND ppd.payd_datetime < vToDate
      AND (pComId = 0 OR ppd.ComId = pComId)
      AND (pLocId = 0 OR ppd.LocId = pLocId)
    GROUP BY pe.emp_id, pe.emp_printname, pm.pcm_name, pl.plm_name
    ORDER BY TotalAdvance DESC, pe.emp_printname;

    -- 4) Advance Payments Details (AdvanceDataDtl)
    SELECT
        vMonYear AS MonYear,
        ppd.payd_id AS Id,
        ppd.payd_refid AS RefId,
        ppd.payd_ledgerid AS EmpID,
        pe.emp_printname AS Salesman,
        CAST(ppd.payd_amount AS DECIMAL(18,2)) AS Amount,
        ppd.payd_remarks AS Remarks,
        ppd.payd_shiftno AS ShiftNo,
        ppd.payd_dayno AS DayNo,
        ppd.payd_user AS Users,
        ppd.payd_datetime AS DateTime,
        ppd.PmId,
        ppd.ComId,
        ppd.LocId,
        pm.pcm_name AS CompanyName,
        pl.plm_name AS LocationName
    FROM pos_payout_dtl AS ppd
    INNER JOIN pos_employeeinfo AS pe ON ppd.payd_ledgerid = pe.emp_id
    INNER JOIN pos_company_mast AS pm ON pm.pcm_id = ppd.ComId
    INNER JOIN pos_location_mast AS pl ON pl.plm_id = ppd.LocId
    WHERE ppd.payd_datetime >= vFromDate
      AND ppd.payd_datetime < vToDate
      AND (pComId = 0 OR ppd.ComId = pComId)
      AND (pLocId = 0 OR ppd.LocId = pLocId)
    ORDER BY ppd.payd_datetime DESC, pe.emp_printname;
END $$

DELIMITER ;
