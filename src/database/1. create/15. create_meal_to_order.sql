CREATE TABLE "Meal_to_Order" (
    meal_id			    int,
    order_id			int,
    count           	int,
	FOREIGN KEY (order_id) REFERENCES "Order" (id),
	FOREIGN KEY (meal_id) REFERENCES "Meal" (id)
 );