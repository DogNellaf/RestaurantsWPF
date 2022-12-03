CREATE TABLE "Worker" (
    id       			int PRIMARY KEY,
    first_name			varchar(50),
    last_name           varchar(50),
	phone 				int,
	position_id			int,
	username 			varchar(50),
	password			text,
	FOREIGN KEY (position_id) REFERENCES "Position" (id)
 );