-- Fix: voucher_sales ended up with two creation-timestamp columns after the audit-fields
-- ALTER ran - the pre-existing `createddate` and the newly added `vs_created` (both just
-- default to CURRENT_TIMESTAMP; neither is ever set explicitly by the PHP code).
-- Keeping `vs_created` (matches the vs_* naming used by the other new audit columns);
-- dropping the older `createddate`.
-- Run this once on the live MySQL database (myposqrc).

ALTER TABLE `voucher_sales`
    DROP COLUMN `createddate`;
