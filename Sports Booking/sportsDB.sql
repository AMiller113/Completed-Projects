CREATE DATABASE Sports_Booking;

USE Sports_Booking;

#----------------------------------TABLE CREATION------------------------------------------------#
CREATE TABLE Members (
    id VARCHAR(255) PRIMARY KEY,
    member_password VARCHAR(50) NOT NULL,
    email VARCHAR(255) NOT NULL,
    member_since TIMESTAMP DEFAULT NOW(),
    payment_due DECIMAL(10 , 2 ) NOT NULL DEFAULT 0.0
);

CREATE TABLE Pending_Terminations (
    id VARCHAR(255),
    email VARCHAR(255) NOT NULL,
    payment_due DECIMAL(10 , 2 ) NOT NULL DEFAULT 0.0,
    request_date TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE Rooms (
    id VARCHAR(255) PRIMARY KEY,
    room_type VARCHAR(255) NOT NULL,
    price DECIMAL(10 , 2 ) NOT NULL
);

CREATE TABLE Bookings (
    booking_id INT PRIMARY KEY AUTO_INCREMENT,
    room_id VARCHAR(255) NOT NULL,
    member_id VARCHAR(255) NOT NULL,
    booked_date DATE NOT NULL,
    booked_time TIME NOT NULL,
    datetime_of_booking TIMESTAMP NOT NULL DEFAULT NOW(),
    payment_status VARCHAR(50) NOT NULL DEFAULT 'Unpaid',
    CONSTRAINT uc1 UNIQUE (room_id , booked_date , booked_time),
    CONSTRAINT fk1 FOREIGN KEY (member_id)
        REFERENCES members (id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk2 FOREIGN KEY (room_id)
        REFERENCES rooms (id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

#----------------------------------DATA INSERTION------------------------------------------------#

INSERT INTO members (id, member_password, email, member_since, payment_due) VALUES 
('afeil', 'feil1988<3', 'Abdul.Feil@hotmail.com', '2017-04-15 12:10:13', 0),
('amely_18', 'loseweightin18', 'Amely.Bauch91@yahoo.com', '2018-02-06 16:48:43', 0),
('bbahringer', 'iambeau17', 'Beaulah_Bahringer@yahoo.com', '2017-12-28 05:36:50', 0),
('little31', 'whocares31', 'Anthony_Little31@gmail.com', '2017-06-01 21:12:11', 10),
('macejkovic73', 'jadajeda12', 'Jada.Macejkovic73@gmail.com', '2017-05-30 17:30:22', 0),
('marvin1', 'if0909mar', 'Marvin_Schulist@gmail.com', '2017-09-09 02:30:49', 10),
('nitzsche77', 'bret77@#', 'Bret_Nitzsche77@gmail.com', '2018-01-09 17:36:49', 0),
('noah51', '18Oct1976#51', 'Noah51@gmail.com', '2017-12-16 22:59:46', 0),
('oreillys', 'reallycool#1', 'Martine_OReilly@yahoo.com', '2017-10-12 05:39:20', 0),
('wyattgreat', 'wyatt111', 'Wyatt_Wisozk2@gmail.com', '2017-07-18 16:28:35', 0);

INSERT INTO rooms (id, room_type, price) VALUES 
('AR', 'Archery Range', 120),
('B1', 'Badminton Court', 8),
('B2', 'Badminton Court', 8),
('MPF1', 'Multi Purpose Field', 50),
('MPF2', 'Multi Purpose Field', 60),
('T1', 'Tennis Court', 10),
('T2', 'Tennis Court', 10);


INSERT INTO bookings (booking_id, room_id, booked_date, booked_time, member_id, datetime_of_booking, payment_status) VALUES
(1, 'AR', '2017-12-26', '13:00:00', 'oreillys', '2017-12-20 20:31:27', 'Paid'),
(2, 'MPF1', '2017-12-30', '17:00:00', 'noah51', '2017-12-22 05:22:10', 'Paid'),
(3, 'T2', '2017-12-31', '16:00:00', 'macejkovic73', '2017-12-28 18:14:23', 'Paid'),
(4, 'T1', '2018-03-05', '08:00:00', 'little31', '2018-02-22 20:19:17', 'Unpaid'),
(5, 'MPF2', '2018-03-02', '11:00:00', 'marvin1', '2018-03-01 16:13:45', 'Paid'),
(6, 'B1', '2018-03-28', '16:00:00', 'marvin1', '2018-03-23 22:46:36', 'Paid'),
(7, 'B1', '2018-04-15', '14:00:00', 'macejkovic73', '2018-04-12 22:23:20', 'Cancelled'),
(8, 'T2', '2018-04-23', '13:00:00', 'macejkovic73', '2018-04-19 10:49:00', 'Cancelled'),
(9, 'T1', '2018-05-25', '10:00:00', 'marvin1', '2018-05-21 11:20:46', 'Unpaid'),
(10, 'B2', '2018-06-12', '15:00:00', 'bbahringer', '2018-05-30 14:40:23', 'Paid');

#----------------------------------VIEW CREATION------------------------------------------------#

CREATE VIEW member_bookings AS
SELECT bk.booking_id, bk.room_id,rm.room_type, bk.booked_date, bk.booked_time, bk.member_id, bk.datetime_of_booking, rm.price, bk.payment_status
FROM Sports_Booking.bookings bk
JOIN
Sports_Booking.rooms rm
ON bk.room_id = rm.id
ORDER BY bk.booking_id;