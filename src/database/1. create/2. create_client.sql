CREATE TABLE "Client" (
    id            		serial PRIMARY KEY,
    username         	varchar(50),
    first_name          varchar(50),
	second_name			varchar(50),
	"password" 			text
);