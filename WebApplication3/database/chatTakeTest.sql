CREATE DATABASE chatTakeTest;

USE chatTakeTest;

CREATE TABLE user(
	id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
	NAME VARCHAR(45) NOT NULL
);

CREATE TABLE room(
	id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
	NAME VARCHAR(45) NOT NULL
);

CREATE TABLE user_has_room(
	user_id INT(6) UNSIGNED,
	room_id INT(6) UNSIGNED,
	PRIMARY KEY (user_id, room_id),
	FOREIGN KEY (user_id) REFERENCES user (id),
	FOREIGN KEY (room_id) REFERENCES room (id)
);


CREATE TABLE message(
	id 					INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
	message 				VARCHAR(254) NOT NULL,
	user_id 				INT(6) UNSIGNED,
	private_user_id 	INT(6) UNSIGNED NULL,
	room_id 				INT(6) UNSIGNED,
	FOREIGN KEY (user_id) REFERENCES user (id),
	FOREIGN KEY (private_user_id) REFERENCES user (id),
	FOREIGN KEY (room_id) REFERENCES room (id)
);

INSERT INTO room(name) VALUES('salaPublica');
INSERT INTO user(NAME) VALUES('admin');