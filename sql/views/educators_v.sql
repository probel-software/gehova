drop view if exists educators_v;
create view educators_v as
    select p.first_name
         , p.last_name
         , c.display
         , t.name
    from person p
    inner join person_category pc on p.id = pc.person_id 
    inner join category c         on c.id = pc.category_id
    inner join team t             on t.id = p.team_id
    where c.key = 'edu';