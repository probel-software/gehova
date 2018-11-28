drop view if exists absence_today_v;
create view absence_today_v as 
    select a.date_start                       as date_start
         , a.date_end                         as date_end
         , p.first_name || ' ' || p.last_name as person_name
         , t.name                             as team
    from absence a
    inner join person p on a.person_id = p.id
    inner join team t   on p.team_id = t.id
    where date('now') > date_start 
      and date('now') < date_end;
-------------------------------------------------------------------------------
drop view if exists all_absence_v;
create view all_absence_v as 
    select a.date_start                       as date_start
         , a.date_end                         as date_end
         , p.first_name || ' ' || p.last_name as person_name
         , t.name                             as team
         , is_lunchtime
         , is_reception_evening
         , is_reception_morning
    from absence a
    inner join person p on a.person_id = p.id
    inner join team t   on p.team_id = t.id;
-------------------------------------------------------------------------------
drop view if exists absence_lunchtime_v;
create view absence_lunchtime_v as 
    select date_start
         , date_end
         , person_name
         , team
      from all_absence_v
    where is_lunchtime = 1;
-------------------------------------------------------------------------------
drop view if exists absence_reception_morning_v;
create view absence_reception_morning_v as     
        select date_start
         , date_end
         , person_name
         , team
     from all_absence_v
    where is_reception_morning = 1;
-------------------------------------------------------------------------------
drop view if exists absence_reception_evening_v;
create view absence_reception_evening_v as 
        select date_start
         , date_end
         , person_name
         , team
     from all_absence_v
    where is_reception_evening = 1;