--/////////////////DELETE///////////////// удаление данных проекта

delete from projects;

delete from steps;

delete from tasks;


--/////////////////DROP///////////////// удаление  проекта

drop table tasks;

drop table steps;

drop table projects;




--/////////////////DELETE///////////////// удаление данных справочника

delete from posts;-- Project_db.dbo.post;
		    
delete from organisations;-- Project_db.dbo.organisation;
		    
delete from employees;-- Project_db.dbo.employees;


--///////// //////////////--////////DROP///////////////// удаление справочника

drop table  employees;--Project_db.dbo.employees;

drop table  organisations;--Project_db.dbo.organisation;

drop table  posts;--Project_db.dbo.post;




--/////////////////DELETE///////////////// удаление данных вспомогательных таблиц

delete from work_time;

delete from _status; 


--/////////////////DROP///////////////// удаление вспомогательных таблиц
drop table work_time;

drop table _status;

