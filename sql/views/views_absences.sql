drop view if exists absence_today_v;
create view absence_today_v as 
    select * 
    from absence
    where date('now') > date_start 
      and date('now') < date_end;