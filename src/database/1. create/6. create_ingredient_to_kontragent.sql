CREATE TABLE "Ingredient_to_Kontragent" (
    ingredient_id       int,
    kontragent_id		int,
    weight           	float,
	cost				float,
	FOREIGN KEY (ingredient_id) REFERENCES "Ingredient" (id),
	FOREIGN KEY (kontragent_id) REFERENCES "Kontragent" (id)
 );