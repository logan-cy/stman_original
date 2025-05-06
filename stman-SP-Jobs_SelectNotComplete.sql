DELIMITER $$

USE `stman`$$

DROP PROCEDURE IF EXISTS `Jobs_SelectNotComplete`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Jobs_SelectNotComplete`()
BEGIN
	SELECT
		jo.*,
		c.complexid,
		c.name AS complexname,
		sub.name,
		sub.contactperson AS subcontact,
		sub.contactnumber AS subnumber,
		ins.name AS insname,
		ins.contactname AS inscontact,
		ins.contactnumber AS insnumber
	FROM jobs jo
	INNER JOIN units u ON jo.unitid = u.unitid
	INNER JOIN complex c ON u.complexid = c.complexid
	LEFT JOIN subcontractors sub ON jo.subcontractors = sub.subcontractorid
	LEFT JOIN policies pol ON jo.policyid = pol.policyid
	LEFT JOIN insurers ins ON pol.insurerid = ins.insurerid
	WHERE  STATUS <> 'Complete'
	ORDER BY jo.startdate DESC;
    END$$

DELIMITER ;