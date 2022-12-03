CREATE TABLE "OnlineOrder" (
    id       			int PRIMARY KEY,
    created				timestamp,
    client_id           int,
	address 			text,
	is_complited		bool,
	FOREIGN KEY (client_id) REFERENCES "Client" (id)
 );