--create database Project_db;

--use Project_db;

-- //////////////////CREATE////////////////// создание вспомогательных таблиц

CREATE TABLE work_time -- Создание таблицы work_time
(
    id_work_time TINYINT PRIMARY KEY IDENTITY(1,1),
    work_time TINYINT NOT NULL UNIQUE
);

CREATE TABLE _status -- Создание таблицы status
(
    id_status TINYINT PRIMARY KEY IDENTITY(1,1),
    status_name NVARCHAR(50) NOT NULL UNIQUE
);




-- //////////////////CREATE////////////////// создание таблиц справочника

create table posts
(
	id_post INT not null primary key IDENTITY(1,1),
    id_work_time TINYINT default(1),
	post_name nvarchar(100) not null, 
	post_description nvarchar(100),
	salary int not null,
	allowance tinyint -- надбавка за переработку
	FOREIGN KEY (id_work_time) REFERENCES work_time(id_work_time)ON DELETE SET NULL
);

create table organisations
(
	id_organisation INT not null primary key IDENTITY(1,1),  
	organisation_name nvarchar(100) not null, 
	--organisation_type nvarchar(100),
	--organisation_description nvarchar(4000),
	--legal_address nvarchar(200),
	--physical_address nvarchar(200),
    phone_number nvarchar(20),
    email nvarchar(320),
	--TIN BIGINT -- ИНН организации
);

create table employees
(
	id_employee INT not null primary key IDENTITY(1,1),  
	id_organisation int,
	id_post int,
	employees_name nvarchar(100) not null, 
    phone_number nvarchar(20) unique ,
    email nvarchar(320),
	--residence_address nvarchar(200),
	--date_of_birth date default('11.11.1111'),
	--gender nvarchar(10),
	FOREIGN KEY (id_organisation) REFERENCES organisations(id_organisation) ON DELETE SET NULL,
	FOREIGN KEY (id_post) REFERENCES posts(id_post) ON DELETE SET NULL
);




-- //////////////////CREATE////////////////// создание таблиц проекта

create table projects
(
	id_project INT not null primary key IDENTITY(1,1),  
    id_status TINYINT default(1),
	project_name nvarchar(100) unique not null, 
    project_description nvarchar(4000),
	date_of_start date,--время с которого проект необходимо выполнять
	date_of_end date,--время до которого проект необходимо выполнить
	planed_budget decimal(10,2),
	--,    actual_budget decimal(10,2)
	FOREIGN KEY (id_status) REFERENCES _status(id_status)ON DELETE SET NULL
);

create table steps
(
	id_step INT not null primary key IDENTITY(1,1),
	id_project int not null,
    id_status TINYINT default(1),
	step_name nvarchar(100) not null, 
	step_description nvarchar(4000),
	date_of_start date,--время с которого проект необходимо выполнять
	date_of_end date,--время до которого проект необходимо выполнить
	planed_budget decimal(10,2),
	FOREIGN KEY (id_project) REFERENCES projects(id_project) ON DELETE CASCADE,
	FOREIGN KEY (id_status) REFERENCES _status(id_status)ON DELETE SET NULL,
	CONSTRAINT UC_Project_StepName UNIQUE (id_project, step_name)
);

create table tasks
(
	id_task INT not null primary key IDENTITY(1,1),
	id_step INT not null, 
    id_status TINYINT default(1),
	id_employee INT,
	task_name nvarchar(100) not null,
	task_description nvarchar(4000),
	date_of_start date,--время с которого задача необходимо выполнять
	date_of_end date,--время до которого задачу необходимо выполнить
	task_time tinyint, --время затраченное на выполнение задачи
	FOREIGN KEY (id_step) REFERENCES steps(id_step) ON DELETE CASCADE,
	FOREIGN KEY (id_status) REFERENCES _status(id_status) ON DELETE SET NULL,
	FOREIGN KEY (id_employee) REFERENCES employees(id_employee)ON DELETE SET NULL,
	CONSTRAINT UC_Step_TaskName UNIQUE (id_step, task_name)
);
	



-- //////////////////CREATE////////////////// смежные таблицы

--create table assignments
--(
--	id_task INT not null,
--	primary key(id_task, id_employee),
--    FOREIGN KEY (id_task) REFERENCES tasks(id_task) ON DELETE CASCADE,
--);



-- //////////////////ALTER////////////////// изменение

-- ALTER TABLE db_project.steps ADD CONSTRAINT FOREIGN KEY (id_project) REFERENCES projects(id_project);
-- ALTER TABLE db_project.tasks ADD CONSTRAINT FOREIGN KEY (id_step) REFERENCES steps(id_step);
-- ALTER TABLE db_project.tasks ADD CONSTRAINT FOREIGN KEY (id_employee) REFERENCES employees(id_employee);



-- //////////////////SELECT////////////////// запросы

select * from tasks;

-- insert into db_project.employees_projects(project_id, employee_id) values(1, 1);
