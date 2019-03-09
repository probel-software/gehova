drop view if exists presence_full_v;
create view presence_full_v as
    select p.*
         , rp.reception_id 
         , r.name   as reception
         , r.o_val  as r_order
         , rg.o_val as rg_order
         , r.reception_group_id        
         , rg.name  as reception_group
    from everyone_v p 
    left join reception_person rp on p.person_id = rp.person_id
    left join reception r         on r.id        = rp.reception_id
    left join reception_group rg  on rg.id       = r.reception_group_id;
/********************************************************************/
drop view if exists presence_monday_v;
create view presence_monday_v as
    select * 
    from presence_full_v p
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select monday from settings_weekday_v limit 1)
        and (select monday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/
drop view if exists presence_tuesday_v;
create view presence_tuesday_v as
    select * 
    from presence_full_v p
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select tuesday from settings_weekday_v limit 1)
        and (select tuesday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/
drop view if exists presence_wednesday_v;
create view presence_wednesday_v as
    select * 
    from presence_full_v p
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select wednesday from settings_weekday_v limit 1)
        and (select wednesday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/
drop view if exists presence_thursday_v;
create view presence_thursday_v as
    select * 
    from presence_full_v p
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select thursday from settings_weekday_v limit 1)
        and (select thursday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/
drop view if exists presence_friday_v;
create view presence_friday_v as
    select * 
    from presence_full_v p
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select friday from settings_weekday_v limit 1)
        and (select friday from settings_weekday_v limit 1) <= date_end
    );
/********************************************************************/
drop view if exists presence_week_v;
create view presence_week_v as
    select * 
    from (
        select 1 as "day", d.*
        from presence_monday_v d
        union    
        select 2 as "day", d.*
        from presence_tuesday_v d
        union
        select 3 as "day", d.*
        from presence_wednesday_v d
        union
        select 4 as "day", d.*
        from presence_thursday_v d
        union
        select 5 as "day", d.*
        from presence_friday_v d
    )
    order by "day"              asc
           , reception_group_id asc
           , reception_id       asc
           , team               asc
           , category           asc
           , rg_order           asc
           , r_order            asc;