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
/********************************************************************/
drop view if exists people_v;
create view people_v as    
    select first_name || ' ' || last_name as person_name
         , team
    from all_person_v;
/********************************************************************/
drop view if exists reception_morning_current_week_v;
create view reception_morning_current_week_v as
    select day_name
         , team
         , person_name         
    from (
        select *
        from (
            select *, 'lundi' as day_name, 1 as day_nr
            from people_v
            except
            select person_name
                , team
                , 'lundi' as day_name
                , 1       as day_nr
            from absence_reception_morning_v
            where date_start <= date('now', (1 - strftime('%w', date('now'))) || ' days') 
            and date('now', (1 - strftime('%w', date('now'))) || ' days')  <= date_end
        )
        union
        select *
        from (
            select *, 'mardi' as monday_nameday, 2 as day_nr
            from people_v
            except
            select person_name
                , team
                , 'mardi' as day_name
                , 2       as day_nr
            from absence_reception_morning_v
            where date_start <= date('now', (2 - strftime('%w', date('now'))) || ' days') 
            and date('now', (2 - strftime('%w', date('now'))) || ' days')  <= date_end
        )
        union
        select *
        from (
            select *, 'mercredi' as day_name, 3 as day_nr
            from people_v
            except
            select person_name
                , team
                , 'mercredi' as day_name
                , 3       as day_nr
            from absence_reception_morning_v
            where date_start <= date('now', (3 - strftime('%w', date('now'))) || ' days') 
            and date('now', (3 - strftime('%w', date('now'))) || ' days')  <= date_end
        )
        union
        select *
        from (
            select *, 'jeudi' as day_name, 4 as day_nr
            from people_v
            except
            select person_name
                , team
                , 'jeudi' as day_name
                , 4       as day_nr
            from absence_reception_morning_v
            where date_start <= date('now', (4 - strftime('%w', date('now'))) || ' days') 
            and date('now', (4 - strftime('%w', date('now'))) || ' days')  <= date_end
        )
        union
        select *
        from (
            select *, 'vendredi' as day_name, 5 as day_nr
            from people_v
            except
            select person_name
                , team
                , 'vendredi' as day_name
                , 5       as day_nr
            from absence_reception_morning_v
            where date_start <= date('now', (5 - strftime('%w', date('now'))) || ' days') 
            and date('now', (5 - strftime('%w', date('now'))) || ' days')  <= date_end
        )
   )
   order by day_nr;