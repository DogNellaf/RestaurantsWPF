CREATE TABLE "Meal_to_OnlineOrder" (
    meal_id			    int,
    online_order_id		int,
    count           	int,
	FOREIGN KEY (online_order_id) REFERENCES "OnlineOrder" (id),
	FOREIGN KEY (meal_id) REFERENCES "Meal" (id)
 );