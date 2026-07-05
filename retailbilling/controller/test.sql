
SELECT 
    pef.`pef_id` AS EmpTrId,
    pef.`pef_refid` AS EmpRefId,
    pe.`emp_printname` AS EmpName,
    pef.`pef_comid` AS EmpComId,
    pcm.`pcm_name` AS EmpComName,
    pef.`pef_locid` AS EmpLocId,
    plm.`plm_name` AS EmpLocName,
    pef.`pef_month` AS EmpMonth,
    pef.`pef_basicsalary` AS EmpBasic,
    pef.`pef_workingdays` AS EmpNoOfDays,
    pef.`pef_wages` AS EmpWages,
    pef.`pef_extraday` AS EmpExtraDays,
    pef.`pef_extradayamt` AS EmpExtraDayAmt,
    pef.`pef_extrahours` AS EmpExtraOtHrs,
    pef.`pef_extrahrsamt` AS EmpExtraOtAmt,
    pef.`pef_allowance` AS EmpAllowance,
    pef.`pef_grossamt` AS EmpGrossAmt,
    pef.`pef_advance` AS EmpAdvance,
    pef.`pef_epf` AS EmpEpf,
    pef.`pef_socso` AS EmpSocso,
    pef.`pef_deduction` AS EmpDeduction,
    pef.`pef_netpay` AS EmpNetPay,
    pef.`pef_bank` AS EmpBank,
    pef.`pef_netcash` AS EmpNetCash
FROM 
    `pos_emp_finalprocess` AS pef
INNER JOIN  
    `pos_employeeinfo` AS pe ON pe.`emp_id` = pef.`pef_refid`
INNER JOIN 
    `pos_company_mast` AS pcm ON pcm.`pcm_id` = pef.`pef_comid`
INNER JOIN 
    `pos_location_mast` AS plm ON plm.`plm_id` = pef.`pef_locid`
WHERE   
    pef.`pef_month` = 'Nov-2025' 
    AND pef.`pef_comid` = '1' 
    AND pef.`pef_locid` = '2';


