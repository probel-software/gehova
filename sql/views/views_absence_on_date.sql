/********************************************************************/
drop view if exists reception_morning_v;
create view reception_morning_v as
    select day_name
         , team
         , person_name     
    from (
        select team
             , person_name
             , 1       as day_nr
             , 'lundi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_morning = 1
            except
            select person_name
                , team
            from absence_reception_morning_v
            where date_start <= (select monday from settings_weekday_v limit 1)
            and (select monday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 2       as day_nr
             , 'mardi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_morning = 1
            except
            select person_name
                , team
            from absence_reception_morning_v
            where date_start <= (select tuesday from settings_weekday_v limit 1)
            and (select tuesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 3       as day_nr
             , 'mercredi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_morning = 1
            except
            select person_name
                , team
            from absence_reception_morning_v
            where date_start <= (select wednesday from settings_weekday_v limit 1)
            and (select wednesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 4       as day_nr
             , 'jeudi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_morning = 1
            except
            select person_name
                , team
            from absence_reception_morning_v
            where date_start <= (select thursday from settings_weekday_v limit 1)
            and (select thursday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 5       as day_nr
             , 'vendredi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_morning = 1
            except
            select person_name
                , team
            from absence_reception_morning_v
            where date_start <= (select friday from settings_weekday_v limit 1)
            and (select friday from settings_weekday_v limit 1) <= date_end
        )
   )
   order by day_nr;
/********************************************************************/
drop view if exists reception_evening_v;
create view reception_evening_v as
    select day_name
         , team
         , person_name
    from (
        select team
             , person_name 
             , 1       as day_nr
             , 'lundi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_evening = 1
            except
            select person_name
                , team
            from absence_reception_evening_v
            where date_start <= (select monday from settings_weekday_v limit 1)
            and (select monday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 2       as day_nr
             , 'mardi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_evening = 1
            except
            select person_name
                , team
            from absence_reception_evening_v
            where date_start <= (select tuesday from settings_weekday_v limit 1)
            and (select tuesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 3          as day_nr
             , 'mercredi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_evening = 1
            except
            select person_name
                , team
            from absence_reception_evening_v
            where date_start <= (select wednesday from settings_weekday_v limit 1)
            and (select wednesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 4       as day_nr
             , 'jeudi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_evening = 1
            except
            select person_name
                , team
            from absence_reception_evening_v
            where date_start <= (select thursday from settings_weekday_v limit 1)
            and (select thursday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 5          as day_nr
             , 'vendredi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_reception_evening = 1
            except
            select person_name
                , team
            from absence_reception_evening_v
            where date_start <= (select friday from settings_weekday_v limit 1)
            and (select friday from settings_weekday_v limit 1) <= date_end
        )
   )
   order by day_nr;
/********************************************************************/
drop view if exists lunchtime_v;
create view lunchtime_v as
    select day_name
         , team
         , person_name         
    from (
        select team
             , person_name 
             , 1       as day_nr
             , 'lundi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_lunchtime = 1
            except
            select person_name
                , team
            from absence_lunchtime_v
            where date_start <= (select monday from settings_weekday_v limit 1)
            and (select monday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 2       as day_nr
             , 'mardi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_lunchtime = 1
            except
            select person_name
                , team
            from absence_lunchtime_v
            where date_start <= (select tuesday from settings_weekday_v limit 1)
            and (select tuesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 3          as day_nr
             , 'mercredi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_lunchtime = 1
            except
            select person_name
                , team
            from absence_lunchtime_v
            where date_start <= (select wednesday from settings_weekday_v limit 1)
            and (select wednesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 4       as day_nr
             , 'jeudi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_lunchtime = 1
            except
            select person_name
                , team
            from absence_lunchtime_v
            where date_start <= (select thursday from settings_weekday_v limit 1)
            and (select thursday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , person_name 
             , 5          as day_nr
             , 'vendredi' as day_name 
        from (
            select person_name, team
            from people_v
            where is_lunchtime = 1
            except
            select person_name
                , team
            from absence_lunchtime_v
            where date_start <= (select friday from settings_weekday_v limit 1)
            and (select friday from settings_weekday_v limit 1) <= date_end
        )
   )
   order by day_nr;