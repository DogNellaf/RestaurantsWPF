CREATE TABLE "Ingredient_to_Meal" (
    ingredient_id       int,
    meal_id				int,
    weight           	float,
	FOREIGN KEY (ingredient_id) REFERENCES "Ingredient" (id),
	FOREIGN KEY (meal_id) REFERENCES "Meal" (id)
 );