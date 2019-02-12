drop view if exists everyone_v;
create view everyone_v as
    select p.id
         , p.first_name as first_name
         , p.last_name  as last_name
         , t.name       as team
         , t.id         as team_id
         , pu.name      as pickup_round
         , pu.id        as pickup_round_id
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
    left join team t on t.id = p.team_id
    left join pickup_round pu on pu.id = p.pickup_round_id
    order by category_key, p.last_name;
