TRUNCATE TABLE pos_sale_invoicehdr;
TRUNCATE TABLE pos_sale_invoicedtl;
TRUNCATE TABLE journaldetails;
TRUNCATE TABLE pos_sale_deletelog;
UPDATE POS_MASTER SET PM_TRANS_NO =0;
