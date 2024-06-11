--/////////////////INSERT///////////////// вставка данных вспомогательных таблиц

INSERT INTO work_time (work_time) VALUES (40), (36), (24);

--INSERT INTO _status (status_name) VALUES ('Запланировано'), ('В работе'), ('Завершено'), ('Сдано'), ('Просрочено');

INSERT INTO _status (status_name) VALUES ('Planed'), ('In progress'), ('Completed'), ('Delivered'), ('Overdue');



--/////////////////INSERT///////////////// вставка данных справочника

INSERT INTO posts (post_name, post_description, salary, allowance)
VALUES 
    ('Software Developer', 'Develops software applications', 60000, 5),
    ('Marketing Manager', 'Manages marketing campaigns', 70000, 8),
    ('HR Specialist', 'Handles recruitment and employee relations', 50000, 3),
    ('Financial Analyst', 'Analyzes financial data and prepares reports', 65000, 6),
    ('Sales Executive', 'Responsible for sales and business development', 75000, 7),
    ('Project Manager', 'Oversees project execution and delivery', 80000, 10),
    ('Graphic Designer', 'Creates visual concepts and designs', 55000, 4),
    ('Customer Support Representative', 'Provides customer assistance and support', 45000, 2),
    ('Quality Assurance Tester', 'Tests software applications for defects', 55000, 4),
    ('Accountant', 'Manages financial records and prepares statements', 60000, 5);

INSERT INTO organisations (organisation_name, phone_number, email)
VALUES 		
    ('ABC Corporation',	 '123-456-7890',	 'info@abccorp.com'),
    ('XYZ Industries',	 '456-789-0123',	 'contact@xyzindustries.com'),
    ('Tech Solutions Inc.',	 '789-012-3456',	 'info@techsolutions.com'),
    ('Global Bank',	 '321-654-9870',	 'customerservice@globalbank.com'),
    ('Sunshine Hospital',	 '789-012-3456',	 'info@sunshinehospital.com'),
    ('Green Energy LLC',	 '456-789-0123',	 'info@greenenergy.com'),
    ('ABC Foundation',	 '123-456-7890',	 'info@abcfoundation.org'),
    ('Silver Insurance Agency',	 '789-012-3456',	 'info@silverinsurance.com'),
    ('Foodie Restaurant Group',	 '456-789-0123',	 'info@foodierestaurants.com'),
    ('City Library',	 '321-654-9870',	 'info@citylibrary.gov')


INSERT INTO employees (id_organisation, id_post, employees_name,phone_number, email)
VALUES 				
    (3, 2, 'Michael Williams',	 '789-012-3456',	 'michael.williams@example.com'),
    (1, 1, 'John Smith',	 '123-456-7890',	 'john.smith@example.com'),
    (2, 3, 'Emily Johnson',	 '456-789-0123',	 'emily.johnson@example.com'),
    (4, 4, 'Jessica Brown',	 '321-654-9870',	 'jessica.brown@example.com'),
    (5, 5, 'David Miller',	 '789-012-3456',	 'david.miller@example.com'),
    (6, 6, 'Sarah Davis',	 '456-789-0123',	 'sarah.davis@example.com'),
    (7, 7, 'James Wilson',	 '123-456-7890',	 'james.wilson@example.com'),
    (8, 8, 'Linda Moore',	 '789-012-3456',	 'linda.moore@example.com'),
    (9, 9, 'William Taylor',	 '456-789-0123',	 'william.taylor@example.com'),
    (10, 10, 'Emma Anderson',	 '321-654-9870',	 'emma.anderson@example.com')




--/////////////////INSERT///////////////// вставка данных проекта


INSERT INTO projects (id_status, project_name, project_description, date_of_start, date_of_end, planed_budget)
VALUES
    (1, 'Project A', 'Description for Project A', '2023-01-01', '2023-12-31', '10000'),
    (2, 'Project B', 'Description for Project B', '2023-02-01', '2023-11-30', '15000'),
    (3, 'Project C', 'Description for Project C', '2023-03-01', '2023-10-31', '20000'),
    (4, 'Project D', 'Description for Project D', '2023-04-01', '2023-09-30', '25000'),
    (1, 'Project E', 'Description for Project E', '2023-05-01', '2023-08-31', '30000'),
    (2, 'Project F', 'Description for Project F', '2023-06-01', '2023-07-31', '35000'),
    (3, 'Project G', 'Description for Project G', '2023-07-01', '2023-06-30', '40000'),
    (4, 'Project H', 'Description for Project H', '2023-08-01', '2023-05-31', '45000'),
    (1, 'Project I', 'Description for Project I', '2023-09-01', '2023-04-30', '50000'),
    (2, 'Project J', 'Description for Project J', '2023-10-01', '2023-03-31', '55000');


INSERT INTO steps (id_project, id_status, step_name, step_description, date_of_start, date_of_end, planed_budget)
VALUES 
(1, 1, 'Step 1', 'Description for Step 1', '2023-01-01', '2023-01-10', '1000'),
(2, 2, 'Step 2', 'Description for Step 2', '2023-02-01', '2023-02-10', '2000'),
(3, 3, 'Step 3', 'Description for Step 3', '2023-03-01', '2023-03-10', '3000'),
(4, 4, 'Step 4', 'Description for Step 4', '2023-04-01', '2023-04-10', '4000'),
(5, 5, 'Step 5', 'Description for Step 5', '2023-05-01', '2023-05-10', '5000'),
(6, 1, 'Step 6', 'Description for Step 6', '2023-06-01', '2023-06-10', '6000'),
(7, 2, 'Step 7', 'Description for Step 7', '2023-07-01', '2023-07-10', '7000'),
(8, 3, 'Step 8', 'Description for Step 8', '2023-08-01', '2023-08-10', '8000'),
(9, 4, 'Step 9', 'Description for Step 9', '2023-09-01', '2023-09-10', '9000'),
(10, 5, 'Step 10', 'Description for Step 10', '2023-10-01', '2023-10-10', '10000');


INSERT INTO tasks (id_step, id_employee, id_status, task_name, task_description, date_of_start, date_of_end, task_time)
VALUES
    (1, 1, 1, 'Task 1', 'Description for Task 1', '2024-06-01', '2024-06-02', 5),
    (1, 2, 2, 'Task 2', 'Description for Task 2', '2024-06-02', '2024-06-03', 6),
    (2, 3, 3, 'Task 3', 'Description for Task 3', '2024-06-03', '2024-06-04', 4),
    (2, 4, 4, 'Task 4', 'Description for Task 4', '2024-06-04', '2024-06-05', 7),
    (3, 5, 1, 'Task 5', 'Description for Task 5', '2024-06-05', '2024-06-06', 3),
    (3, 1, 2, 'Task 6', 'Description for Task 6', '2024-06-06', '2024-06-07', 8),
    (4, 2, 3, 'Task 7', 'Description for Task 7', '2024-06-07', '2024-06-08', 5),
    (4, 3, 4, 'Task 8', 'Description for Task 8', '2024-06-08', '2024-06-09', 6),
    (5, 4, 1, 'Task 9', 'Description for Task 9', '2024-06-09', '2024-06-10', 7),
    (5, 5, 2, 'Task 10', 'Description for Task 10', '2024-06-10', '2024-06-11', 4);
