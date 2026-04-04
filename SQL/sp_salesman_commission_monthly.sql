DELIMITER $$

DROP PROCEDURE IF EXISTS sp_salesman_commission_monthly $$
CREATE PROCEDURE sp_salesman_commission_monthly(
    IN p_date DATE,
    IN p_comid INT,
    IN p_locid INT
)
BEGIN
    SELECT
        pe.emp_id AS ID,
        pe.emp_printname AS Name,
        CAST(SUM(psid.psid_invoice_netamt) AS DECIMAL(18,2)) AS TotalNetAmt,
        CAST(AVG(psid.psid_invoice_salemanper) AS DECIMAL(5,2)) AS AvgPercentage,
        CAST(SUM((psid.psid_invoice_netamt * psid.psid_invoice_salemanper) / 100) AS DECIMAL(18,2)) AS TotalCommission
    FROM pos_sale_invoicedtl psid
    INNER JOIN pos_sale_invoicehdr psih
        ON psih.psih_invoice_trno = psid.psid_invoice_trno
       AND psih.psih_invoice_billstatus = 'Closed'
       AND psih.psih_invoice_comid = p_comid
       AND psih.psih_invoice_locid = p_locid
    INNER JOIN pos_employeeinfo pe
        ON pe.emp_id = psid.psid_invoice_salesmanid
    WHERE psid.psid_invoice_comid = p_comid
      AND psid.psid_invoice_locid = p_locid
      AND psid.psid_invoice_date >= DATE_FORMAT(p_date, '%Y-%m-01')
      AND psid.psid_invoice_date < DATE_ADD(DATE_FORMAT(p_date, '%Y-%m-01'), INTERVAL 1 MONTH)
    GROUP BY pe.emp_id, pe.emp_printname
    ORDER BY TotalCommission DESC;
END $$

DELIMITER ;