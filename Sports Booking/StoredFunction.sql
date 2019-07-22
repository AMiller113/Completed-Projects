USE sports_booking;

DELIMITER $$
CREATE FUNCTION check_cancellation(p_booking_id INT) RETURNS INT DETERMINISTIC
	BEGIN
		DECLARE v_done INT;
        DECLARE v_cancellation INT;
        DECLARE v_current_payment_status VARCHAR(50);
        DECLARE cur CURSOR FOR
			SELECT payment_status FROM sports_booking.bookings WHERE member_id = 
			(SELECT bookings.member_id FROM sports_booking.bookings WHERE bookings.booking_id = p_booking_id)
			ORDER BY datetime_of_booking DESC;
        DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;
        SET @v_done = 0;
        SET @v_cancellation = 0;
        OPEN cur;
        
        cancellation_loop:LOOP
        FETCH cur INTO v_current_payment_status;
        IF v_current_payment_status <> 'Cancelled' OR v_done = 1  
        THEN LEAVE cancellation_loop;
        ELSE SET @v_cacellation = @v_cancellation + 1;
        END IF;
        END LOOP;
        
        CLOSE cur;
        RETURN v_cancellation;
	END $$
DELIMITER ;