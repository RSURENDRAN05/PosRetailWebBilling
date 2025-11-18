
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
