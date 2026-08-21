-- Enrich voucher_sales with the same audit fields the POS client now records locally
-- (billno/billamount/locationid/companyid/shiftno/dayno), so the cloud copy carries
-- the full redemption record, not just voucher_id/voucher_no/sal_id.
-- Run this on the live MySQL database (myposqrc).
--
-- NOTE: voucher_sales already had a `createddate` column when this first ran; `vs_created`
-- below duplicated it. We're keeping `vs_created` (matches the vs_* naming of the other
-- audit columns) and dropping `createddate` instead - see
-- drop_voucher_sales_duplicate_created_column.sql.

ALTER TABLE `voucher_sales`
    ADD COLUMN `vs_comid`      INT            NOT NULL DEFAULT 0  AFTER `sal_id`,
    ADD COLUMN `vs_locid`      INT            NOT NULL DEFAULT 0  AFTER `vs_comid`,
    ADD COLUMN `vs_shiftno`    INT            NOT NULL DEFAULT 0  AFTER `vs_locid`,
    ADD COLUMN `vs_dayno`      INT            NOT NULL DEFAULT 0  AFTER `vs_shiftno`,
    ADD COLUMN `vs_billamount` DECIMAL(18,2)  NOT NULL DEFAULT 0  AFTER `vs_dayno`,
    ADD COLUMN `vs_created`    DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP AFTER `vs_billamount`;