SELECT 
    pem.`pemp_id` AS EmpTrId,
    pem.`pemp_refid` AS EmpRefId,
    pe.`emp_printname` AS EmpName,
    pem.`pemp_month` AS EmpMonth,
    pem.`pemp_comid` AS EmpComId,
    pcm.`pcm_name` AS EmpComName,
    pem.`pemp_locid` AS EmpLocId,
    plm.`plm_name` AS EmpLocName,
    pe.`emp_basicsalary` AS EmpBasic,
    pe.`emp_basicrate as EmpBasicRate,
    pe.`emp_otrate` AS EmpOtRate,
    pe.`emp_othrsrate` AS EmpOtHrsRate,
    pe.`emp_allowance` AS EmpAllowance,
    pe.`emp_epfpercent` AS EmpEpf,
    pe.`emp_socsopercent` AS EmpSocso,
    pem.`pemp_noofdays` AS EmpNoOfDays,
    pem.`pemp_extradays` AS EmpExtraDays,
    pem.`pemp_extrahrs` AS EmpExtraOtHrs,
    pem.`pemp_advance` AS EmpAdvance,
    pem.`pemp_deduction` AS EmpDeduction,
    pem.`pemp_bankin` AS EmpBankIn
FROM     `pos_emp_monthprocess` AS pem 
INNER JOIN 
    `pos_employeeinfo` AS pe ON pe.`emp_id` = pem.`pemp_refid`
INNER JOIN 
    `pos_company_mast` AS pcm ON pem.`pemp_comid` = pcm.`pcm_id`
INNER JOIN 
    `pos_location_mast` AS plm ON pem.`pemp_locid` = plm.`plm_id`
WHERE 
    pem.`pemp_comid` = '1'
    AND pem.`pemp_locid` = '2'
    AND pe.`emp_active` = 1
    AND pem.`pemp_month` = 'Nov-2025';



    DELIMITER $$
CREATE DEFINER=`myposqrc`@`localhost` PROCEDURE `sp_modifysalesreport`(IN `pMode` VARCHAR(20), IN `pFromDate` DATETIME, IN `pToDate` DATETIME, IN `pNoOfRows` INT, IN `pBillNo` INT, IN `pComId` INT, IN `pLocId` INT)
proc_end: BEGIN
    DECLARE vTaxInEx INT DEFAULT 0;
    DECLARE vInvCreated DATETIME;
    DECLARE vFrom DATETIME;
    DECLARE vTo   DATETIME;

    -- Rollback on any SQL error
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    -- Normalize range: FromDate 00:00:00 (inclusive) up to ToDate+1 day (exclusive)
    SET vFrom = DATE(pFromDate);
    SET vTo   = DATE(pToDate) + INTERVAL 1 DAY;

    IF pMode = 'Header' THEN

        SELECT
            psih_tax_trno AS RowId,
            psih_tax_modified AS `Date`,
            psih_tax_trno AS Trno,
            psih_tax_billtype AS Payment,
            psih_tax_tnetamt AS NetAmt,
            CASE psih_tax_print
                WHEN 0 THEN 'No Print'
                WHEN 1 THEN 'Printed'
                ELSE NULL
            END AS PrintStatus,
            'No' AS CanProcess
        FROM pos_tax_invoicehdr
        WHERE psih_tax_comid = pComId
          AND psih_tax_locid = pLocId
          AND psih_tax_modified >= vFrom
          AND psih_tax_modified <  vTo;

    ELSEIF pMode = 'Detail' THEN

        SELECT
            psid_tax_trno AS RowId,
            psid_tax_modified AS `Date`,
            psid_tax_trno AS Trno,
            psid_tax_description AS ItemName,
            psid_tax_proqty AS Qty,
            psid_tax_netamt AS NetAmt
        FROM pos_tax_invoicedtl
        WHERE psid_tax_comid = pComId
          AND psid_tax_locid = pLocId
          AND psid_tax_trno = pBillNo;

    ELSEIF pMode = 'DetNetAmt' THEN

        SELECT
            IFNULL(SUM(psid_tax_netamt), 0.00) AS NetAmt
        FROM pos_tax_invoicedtl
        WHERE psid_tax_comid = pComId
          AND psid_tax_locid = pLocId
          AND psid_tax_modified >= vFrom
          AND psid_tax_modified <  vTo;

    ELSEIF pMode = 'HeadNetAmt' THEN

        SELECT
            IFNULL(SUM(psih_tax_tnetamt), 0.00) AS NetAmt
        FROM pos_tax_invoicehdr
        WHERE psih_tax_comid = pComId
          AND psih_tax_locid = pLocId
          AND psih_tax_modified >= vFrom
          AND psih_tax_modified <  vTo;

    ELSEIF pMode = 'Process' THEN

        IF NOT EXISTS (
            SELECT 1
            FROM pos_tax_invoicehdr
            WHERE psih_tax_comid = pComId
              AND psih_tax_locid = pLocId
              AND psih_tax_trno = pBillNo
              AND psih_tax_paymode = 'CASH'
              AND psih_tax_print = 0
            LIMIT 1
        ) THEN
            LEAVE proc_end;
        END IF;

        START TRANSACTION;
       
        DELETE FROM pos_tax_paymode
        WHERE ComId = pComId
          AND LocId = pLocId
          AND Sal_Id = pBillNo;
          
        DELETE d
        FROM pos_tax_invoicedtl d
        JOIN (
            SELECT psid_tax_id
            FROM (
                SELECT
                    psid_tax_id,
                    ROW_NUMBER() OVER (ORDER BY psid_tax_id) AS rn
                FROM pos_tax_invoicedtl
                WHERE psid_tax_comid = pComId
                  AND psid_tax_locid = pLocId
                  AND psid_tax_trno = pBillNo
            ) x
            WHERE x.rn > pNoOfRows
        ) z ON z.psid_tax_id = d.psid_tax_id;

        UPDATE pos_tax_invoicehdr h
        JOIN (
            SELECT
                psid_tax_trno,
                IFNULL(SUM(psid_tax_amt), 0.00)        AS sum_amt,
                IFNULL(SUM(psid_tax_itemdisamt), 0.00) AS sum_itemdisamt,
                IFNULL(SUM(psid_tax_gross), 0.00)      AS sum_gross,
                IFNULL(SUM(psid_tax_taxamt), 0.00)     AS sum_tax,
                IFNULL(SUM(psid_tax_netamt), 0.00)     AS sum_net
            FROM pos_tax_invoicedtl
            WHERE psid_tax_comid = pComId
              AND psid_tax_locid = pLocId
              AND psid_tax_trno = pBillNo
            GROUP BY psid_tax_trno
        ) dsum ON h.psih_tax_trno = dsum.psid_tax_trno
        SET
            h.psih_tax_tamount     = dsum.sum_amt,
            h.psih_tax_titemdisamt = dsum.sum_itemdisamt,
            h.psih_tax_tgrossamt   = dsum.sum_gross,
            h.psih_tax_ttaxamt     = dsum.sum_tax,
            h.psih_tax_tnetamt     = ROUND(dsum.sum_net, 2)
        WHERE h.psih_tax_comid = pComId
          AND h.psih_tax_locid = pLocId
          AND h.psih_tax_trno = pBillNo;

        SELECT IFNULL(MYD_ADD_SETATUS, 0)
          INTO vTaxInEx
        FROM MYD_SETTING_OPT
        WHERE MYD_ADD_SETNAME = 'TAX INCL EXCLU'
        LIMIT 1;

        UPDATE pos_tax_invoicehdr
        SET psih_tax_roundoff =
            CASE
                WHEN vTaxInEx = 1 THEN ROUND(psih_tax_tnetamt - (psih_tax_tgrossamt + psih_tax_ttaxamt), 2)
                ELSE 0
            END
        WHERE psih_tax_comid = pComId
          AND psih_tax_locid = pLocId
          AND psih_tax_trno = pBillNo;

        COMMIT;

    ELSEIF pMode = 'DelInvoice' THEN

        START TRANSACTION;

        SELECT psih_tax_created
          INTO vInvCreated
        FROM pos_tax_invoicehdr
        WHERE psih_tax_comid = pComId
          AND psih_tax_locid = pLocId
          AND psih_tax_trno  = pBillNo
        LIMIT 1;

        IF vInvCreated IS NULL THEN
            ROLLBACK;
            LEAVE proc_end;
        END IF;

        DELETE FROM pos_tax_invoicedtl
        WHERE psid_tax_comid = pComId
          AND psid_tax_locid = pLocId
          AND psid_tax_trno  = pBillNo;

        DELETE FROM pos_tax_invoicehdr
        WHERE psih_tax_comid = pComId
          AND psih_tax_locid = pLocId
          AND psih_tax_trno  = pBillNo;

        DELETE FROM pos_tax_paymode
        WHERE ComId = pComId
          AND LocId = pLocId
          AND Sal_Id = pBillNo
          AND DATE(Created) = DATE(vInvCreated);

        COMMIT;

    ELSE
        SIGNAL SQLSTATE '45000'
          SET MESSAGE_TEXT = 'Invalid pMode. Use Header/Detail/DetNetAmt/HeadNetAmt/Process/DelInvoice.';
    END IF;

END proc_end$$
DELIMITER ;
