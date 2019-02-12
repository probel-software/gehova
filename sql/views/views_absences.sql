drop view if exists presence_monday_v;
create view presence_monday_v as
    select p.*
         , rp.reception_id         
    from everyone_v p 
    inner join reception_person rp on p.id = rp.person_id
    where p.id not in (
        select person_id
        from absence
        where date_start <= (select monday from settings_weekday_v limit 1)
        and (select monday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/
drop view if exists presence_tuesday_v;
create view presence_tuesday_v as
    select p.*
         , rp.reception_id         
    from everyone_v p 
    inner join reception_person rp on p.id = rp.person_id
    where p.id not in (
        select person_id
        from absence
        where date_start <= (select monday from settings_weekday_v limit 1)
        and (select tuesday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/
drop view if exists presence_wednesday_v;
create view presence_wednesday_v as
    select p.*
         , rp.reception_id         
    from everyone_v p 
    inner join reception_person rp on p.id = rp.person_id
    where p.id not in (
        select person_id
        from absence
        where date_start <= (select monday from settings_weekday_v limit 1)
        and (select wednesday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/
drop view if exists presence_thursday_v;
create view presence_thursday_v as
    select p.*
         , rp.reception_id         
    from everyone_v p 
    inner join reception_person rp on p.id = rp.person_id
    where p.id not in (
        select person_id
        from absence
        where date_start <= (select monday from settings_weekday_v limit 1)
        and (select thursday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/drop view if exists presence_thursday_v;
drop view if exists presence_friday_v;
create view presence_friday_v as
    select p.*
         , rp.reception_id         
    from everyone_v p 
    inner join reception_person rp on p.id = rp.person_id
    where p.id not in (
        select person_id
        from absence
        where date_start <= (select monday from settings_weekday_v limit 1)
        and (select friday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/