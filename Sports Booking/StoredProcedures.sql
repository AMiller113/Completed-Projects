USE Sports_Booking;

#----------------------------------SPORTS BOOKINGS - STORED PROCEDURES------------------------------------------------#
DELIMITER $$
CREATE PROCEDURE insert_new_member(IN p_id varchar(255),IN p_password varchar(50),IN p_email varchar(255))
	BEGIN
		INSERT INTO sports_booking.members 
        VALUES (p_id,p_password,p_email);
    END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE delete_member (IN p_id varchar(255))
	BEGIN
		DELETE FROM sports_booking.members
        WHERE sports_booking.members.id = p_id;
    END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_member_password(IN p_id varchar(255),IN p_password varchar(50))
	BEGIN
		UPDATE sports_booking.members
        SET sports_booking.members.member_password = p_password
        WHERE sports_booking.members.id = p_id;
    END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_member_email (IN p_id varchar(255), IN p_email varchar(255))
	BEGIN
		UPDATE sports_booking.members
        SET sports_booking.members.email = p_email
        WHERE sports_booking.members.id = p_id;
    END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE make_booking(IN p_room_id VARCHAR(255), p_booked_date DATE, p_booked_time TIME, p_member_id VARCHAR(255))
	BEGIN
		DECLARE v_price DECIMAL(10 , 2);
        DECLARE v_payment_due DECIMAL(10 , 2);
        SELECT rooms.price INTO v_price FROM sports_booking.rooms WHERE rooms.id = p_room_id;
        SELECT members.payment_due INTO v_payment_due FROM sports_booking.members WHERE members.id = p_member_id; 
        INSERT INTO sports_booking.members VALUES (p_room_id,p_member_id,p_booked_date,p_booked_time);
        UPDATE sports_booking.members
        SET members.payment_due = v_price + v_payment_due
        WHERE sports_booking.members.id = p_member_id;
    END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_payment(IN p_id INT)
	BEGIN
		DECLARE v_member_id VARCHAR(255);
        DECLARE v_payment_due DECIMAL(10, 2);
        DECLARE v_price DECIMAL(10, 2);
		UPDATE sports_booking.bookings
        SET bookings.payment_status = 'Paid'
        WHERE bookings.member_id = p_id;
        SELECT bk.member_id, rm.price INTO v_member_id, v_price FROM member_bookings WHERE bk.member_id = p_id;
        SELECT members.payment_due INTO v_payment_due FROM sports_booking.members WHERE member.id = v_member_id;
        UPDATE sports_booking.members
        SET members.payment_die = v_payment_due - v_price
        WHERE id = v_member_id;
    END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE view_bookings(IN p_id VARCHAR(255))
	BEGIN
		SELECT * FROM sports_booking.member_bookings WHERE bk.member_id = p_id;
    END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE search_room(IN p_room_type VARCHAR(255), IN p_booked_date DATE, IN p_booked_time TIME)
	BEGIN
		SELECT rm.id, rm.room_type 
        FROM sports_booking.rooms rm
        WHERE rm.id NOT IN 
        (SELECT bk.room_id
         FROM sports_booking.bookings bk
         join
         sports_booking.rooms rm
         ON bk.room_id = rm.id
         WHERE rm.room_type = p_room_type AND bk.booked_date = p_booked_date AND bk.booked_time = p_booked_time);
    END $$
DELIMITER ;
		