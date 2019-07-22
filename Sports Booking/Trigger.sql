USE sports_booking;

DELIMITER $$

CREATE TRIGGER payment_check BEFORE DELETE ON sports_booking.members FOR EACH ROW
	BEGIN
		DECLARE v_payment_due DECIMAL (10, 2);
        SELECT members.payment_due INTO v_payment_due FROM sports_booking.members WHERE members.id = OLD.id;
        IF v_payment_due > 0 THEN
			INSERT INTO sports_booking.pending_terminations
			VALUES (OLD.id, OLD.email, OLD.payment_due);
		END IF;
	END $$

DELIMITER ;