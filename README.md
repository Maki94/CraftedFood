Tema: Crafted food - korporativna dostava hrane

System to offer, organize, plan, track, deliver and invoice food delivery (breakfast or lunch) for employees in companies.

Kettering company creates a food menu where each meal is defined with: title, description, image, quantity (weight, volume or number of pieces), unit of measure (grams, millilitres, piece), category (salad, sandwich, bakery, pasta, sweet, drink, cooked meal).
Employee from a company/client that orders the food, access the menu and creates the schedule of meals he wants to be delivered to him for days ahead (week or month) with quantity for each item. He can also add note for each entry (e.g. exclude certain ingredient).
For each day, ketterer personnel has grouped list of meals/drinks what should be delivered for that day and what should be cooked for that day or day ahead (to check necessary supplies).
For each day, when food is delivered, employees should get a list of delivered meals with info who ordered it. Employee can comment on particular meal in a menu and rate it. Employee can comment on particular meal that is delivered on certain day (with special remarks about that meal on that day).
For each time interval (day/week/month), ketterer can create an invoice with all the items, quantity and prices, that should be charged to the client.

Actors - users:
● Admin: Kettering company administrator
○ Administer all other users
○ Defines food menu
○ Has access to invoicing reports (and all other)
● User: Kettering company chef, delivery and supplier personnel
○ Has access to delivery report for a client company for particular day
● Client: Company employee
○ Has access to ordering report to see what did he order
Use cases:
● Login page:
○ Email / Password
○ BONUS: Password recovery
● User management:
○ Each user can edit his own name, email, mobile, password
○ Admin can edit others
● Menu:
○ Management of meals for Admin only (add, edit, delete)
○ Read only table view of whole menu, accessible by all users
○ BONUS: Support to comment meal and rate it by client employees
● Schedule meals:
○ Each client can define one or more meal items to order for particular day
○ Have a calendar range picker
○ BONUS: Ability to make a note for a meal
○ BONUS: Ability to make a comment (after meal is delivered)
● Delivery report:
○ Grouped list of meals and drinks with quantities to prepare and deliver
○ Date picker for certain day
● Order report (to know which employee should take what when food is delivered):
○ List of meals/drinks per each employee
○ Date picker for certain day
● Invoice report:
○ Grouped list of all ordered meals and drinks to be payed
○ Have a calendar range picker
