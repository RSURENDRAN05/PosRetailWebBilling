-- voucher_sales only stored voucher_no as the bare number (e.g. 1), because
-- _ValidateVoucherCode splits the typed code into prefix (from voucher_master) + number
-- for range matching. Add the full code as typed (e.g. 'A1') so reports/tables show it
-- directly without joining back to voucher_master.
-- Run this on the live MySQL database (myposqrc).

ALTER TABLE `voucher_sales`
    ADD COLUMN `vs_vouchercode` VARCHAR(30) NOT NULL DEFAULT '' AFTER `voucher_no`;
