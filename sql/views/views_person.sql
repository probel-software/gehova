drop view if exists all_person_v;
create view all_person_v as
    select p.first_name as first_name
         , p.last_name  as last_name
         , c.display    as category
         , t.name       as team
         , c.key        as "key"
    from person p
    inner join person_category pc on p.id = pc.person_id 
    inner join category c         on c.id = pc.category_id
    inner join team t             on t.id = p.team_id;
/********************************************************************/
drop view if exists educators_v;
create view educators_v as
    select *
    from all_person_v
    where "key" = 'edu';
/********************************************************************/
drop view if exists drivers_v;
create view drivers_v as
    select *
    from all_person_v
    where "key" = 'drv';

/********************************************************************/
drop view if exists phm_v;
create view phm_v as
    select *
    from all_person_v
    where "key" = 'phm';    