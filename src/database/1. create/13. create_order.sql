CREATE TABLE "Order" (
    id       			int PRIMARY KEY,
    status_id			int,
    created           	timestamp,
	table_id 			int,
	worker_id			int,
	FOREIGN KEY (status_id) REFERENCES "OrderStatus" (id),
	FOREIGN KEY (table_id) REFERENCES "Table" (id),
	FOREIGN KEY (worker_id) REFERENCES "Worker" (id)
 );