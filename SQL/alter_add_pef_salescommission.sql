-- Fix: Add missing pef_salescommission column to pos_emp_finalprocess table
-- Error: Unknown column 'pef_salescommission' in 'field list' (clsfunctionmgmt.php:2574)
-- Run this on the live MySQL database (myposqrc)

ALTER TABLE `pos_emp_finalprocess` 
ADD COLUMN `pef_salescommission` DECIMAL(10,2) NOT NULL DEFAULT 0.00;
