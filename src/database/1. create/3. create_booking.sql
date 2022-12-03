CREATE TABLE "Booking" (
    id            		INT PRIMARY KEY,
    time         		timestamp,
    client_id           int,
	is_paid				bool,
	table_id			int,
	FOREIGN KEY (client_id) REFERENCES "Client" (id),
	FOREIGN KEY (table_id) REFERENCES "Table" (id)
 );