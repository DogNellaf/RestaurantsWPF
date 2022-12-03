CREATE TABLE "Position" (
    id       			int PRIMARY KEY,
    name				varchar(50),
    salary           	float,
	prize				float,
	role_id 			int,
	FOREIGN KEY (role_id) REFERENCES "WorkerRole" (id)
 );