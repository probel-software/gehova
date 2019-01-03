drop view if exists everyone_v;
create view everyone_v as
    select p.id
         , p.first_name as first_name
         , p.last_name  as last_name
         , t.name       as team
         , t.id         as team_id
         , p.is_lunchtime
         , p.is_reception_morning
         , p.is_reception_evening
         , (
         	select group_concat(c."display", ', ')
         	from person_category pc
         	inner join category c on pc.category_id = c.id
         	where pc.person_id = p.id
         ) as category
         , (
         	select group_concat(c."key", ', ')
         	from person_category pc
         	inner join category c on pc.category_id = c.id
         	where pc.person_id = p.id
         ) as category_key
    from person p   
    left join team t on t.id = p.team_id;
/********************************************************************/
drop view if exists people_v;
create view people_v as    
    select first_name 
         , last_name 
         , team
         , is_lunchtime
         , is_reception_morning
         , is_reception_evening
    from everyone_v;
/********************************************************************/
