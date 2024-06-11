SELECT e.id_employee, e.employees_name,
    max(case when t.status_name = 'Запланировано'then t.status_count else 0 end) as 'Запланировано',
    max(case when t.status_name = 'В работе'then t.status_count else 0 end) as 'В работе',
    max(case when t.status_name = 'Завершено'then t.status_count else 0 end) as 'Завершено',
    max(case when t.status_name = 'Сдано'then t.status_count else 0 end) as 'Сдано',
    max(case when t.status_name = 'Просрочено'then t.status_count else 0 end) as 'Просрочено'
FROM employees AS e
LEFT JOIN (SELECT t.id_employee,s.status_name,COUNT(t.id_status) as status_count
            FROM tasks as t
            JOIN _status as s ON s.id_status = t.id_status
            GROUP BY t.id_employee, s.status_name
            ) AS t ON t.id_employee = e.id_employee
WHERE e.id_employee = 1
   GROUP BY 
    e.employees_name, e.id_employee;


SELECT e.id_employee, e.employees_name,
    max(case when t.status_name = 'Запланировано'then t.status_count else 0 end) as 'Запланировано',
    max(case when t.status_name = 'В работе'then t.status_count else 0 end) as 'В работе',
    max(case when t.status_name = 'Завершено'then t.status_count else 0 end) as 'Завершено',
    max(case when t.status_name = 'Сдано'then t.status_count else 0 end) as 'Сдано',
    max(case when t.status_name = 'Просрочено'then t.status_count else 0 end) as 'Просрочено'
FROM employees AS e
LEFT JOIN (SELECT t.id_employee,s.status_name,COUNT(t.id_status) as status_count
            FROM tasks as t
            join steps as st on st.id_step=t.id_step
            join projects as p on p.id_project=st.id_project
            JOIN _status as s ON s.id_status = t.id_status
            where p.id_project=1
            GROUP BY t.id_employee, s.status_name
            ) AS t ON t.id_employee = e.id_employee
WHERE e.id_employee = 1
   GROUP BY 
    e.employees_name, e.id_employee;



SELECT e.id_employee, e.employees_name,
            MAX(CASE WHEN t.status_name = 'Запланировано' THEN t.status_count ELSE 0 END) AS 'Запланировано',
            MAX(CASE WHEN t.status_name = 'В работе' THEN t.status_count ELSE 0 END) AS 'В работе',
            MAX(CASE WHEN t.status_name = 'Завершено' THEN t.status_count ELSE 0 END) AS 'Завершено',
            MAX(CASE WHEN t.status_name = 'Сдано' THEN t.status_count ELSE 0 END) AS 'Сдано',
            MAX(CASE WHEN t.status_name = 'Просрочено' THEN t.status_count ELSE 0 END) AS 'Просрочено'
        FROM employees AS e
        LEFT JOIN (
            SELECT t.id_employee, s.status_name, COUNT(t.id_status) AS status_count
            FROM tasks AS t
            JOIN steps AS st ON st.id_step = t.id_step
            JOIN projects AS p ON p.id_project = st.id_project
            JOIN _status AS s ON s.id_status = t.id_status
            WHERE p.id_project = COALESCE(1, p.id_project) -- Проверка наличия проекта в случае его передачи
            GROUP BY t.id_employee, s.status_name
        ) AS t ON t.id_employee = e.id_employee
        WHERE e.id_employee = 1
        GROUP BY e.id_employee, e.employees_name